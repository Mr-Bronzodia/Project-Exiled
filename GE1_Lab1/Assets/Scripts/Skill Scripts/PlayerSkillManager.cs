using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Charm;

public class PlayerSkillManager : MonoBehaviour
{

    public List<GameObject> skills;

    public List<InventoryManager> inventory;

    private Dictionary<InventoryManager, List<CharmItem>> appliedCharms;

    private Character characterStatistic;

    private CharacterUI UI;

    public GameObject charmInventory;

    private Animator animator;


    private void Start()
    {
        inventory = new List<InventoryManager>();

        UI = gameObject.GetComponent<CharacterUI>();

        characterStatistic = gameObject.GetComponent<Character>();

        animator = gameObject.GetComponentInChildren<Animator>();

        appliedCharms = new Dictionary<InventoryManager, List<CharmItem>>();

        for (int i = 0; i < skills.Count; i++)
        {
            InventoryManager skill = new InventoryManager().RegisterSkill(skills[i]);
            inventory.Add(skill);
            appliedCharms.Add(skill, new List<CharmItem>());
        }

        InventoryManager character = new InventoryManager().RegisterSkill(gameObject);
        appliedCharms.Add(character, new List<CharmItem>());

    }

    public void AddCharmToActive(InventoryManager skill, CharmItem charm)
    {
        if (appliedCharms[skill].Count <= 5)
        {
            appliedCharms[skill].Add(charm);
            charm.Apply(skill);
            characterStatistic.RemoveCharmFromInventory(charm);
            gameObject.GetComponent<CharmInventory>().Refresh();
        }
    }

    public void RemoveCharmFromActive(InventoryManager skill, CharmItem charm)
    {
        appliedCharms[skill].Remove(charm);
        charm.Discharge(skill);
        characterStatistic.AddCharm(charm);
        gameObject.GetComponent<CharmInventory>().Refresh();
    }

    public Dictionary<InventoryManager, List<CharmItem>> GetAppliedCharms()
    {
        return appliedCharms;
    }

    private void Update()
    {
        UI.UpdateCooldown();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventory[0].CanCast())
            {
                inventory[0].OnCastBegin(gameObject, animator);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventory[1].CanCast())
            {
                inventory[1].OnCastBegin(gameObject, animator);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (inventory[2].CanCast())
            {
                inventory[2].OnCastBegin(gameObject, animator);
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (charmInventory.activeSelf)
            {
                charmInventory.SetActive(false);
                
            }
            else
            {
                charmInventory.SetActive(true);
                gameObject.GetComponent<CharmInventory>().Refresh();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inventory[3].CanCast())
            {
                inventory[3].OnCastBegin(gameObject, animator);
            }
        }
    }

    [Serializable]
    public class InventoryManager
    {
        public GameObject skill;
        public SkillVariables stats;
        public Action ActiveAbility;
        public float nextCast;
        private string animationTrigger;


        private string iconLocation;

        public InventoryManager RegisterSkill(GameObject skill)
        {
            this.skill = skill;

            if (skill.name == "Fireball")
            {
                stats = skill.GetComponent<Fireball>().baseStats.Clone();
                ActiveAbility = () => skill.GetComponent<Fireball>().SetUp(stats);
                nextCast = 0f;
                iconLocation = "Fireball";
                animationTrigger = "Fireball Trigger";

                return this;
            }
            else if (skill.name == "Dash")
            {
                stats = skill.GetComponent<Dash>().baseStats.Clone();
                ActiveAbility = () => skill.GetComponent<Dash>().SetUp(stats);
                nextCast = 0f;
                iconLocation = "Dash";
                animationTrigger = "Fireball Trigger";

                return this;
            }
            else if (skill.name == "Shadow Clone")
            {
                stats = skill.GetComponent<ShadowClone>().baseStats.Clone();
                ActiveAbility = () => skill.GetComponent<ShadowClone>().SetUp(stats);
                nextCast = 0f;
                iconLocation = "ShadowClon";
                animationTrigger = "Fireball Trigger";

                return this;
            }
            else if (skill.name == "Counter")
            {
                stats = skill.GetComponent<Counter>().baseStats.Clone();
                ActiveAbility = () => skill.GetComponent<Counter>().SetUp(stats);
                nextCast = 0f;
                iconLocation = "Counter";
                animationTrigger = "Fireball Trigger";

                return this;
            }
            else if (skill.name == "Player")
            {
                stats = null;
                ActiveAbility = null;
                nextCast = 0f;
                iconLocation = "Character";
                animationTrigger = null;

                return this;
            }
            else
            {
                Debug.LogError("Unable to assign: " + skill.name);
                return null;
            }
        }

        public Sprite GetIcon()
        {
            return Resources.Load<Sprite>(iconLocation);
        }

        public void OnCastBegin(GameObject user, Animator animator)
        {
            stats.caster = user;
            user.GetComponentInChildren<CastTriggers>().SetSkill(this);
            animator.SetTrigger(animationTrigger);
            nextCast = Time.time + stats.cooldown;
        }



        public bool CanCast()
        {
            if (Time.time > nextCast)
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




