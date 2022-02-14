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

        SkillVariables fireballStats = skills[0].GetComponent<Fireball>().baseStats.Clone();
        InventoryManager fireball = new InventoryManager { skill = skills[0], stats = fireballStats, ActiveAbility = () => skills[0].GetComponent<Fireball>().SetUp(fireballStats), nextCast = 0 };
        inventory.Add(fireball);

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
