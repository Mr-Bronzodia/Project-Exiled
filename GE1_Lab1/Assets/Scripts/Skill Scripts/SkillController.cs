using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillController : MonoBehaviour
{

    public List<GameObject> skills;

    private List<InventoryManager> inventory;

    private List<float> Cooldowns;

    private Character characterStatistic;

    private void Start()
    {
        inventory = new List<InventoryManager>();
        Cooldowns = new List<float>();
        Cooldowns.Insert(0, 0);
        characterStatistic = gameObject.GetComponent<Character>();
        inventory.Add(new InventoryManager { skill = skills[0], ActivateAbility = () => skills[0].GetComponent<FireballBehaviour>().SetUp(gameObject.GetComponent<SkillVariables>())});
    }

    private void Cast(int skillIndex)
    {

        if (Time.time > Cooldowns[skillIndex])
        {
            if (characterStatistic.GetMana() >= gameObject.GetComponent<SkillVariables>().manaCost)
            {
                inventory[skillIndex].ActivateAbility();
                Cooldowns[skillIndex] = Time.time + gameObject.GetComponent<SkillVariables>().cooldown;
                characterStatistic.UseMana(gameObject.GetComponent<SkillVariables>().manaCost);
            }   
        }
        else
        {
            Debug.Log(Cooldowns[skillIndex] - Time.time + "s Remaining");
        }
        
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Cast(0);
        }
    }


    public class InventoryManager
    {
        public GameObject skill;
        public Action ActivateAbility;
    }
}




