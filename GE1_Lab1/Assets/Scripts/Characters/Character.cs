using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public float maxMana;
    private float currentMana;
    public float manaRegenAmount = 0.01f;
    public float healRegenAmount = 0.01f;

    private float timer = 0;

    private const float REGEN_INTERVAL = 0.1f;

    public List<GameObject> nearCharacters;

    public TagManager tagManager;


    void Start()
    {
        Physics.IgnoreCollision(GetComponent<SphereCollider>(), GetComponent<CharacterController>());
        currentHealth = maxHealth;
        currentMana = maxMana;
        nearCharacters = new List<GameObject>();
        tagManager = new TagManager(gameObject.tag);
    }

    private void UpdateUI()
    {
        if (gameObject.tag == "Player")
        {
            CharacterUI UI = gameObject.GetComponent<CharacterUI>();

            UI.UpdateHealth(currentHealth);
            UI.UpdateMana(currentMana);
        }
        else if (gameObject.tag == "Enemy" | gameObject.tag == "Ally")
        {
            EnemyUI UI = gameObject.GetComponent<EnemyUI>();

            UI.UpdateHealth(currentHealth);
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
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateUI();
    }

    private void Die()
    {
        foreach (GameObject nearChar in nearCharacters)
        {
            nearChar.GetComponent<Character>().nearCharacters.Remove(gameObject);
        }

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

        private static List<string> characterEntity = new List<string>() { "Player", "Ally", "Enemy" };

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

    }
}
