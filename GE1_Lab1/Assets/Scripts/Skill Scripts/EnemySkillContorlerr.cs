using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillContorlerr : MonoBehaviour
{
    public GameObject skill;
    private float nextCast;
    public SkillVariables skillStats;

    private void Start()
    {
        skillStats = new SkillVariables
        {
            range = 10,
            caster = gameObject,
            target = null,
            projectileSpeed = 10,
            damage = 10,
            cooldown = 3,
            numberOfProjectiles = 3,
            spread = 30,
            totalBounces = 2,
            totalChains = 0,
            isAutoTargeted = false,
            manaCost = 10
        };
        nextCast = Time.time + skillStats.cooldown;
    }

    private void Update()
    {
        if (skillStats.target & gameObject.GetComponent<Pathfinding>().targetInRange)
        {
            if (Time.time > nextCast)
            {
                gameObject.transform.LookAt(skillStats.target.transform.position);
                skill.GetComponent<FireballBehaviour>().SetUp(skillStats);
                nextCast = Time.time + skillStats.cooldown;
            }

        }
    }
}
