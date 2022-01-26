using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{

    public List<GameObject> skills;

    private List<InventoryManager> inventory;

    private void Start()
    {
        inventory = new List<InventoryManager>();
        inventory.Add(new InventoryManager { skill = skills[0], ActivateAbility = () => skills[0].GetComponent<FireballBehaviour>().SetUp(gameObject.GetComponent<SkillVariables>())});
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            inventory[0].ActivateAbility();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            gameObject.GetComponent<SkillVariables>().range += 10;
        }


    }


    public class InventoryManager
    {
        public GameObject skill;
        public Action ActivateAbility;
    }
}




