using System.Collections.Generic;
using UnityEngine;
using static Charm;

public class Character : MonoBehaviour
{
    public int level;
    public float maxHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;
    public float manaRegenAmount = 0.01f;
    public float healRegenAmount = 0.01f;
    public float speed;

    public GameObject PlayerPrefab;
    public GameObject SpawnLocation;

    private float timer = 0;

    private const float REGEN_INTERVAL = 0.1f;

    public List<GameObject> nearCharacters;

    public TagManager tagManager;

    public GameObject charmPrefab;

    public List<CharmItem> charmInventory;

    public bool isCountering = false;
    public int counterProjectileCount = 0;

    private bool requiresCharmsUpdate = false;
    private Animator animator;
    private bool isDying = false;


    void Start()
    {
        Physics.IgnoreCollision(GetComponent<SphereCollider>(), GetComponent<CharacterController>());


        currentHealth = maxHealth;
        currentMana = maxMana;


        nearCharacters = new List<GameObject>();
        tagManager = new TagManager(gameObject.tag);
        animator = gameObject.GetComponentInChildren<Animator>();

        if (TagManager.isNPC(gameObject.tag))
        {
            Dictionary<CharmItem.charmType, float> lootTable = CharmItem.GetLootTable();
            int appliedCharmsCount = 0;

            foreach (CharmItem.charmType charmType in CharmItem.GetAllAvailibleTypes())
            {
                if (Random.value > lootTable[charmType])
                {
                    if (appliedCharmsCount < level)
                    {
                        CharmItem generatedCharm = new CharmItem(Random.Range(level, level + 1));
                        generatedCharm.type = charmType;
                        AddCharm(generatedCharm);
                        appliedCharmsCount++;
                    }
                    else
                    {
                        break;
                    }
                    
                }
            }
        }

        

    }

    public List<CharmItem> GetCharms()
    {
        return charmInventory;
    }

    public void RemoveCharmFromInventory(CharmItem charm)
    {
        charmInventory.Remove(charm);
    }

    public void AddCharm(CharmItem charm)
    {
        charmInventory.Add(charm);
        charm.SetOwner(this);
        requiresCharmsUpdate = true;
    }

    private void UpdateUI()
    {
        if (TagManager.isPlayer(gameObject.tag))
        {
            CharacterUI UI = gameObject.GetComponent<CharacterUI>();

            UI.UpdateHealth(currentHealth, maxHealth);
            UI.UpdateMana(currentMana, maxMana);
        }
        else if (TagManager.isNPC(gameObject.tag))
        {

            if (isDying)
            {
                return;
            }

            EnemyUI UI = gameObject.GetComponent<EnemyUI>();

            UI.UpdateHealth(currentHealth, maxHealth);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= REGEN_INTERVAL)
        {
            RegenerateHealth(healRegenAmount);
            RegenrateMana(manaRegenAmount);
            UpdateUI();

            timer = 0;
        }

        if (requiresCharmsUpdate & TagManager.isNPC(gameObject.tag))
        {
            foreach(CharmItem item in charmInventory)
            {
                if (!item.WasApplied)
                {
                    item.Apply(gameObject.GetComponent<NPCSkillManager>().inventory[0]);
                    item.WasApplied = true;
                }  
            }

            requiresCharmsUpdate = false;
        }
        else if (requiresCharmsUpdate & TagManager.isCharacter(gameObject.tag))
        {
            gameObject.GetComponentInChildren<CharmInventory>().Refresh();
            requiresCharmsUpdate = false;
        }
    }

    private void RegenrateMana(float amount)
    {
        if (currentMana + amount > maxMana)
        {
            currentMana = maxMana;
        }
        else
        {
            currentMana += amount;
        }
    }

    private void RegenerateHealth(float amount)
    {
        if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
    }

    public void ApplyDamage(float damage)
    {
        if (isDying)
        {
            return;
        }

        currentHealth -= damage;

        animator.SetTrigger("Take Damage Trigger");

        if (currentHealth <= 0)
        {
            OnDieBegin();
        }

        UpdateUI();
    }

    public void OnDieBegin()
    {
        if (TagManager.isNPC(gameObject.tag))
        {
            gameObject.GetComponent<Pathfinding>().targetInRange = false;
            gameObject.GetComponent<Pathfinding>().enabled = false;
            animator.SetTrigger("Die Trigger");
        }
        else if (TagManager.isPlayer(gameObject.tag))
        {
            gameObject.transform.position = SpawnLocation.transform.position;
            currentHealth = 1;
            currentMana = 1;
        }
        
    }

    private void DropCharms()
    {
        foreach (CharmItem charm in charmInventory)
        {
            GameObject charmDrop = Instantiate(charmPrefab, new Vector3(gameObject.transform.position.x + UnityEngine.Random.Range(-3,3), 1.95f, gameObject.transform.position.z + UnityEngine.Random.Range(-3, 3)), new Quaternion(0,0,0,0));
            charmDrop.GetComponent<Charm>().SetItem(charm);
        }
    }

    public void Die()
    {
        foreach (GameObject nearChar in nearCharacters)
        {
            nearChar.GetComponent<Character>().nearCharacters.Remove(gameObject);
        }

        DropCharms();

        Destroy(gameObject);
    }

    public float GetMana()
    {
        return currentMana;
    }

    public void UseMana(float cost)
    {
        if (currentMana - cost <= 0)
        {
           currentMana = 0;
        }
        else
        {
            currentMana -= cost;
        }

        UpdateUI();
    }

    public List<GameObject> GetNearCharacters()
    {
        return nearCharacters;
    }

    public List<GameObject> GetNearFriendlies()
    {
        List<GameObject> friendlies = new List<GameObject>();

        foreach (GameObject character in nearCharacters)
        {
            if (tagManager.isFriendly(character.tag))
            {
                friendlies.Add(character);
            }
        }
        return friendlies;
    }

    public List<GameObject> GetNearEnemies()
    {
        List<GameObject> enemies = new List<GameObject>();

        foreach (GameObject character in nearCharacters)
        {
            if (tagManager.isHostile(character.tag))
            {
                enemies.Add(character);
            }
        }
        return enemies;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TagManager.isCharacter(other.tag) & other is CharacterController)
        {
            if (!nearCharacters.Contains(other.gameObject))
            {
                nearCharacters.Add(other.gameObject);
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (TagManager.isCharacter(other.tag) & other is CharacterController)
        {
            nearCharacters.Remove(other.gameObject);
        }
    }

    public class TagManager
    {
        private string selfTag;

        public TagManager(string selfTag)
        {
            this.selfTag = selfTag;
        }

        private static Dictionary<string, List<string>> friendlyTags = new Dictionary<string, List<string>>()
        {
            {"Player", new List<string>() { "Ally", "Player" } },
            {"Ally",  new List<string>() { "Ally", "Player" } },
            {"Enemy",  new List<string>() { "Enemy" } }

        };

        private static List<string> npcs = new List<string>() { "Ally", "Enemy" };

        private static List<string> characterEntity = new List<string>() { "Player", "Ally", "Enemy" };

        private static List<string> player = new List<string>() { "Player" };

        public bool isFriendly(string tag)
        {
            if (friendlyTags[selfTag].Contains(tag))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isHostile(string tag)
        {
            if (!friendlyTags[selfTag].Contains(tag))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isCharacter(string tag)
        {
            if (characterEntity.Contains(tag))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isNPC(string tag)
        {
            if (npcs.Contains(tag))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isPlayer(string tag)
        {
            if (player.Contains(tag))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
