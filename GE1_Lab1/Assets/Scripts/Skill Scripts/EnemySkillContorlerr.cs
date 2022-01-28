using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillContorlerr : MonoBehaviour
{
    public GameObject skill;
    private float nextCast;

    private void Start()
    {
        nextCast = Time.time + gameObject.GetComponent<SkillVariables>().cooldown;
    }

    private void Update()
    {

        if (Time.time > nextCast)
        {
            gameObject.transform.LookAt(gameObject.GetComponent<SkillVariables>().target.transform.position);
            skill.GetComponent<FireballBehaviour>().SetUp(gameObject.GetComponent<SkillVariables>());
            nextCast = Time.time + gameObject.GetComponent<SkillVariables>().cooldown;
        }

    }
}
