using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    private SkillVariables skillStats;
    private Vector3 castingPos;
    private bool isFirstCast = true;
    private Vector3 targetLocation;
    private const int OFFSET = 2;
    private float totalSpread;
    private Vector3 finalTarget;
    private int currentBounce = 0;



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
        this.skillStats = stats;
    }

    public void OnHitDetected(GameObject other, List<ParticleCollisionEvent> collisionEvents)
    { 
        if (other.tag != skillStats.caster.tag & other.tag != "Wall")
        {
            Impact(other);
        }
        else if (other.tag == "Wall")
        {
            if (currentBounce < skillStats.totalBounces)
            {
                Bounce(collisionEvents[0]);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }

    private void Impact(GameObject enemy)
    {
        enemy.GetComponent<Character>().ApplyDamage(skillStats.damage);

        if (skillStats.totalChains > 0)
        {
            Chain(enemy);
        }
        
        Destroy(gameObject);
    }

    private void Chain(GameObject initialEnemy)
    {

    }


    private void Bounce(ParticleCollisionEvent collisionEvent)
    {
        Vector3 newTarget = Vector3.Reflect(finalTarget, collisionEvent.normal);
        newTarget = Vector3.ClampMagnitude(newTarget, skillStats.range);
        transform.rotation = Quaternion.LookRotation(newTarget);
        transform.position += Vector3.ClampMagnitude(newTarget, 2);
        finalTarget = newTarget;
        castingPos = collisionEvent.intersection;
        currentBounce++;
    }


    void Update()
    {
        if (isFirstCast)
        {
            castingPos = new Vector3(skillStats.caster.transform.position.x,
                                     skillStats.caster.transform.position.y,
                                     skillStats.caster.transform.position.z);

            Vector3 castingOffset = skillStats.caster.transform.TransformDirection(Vector3.forward) * OFFSET;

            targetLocation = new Vector3(transform.TransformDirection(Vector3.forward).x,
                                     transform.TransformDirection(Vector3.forward).y,
                                     transform.TransformDirection(Vector3.forward).z);

            castingPos += castingOffset;
            finalTarget = targetLocation * skillStats.range;
            isFirstCast = false;
        }

        transform.position += finalTarget.normalized * skillStats.projectileSpeed * Time.deltaTime;

        Debug.DrawRay(castingPos, finalTarget, Color.blue);

        if (Vector3.Distance(castingPos, transform.position - finalTarget) <= skillStats.range * 0.15)
        {
            Destroy(gameObject);
        }
    }
}
