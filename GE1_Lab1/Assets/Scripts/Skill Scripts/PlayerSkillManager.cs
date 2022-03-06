using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{

    public List<GameObject> skills;

    public List<InventoryManager> inventory;

    private Character characterStatistic;

    private CharacterUI UI;

    public GameObject charmInventory;


    private void Start()
    {
        inventory = new List<InventoryManager>();

        UI = gameObject.GetComponent<CharacterUI>();

        for (int i = 0; i < skills.Count; i++)
        {
            InventoryManager skill = new InventoryManager().RegisterSkill(skills[i]);
            inventory.Add(skill);
        }
    }

    private void Update()
    {
        UI.UpdateCooldown();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventory[0].CanCast())
            {
                inventory[0].Use(gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventory[1].CanCast())
            {
                inventory[1].Use(gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (inventory[2].CanCast())
            {
                inventory[2].Use(gameObject);
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
                inventory[3].Use(gameObject);
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

        public InventoryManager RegisterSkill(GameObject skill)
        {
            this.skill = skill;

            if (skill.name == "Fireball")
            {
                stats = skill.GetComponent<Fireball>().baseStats.Clone();
                ActiveAbility = () => skill.GetComponent<Fireball>().SetUp(stats);
                nextCast = 0f;

                return this;
            }
            else if (skill.name == "Dash")
            {
                stats = skill.GetComponent<Dash>().baseStats.Clone();
                ActiveAbility = () => skill.GetComponent<Dash>().SetUp(stats);
                nextCast = 0f;

                return this;
            }
            else if (skill.name == "Shadow Clone")
            {
                stats = skill.GetComponent<ShadowClone>().baseStats.Clone();
                ActiveAbility = () => skill.GetComponent<ShadowClone>().SetUp(stats);
                nextCast = 0f;

                return this;
            }
            else if (skill.name == "Counter")
            {
                stats = skill.GetComponent<Counter>().baseStats.Clone();
                ActiveAbility = () => skill.GetComponent<Counter>().SetUp(stats);
                nextCast = 0f;

                return this;
            }
            else
            {
                Debug.LogError("Unable to assign: " + skill.name);
                return null;
            }
        }

        public void Use(GameObject user)
        {
            stats.caster = user;
            ActiveAbility();
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




