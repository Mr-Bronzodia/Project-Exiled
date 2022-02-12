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


    private void Start()
    {
        inventory = new List<InventoryManager>();

        SkillVariables fireballStats = skills[0].GetComponent<Fireball>().baseStats.Clone();

        fireballStats.caster = gameObject;

        InventoryManager fireball = new InventoryManager { skill = skills[0], stats = fireballStats, ActivateAbility = () => skills[0].GetComponent<Fireball>().SetUp(fireballStats), nextCast = 0f };
        inventory.Add(fireball);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventory[0].CanCast())
            {
                inventory[0].ActivateAbility();
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
                nextCast = Time.time + stats.cooldown;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}




