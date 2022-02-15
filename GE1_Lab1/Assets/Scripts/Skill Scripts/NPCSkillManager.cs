using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerSkillManager;

public class NPCSkillManager : MonoBehaviour
{
    public List<GameObject> skills;
    public List<InventoryManager> inventory;

    private void Start()
    {
        inventory = new List<InventoryManager>();

        for (int i = 0; i < skills.Count; i++)
        {
            InventoryManager skill = new InventoryManager().RegisterSkill(skills[i]);
            inventory.Add(skill);
        }
    }

    private void Update()
    {
        if (inventory[0].stats.target & gameObject.GetComponent<Pathfinding>().targetInRange)
        {
            if (inventory[0].CanCast())
            {
                gameObject.transform.LookAt(inventory[0].stats.target.transform.position);
                inventory[0].Use(gameObject);
            }

        }
    }
}
