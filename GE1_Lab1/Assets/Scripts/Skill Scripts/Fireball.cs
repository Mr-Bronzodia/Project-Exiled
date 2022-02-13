using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public SkillVariables baseStats;

    private Vector3 castingPos;
    private bool isFirstCast = true;
    private Vector3 targetLocation;
    private const int OFFSET = 2;
    private float totalSpread;
    private Vector3 finalTarget;
    private int currentBounce = 0;



    public void SetUp(SkillVariables stats)
    {
        stats.caster.gameObject.GetComponent<Character>().UseMana(stats.manaCost);

        Vector3 castingOffset = stats.caster.transform.TransformDirection(Vector3.forward) * OFFSET;

        totalSpread = stats.spread * stats.numberOfProjectiles;

        if (stats.numberOfProjectiles > 1)
        {
            for (int i = 1; i <= stats.numberOfProjectiles; i++)
            {
                GameObject skill = Instantiate(gameObject, stats.caster.transform.position + castingOffset, stats.caster.transform.rotation);
                skill.GetComponent<Fireball>().SetStats(stats);

                skill.transform.rotation *= Quaternion.Euler(0, -(totalSpread / 2) + (stats.spread * i), 0);
            }
        }
        else if (stats.numberOfProjectiles == 1)
        {
            GameObject skill = Instantiate(gameObject, stats.caster.transform.position + castingOffset, stats.caster.transform.rotation);
            skill.GetComponent<Fireball>().SetStats(stats);
        }

    }

    public void SetUpAutoTarget(SkillVariables stats, Vector3 castPos)
    {
        stats.caster.gameObject.GetComponent<Character>().UseMana(stats.manaCost);

        Vector3 castingOffset = Vector3.ClampMagnitude(stats.target.transform.position - castingPos, OFFSET);

        GameObject skill = Instantiate(gameObject, castPos + castingOffset, Quaternion.identity);
        skill.GetComponent<Fireball>().SetStats(stats);
    }

    public void SetStats(SkillVariables stats)
    {
        this.baseStats = stats;
    }

    public SkillVariables GetStats()
    {
        return baseStats;
    }

    public void OnHitDetected(GameObject other, List<ParticleCollisionEvent> collisionEvents)
    { 
        if (other.tag != baseStats.caster.tag & other.tag != "Wall")
        {
            if (baseStats.isAutoTargeted)
            {
                if (other == baseStats.target)
                {
                    Impact(other);
                } 
            }
            else
            {
                Impact(other);
            }
        }
        else if (other.tag == "Wall")
        {
            if (currentBounce < baseStats.totalBounces)
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
        enemy.GetComponent<Character>().ApplyDamage(baseStats.damage);

        if (baseStats.totalChains > 0)
        {
            Chain(enemy);
        }

        Destroy(gameObject);
    }

    private void Chain(GameObject initialEnemy)
    {
        List<GameObject> chainTargests = new List<GameObject>();

        foreach (GameObject character in initialEnemy.GetComponent<Character>().GetNearCharacters())
        {
            if (character.tag == initialEnemy.tag)
            {
                chainTargests.Add(character);
            }
        }

        for (int i = 0; i <= baseStats.totalChains - 1; i++)
        {

            if (i < chainTargests.Count)
            {
                SkillVariables clone = baseStats.Clone();

                clone.target = chainTargests[i];
                clone.totalChains = 0;
                clone.numberOfProjectiles = 1;
                clone.totalBounces = 0;
                clone.isAutoTargeted = true;

                SetUpAutoTarget(clone, initialEnemy.transform.position);
            }
        }
    }


    private void Bounce(ParticleCollisionEvent collisionEvent)
    {
        Vector3 newTarget = Vector3.Reflect(finalTarget, collisionEvent.normal);
        newTarget = Vector3.ClampMagnitude(newTarget, baseStats.range);
        transform.rotation = Quaternion.LookRotation(newTarget);
        transform.position += Vector3.ClampMagnitude(newTarget, 2);
        finalTarget = newTarget;
        castingPos = collisionEvent.intersection;
        currentBounce++;
    }


    void Update()
    {
        if (!baseStats.isAutoTargeted)
        {
            if (isFirstCast)
            {
                castingPos = new Vector3(baseStats.caster.transform.position.x,
                                         baseStats.caster.transform.position.y,
                                         baseStats.caster.transform.position.z);

                Vector3 castingOffset = baseStats.caster.transform.TransformDirection(Vector3.forward) * OFFSET;

                targetLocation = new Vector3(transform.TransformDirection(Vector3.forward).x,
                                         transform.TransformDirection(Vector3.forward).y,
                                         transform.TransformDirection(Vector3.forward).z);

                castingPos += castingOffset;
                finalTarget = targetLocation * baseStats.range;
                isFirstCast = false;
            }

            transform.position += finalTarget.normalized * baseStats.projectileSpeed * Time.deltaTime;

            Debug.DrawRay(castingPos, finalTarget, Color.blue);

            if (Vector3.Distance(castingPos, transform.position - finalTarget) <= baseStats.range * 0.15)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (isFirstCast)
            {
                transform.rotation = Quaternion.LookRotation(baseStats.target.transform.position - gameObject.transform.position);
                isFirstCast = false;
            }
            transform.position = Vector3.MoveTowards(gameObject.transform.position, baseStats.target.transform.position, baseStats.projectileSpeed * Time.deltaTime);
        }
    }
}
