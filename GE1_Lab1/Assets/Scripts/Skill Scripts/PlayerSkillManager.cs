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

        fireballStats.caster = gameObject;

        InventoryManager fireball = new InventoryManager { skill = skills[0], stats = fireballStats, ActivateAbility = () => skills[0].GetComponent<Fireball>().SetUp(fireballStats), nextCast = 0f };
        
        inventory.Add(fireball);
    }

    private void Update()
    {
        UI.UpdateCooldown();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventory[0].CanCast())
            {
                inventory[0].ActivateAbility();
                inventory[0].nextCast = Time.time + inventory[0].stats.cooldown;
            }
        }
    }

    [Serializable]
    public class InventoryManager
    {
        public GameObject skill;
        public SkillVariables stats;
        public Action ActivateAbility;
        public float nextCast;

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




