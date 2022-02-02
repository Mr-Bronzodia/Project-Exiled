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
    private float totalSpread;
    private Vector3 finalTarget;



    public void SetUp(SkillVariables stats)
    {
        Vector3 castingOffset = stats.caster.transform.TransformDirection(Vector3.forward) * OFFSET;

        totalSpread = stats.spread * stats.numberOfProjectiles;

        if (stats.numberOfProjectiles > 1)
        {
            for (int i = 1; i <= stats.numberOfProjectiles; i++)
            {
                GameObject skill = Instantiate(gameObject, stats.caster.transform.position + castingOffset, stats.caster.transform.rotation);
                skill.GetComponent<FireballBehaviour>().SetStats(stats);

                skill.transform.rotation *= Quaternion.Euler(0, -(totalSpread / 2) + (stats.spread * i), 0);
            }
        }
        else if (stats.numberOfProjectiles == 1)
        {
            GameObject skill = Instantiate(gameObject, stats.caster.transform.position + castingOffset, stats.caster.transform.rotation);
            skill.GetComponent<FireballBehaviour>().SetStats(stats);
        }
    }

    public void SetStats(SkillVariables stats)
    {
        this.skillStatistics = stats;
    }

    public void OnHitDetected(GameObject other, List<ParticleCollisionEvent> collisionEvent)
    { 
        if (other.tag != skillStatistics.caster.tag & other.tag != "Wall")
        {
            Debug.Log("Valid Hit form " + skillStatistics.caster.name + " to " + other.name);
            other.GetComponent<Character>().ApplyDamage(skillStatistics.damage);
            Destroy(gameObject);
        }
        else if (other.tag == "Wall")
        {
            Debug.Log("Wall");
            Vector3 newTarget = Vector3.Reflect(finalTarget, collisionEvent[0].normal);
            newTarget = Vector3.ClampMagnitude(newTarget, skillStatistics.range);
            transform.rotation = Quaternion.LookRotation(newTarget);
            transform.position += Vector3.ClampMagnitude(newTarget, 2);
            finalTarget = newTarget;
            castingPos = collisionEvent[0].intersection;
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

            targetLocation = new Vector3(transform.TransformDirection(Vector3.forward).x,
                                     transform.TransformDirection(Vector3.forward).y,
                                     transform.TransformDirection(Vector3.forward).z);

            castingPos += castingOffset;
            finalTarget = targetLocation * skillStatistics.range;
            isFirstCast = false;
        }

        transform.position += finalTarget.normalized * skillStatistics.projectileSpeed * Time.deltaTime;

        Debug.DrawRay(castingPos, finalTarget, Color.blue);

        if (Vector3.Distance(castingPos, transform.position - finalTarget) <= skillStatistics.range * 0.15)
        {
            Destroy(gameObject);
        }
    }
}
