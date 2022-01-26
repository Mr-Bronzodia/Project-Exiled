using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    private SkillVariables skillStatistics;
    private Vector3 castingPos;
    private bool isFirstCast = true;
    private Vector3 targetLocation;


    public void SetUp(SkillVariables stats)
    {
        GameObject skill = Instantiate(gameObject, stats.caster.transform.position, stats.caster.transform.rotation);
        skill.GetComponent<FireballBehaviour>().SetStats(stats);
    }

    public void SetStats(SkillVariables stats)
    {
        this.skillStatistics = stats;
    }

    void Update()
    {
        if (isFirstCast)
        {
            castingPos = new Vector3(skillStatistics.caster.transform.position.x,
                                     skillStatistics.caster.transform.position.y,
                                     skillStatistics.caster.transform.position.z);
            isFirstCast = false;
        }


        targetLocation = new Vector3(transform.TransformDirection(Vector3.forward).x,
                                     transform.TransformDirection(Vector3.forward).y,
                                     transform.TransformDirection(Vector3.forward).z);


        transform.position += targetLocation.normalized * 100 * Time.deltaTime;
        Debug.DrawRay(castingPos, targetLocation * skillStatistics.range, Color.blue); 

        if (Vector3.Distance(castingPos, transform.position - targetLocation * skillStatistics.range) <= skillStatistics.range * 0.15)
        {
            Destroy(gameObject);
        }
    }
}
