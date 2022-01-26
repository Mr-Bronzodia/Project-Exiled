using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillContorlerr : MonoBehaviour
{
    public GameObject skill;


    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            gameObject.transform.LookAt(gameObject.GetComponent<SkillVariables>().target.transform);
            skill.GetComponent<FireballBehaviour>().SetUp(gameObject.GetComponent<SkillVariables>());
        }
    }
}
