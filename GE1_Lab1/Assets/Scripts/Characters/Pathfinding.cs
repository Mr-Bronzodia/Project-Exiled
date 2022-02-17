using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    public GameObject target;
    private Character character;
    private NavMeshAgent agent;
    public GameObject commander;
    private NPCSkillManager manager;
    private float range;
    public bool targetInRange = false;
    public List<GameObject> enemies;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        character = gameObject.GetComponent<Character>();
        manager = gameObject.GetComponent<NPCSkillManager>();
    }

    private void StandBy()
    {
        enemies = new List<GameObject>();

        enemies = character.GetNearEnemies();

        if (!(enemies.Count <= 0))
        {
            target = enemies[Random.Range(0, enemies.Count)];
        }
    }

    private void Follow()
    {

        Debug.DrawRay(gameObject.transform.position, commander.transform.position - gameObject.transform.position, Color.red);
        if (Physics.Linecast(gameObject.transform.position, commander.transform.position - gameObject.transform.position))
        {
            agent.stoppingDistance = 0f;
        }
        else
        {
            agent.stoppingDistance = 10;
        }

        if (Vector3.Distance(gameObject.transform.position, commander.transform.position) > 10)
        {
            agent.SetDestination(commander.transform.position);
        }
        else
        {
            List<GameObject> enemies = commander.GetComponent<Character>().GetNearEnemies();

            if (!(enemies.Count <= 0))
            {
                target = enemies[Random.Range(0, enemies.Count)];
            }
        }
    }

    public void SetCommander(GameObject commander)
    {
        this.commander = commander;
    }

    private void Attack()
    {
        if (manager.inventory[0].stats.target != target)
        {
            manager.inventory[0].stats.target = target;
            range = manager.inventory[0].stats.range;
        }

        enemies = character.GetNearEnemies();

        Debug.DrawRay(gameObject.transform.position, target.transform.position - gameObject.transform.position, Color.red);

        if (Physics.Linecast(gameObject.transform.position, target.transform.position - gameObject.transform.position))
        {
            agent.stoppingDistance = 0f;
        }
        else
        {
            agent.stoppingDistance = range;
        }

        if (Vector3.Distance(gameObject.transform.position, target.transform.position) > range)
        {
            if (enemies.Count > 0)
            {
                target = enemies[Random.Range(0, enemies.Count)];
                targetInRange = true;
            }
            else
            {
                agent.SetDestination(target.transform.position);
                targetInRange = false;
            }
        }
        else
        {
            targetInRange = true;
        }
    }

    void Update()
    {
        if (target)
        {
            Attack();
        }
        else
        {
            if (commander)
            {
                Follow();
            }
            else
            {
                StandBy();
            }
            
        }
    }
}
