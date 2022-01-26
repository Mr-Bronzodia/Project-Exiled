using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTest : MonoBehaviour
{
    public GameObject skillPrefab;

    public float range;

    private GameObject skill;

    private Vector3 targetLocation;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            skill =  (GameObject)Instantiate(skillPrefab, transform.position, transform.rotation);
            targetLocation = new Vector3(transform.TransformDirection(Vector3.forward).x, transform.TransformDirection(Vector3.forward).y, transform.TransformDirection(Vector3.forward).z);
            skill.GetComponent<SkillBehaviourTest>().Setup(targetLocation.normalized, range);
        }


    }
}