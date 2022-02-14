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


    private void Start()
    {
        inventory = new List<InventoryManager>();

        UI = gameObject.GetComponent<CharacterUI>();

        SkillVariables fireballStats = skills[0].GetComponent<Fireball>().baseStats.Clone();
        InventoryManager fireball = new InventoryManager { skill = skills[0], stats = fireballStats, ActiveAbility = () => skills[0].GetComponent<Fireball>().SetUp(fireballStats), nextCast = 0f };
        
        inventory.Add(fireball);

        SkillVariables dashStatistic = skills[1].GetComponent<Dash>().baseStats.Clone();
        InventoryManager dash = new InventoryManager { skill = skills[1], stats = dashStatistic, ActiveAbility = () => skills[1].GetComponent<Dash>().SetUp(dashStatistic), nextCast = 0f };

        inventory.Add(dash);
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
    }

    [Serializable]
    public class InventoryManager
    {
        public GameObject skill;
        public SkillVariables stats;
        public Action ActiveAbility;
        public float nextCast;

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




