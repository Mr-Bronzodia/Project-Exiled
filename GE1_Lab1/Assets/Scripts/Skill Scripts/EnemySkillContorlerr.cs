using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillContorlerr : MonoBehaviour
{
    public GameObject skill;
    private float nextCast;
    private SkillVariables skillStats;

    private void Start()
    {
        skillStats = gameObject.GetComponent<SkillVariables>();
        nextCast = Time.time + skillStats.cooldown;
    }

    private void Update()
    {
        if (skillStats.target)
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
