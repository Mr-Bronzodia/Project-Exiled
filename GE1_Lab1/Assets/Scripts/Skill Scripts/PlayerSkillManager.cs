using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{

    public List<GameObject> skills;

    private List<InventoryManager> inventory;

    private Character characterStatistic;

    public List<SkillVariables> skillStatistic;

    private void Start()
    {
        inventory = new List<InventoryManager>();
        skillStatistic = new List<SkillVariables>();

        SkillVariables firebalStats = new SkillVariables 
        {
            range = 10, 
            caster = gameObject, 
            target = null, 
            projectileSpeed = 20, 
            damage = 10, 
            cooldown = 3, 
            numberOfProjectiles = 1, 
            spread = 5, 
            totalBounces = 2, 
            totalChains = 2, 
            isAutoTargeted = false, 
            manaCost = 10 
        };

        skillStatistic.Add(firebalStats);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            skills[0].GetComponent<FireballBehaviour>().SetUp(skillStatistic[0]);
        }
    }


    public class InventoryManager
    {
        public GameObject skill;
        public SkillVariables stats;
        public Action ActivateAbility;
    }
}




