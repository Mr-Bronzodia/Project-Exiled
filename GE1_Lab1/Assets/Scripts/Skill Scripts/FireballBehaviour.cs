using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    private SkillVariables skillStatistics;
    private Vector3 castingPos;
    private bool isFirstCast = true;
    private Vector3 targetLocation;
    private const int OFFSET = 2;


    public void SetUp(SkillVariables stats)
    {
        Vector3 castingOffset = stats.caster.transform.TransformDirection(Vector3.forward) * OFFSET;
        GameObject skill = Instantiate(gameObject, stats.caster.transform.position + castingOffset, stats.caster.transform.rotation);
        skill.GetComponent<FireballBehaviour>().SetStats(stats);
    }

    public void SetStats(SkillVariables stats)
    {
        this.skillStatistics = stats;
    }

    public void OnHitDetected(GameObject other)
    { 
        if (other.tag != skillStatistics.caster.tag & other.tag != "Wall")
        {
            Debug.Log("Valid Hit form " + skillStatistics.caster.name + " to " + other.name);
            other.GetComponent<Character>().ApplyDamage(skillStatistics.damage);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isFirstCast)
        {
            castingPos = new Vector3(skillStatistics.caster.transform.position.x,
                                     skillStatistics.caster.transform.position.y,
                                     skillStatistics.caster.transform.position.z);

            Vector3 castingOffset = skillStatistics.caster.transform.TransformDirection(Vector3.forward) * OFFSET;

            castingPos += castingOffset;
            isFirstCast = false;
        }


        targetLocation = new Vector3(transform.TransformDirection(Vector3.forward).x,
                                     transform.TransformDirection(Vector3.forward).y,
                                     transform.TransformDirection(Vector3.forward).z);


        transform.position += targetLocation.normalized * skillStatistics.projectileSpeed * Time.deltaTime;

        Debug.DrawRay(castingPos, targetLocation * skillStatistics.range, Color.blue); 

        if (Vector3.Distance(castingPos, transform.position - targetLocation * skillStatistics.range) <= skillStatistics.range * 0.15)
        {
            Destroy(gameObject);
        }
    }
}
