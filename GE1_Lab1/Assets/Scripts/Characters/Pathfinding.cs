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
    private Animator animator;
    public float range;
    public bool targetInRange = false;
    public List<GameObject> enemies;
    public LayerMask groundLayer;

    private Vector3 lastPosition;
    private int navPriority;
    private float movementMagnitude;



    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        character = gameObject.GetComponent<Character>();
        manager = gameObject.GetComponent<NPCSkillManager>();
        animator = gameObject.GetComponentInChildren<Animator>();
        lastPosition = gameObject.transform.position;
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
        range = manager.inventory[0].stats.range;
        if (Physics.Linecast(gameObject.transform.position, commander.transform.position - gameObject.transform.position, groundLayer))
        {
            agent.stoppingDistance = 0.5f;
        }
        else
        {
            agent.stoppingDistance = range;
        }

        float distanceToCommander = Vector3.Distance(gameObject.transform.position, commander.transform.position);

        if (distanceToCommander > range)
        {
            agent.isStopped = false;

            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        Random.InitState((int)System.DateTime.Now.Ticks);
                        agent.SetDestination(new Vector3(commander.transform.position.x + Random.Range(-range, range), commander.transform.position.y, commander.transform.position.z + Random.Range(-range, range)));
                        agent.avoidancePriority = navPriority;
                        
                    }
                }
            }
        }
        else
        {
            agent.isStopped = true;
            agent.avoidancePriority = 99;


            List<GameObject> enemies = commander.GetComponent<Character>().GetNearEnemies();

            if (!(enemies.Count <= 0))
            {
                target = enemies[Random.Range(0, enemies.Count)];
            }
        }
    }

    public void SetCommander(GameObject commander, int priority)
    {
        this.commander = commander;
        navPriority = priority;
    }

    private void Attack()
    {
        if (manager.inventory[0].stats.target != target)
        {
            manager.inventory[0].stats.target = target;
            range = (float)manager.inventory[0].stats.range * 0.9f;
        }

        enemies = character.GetNearEnemies();

        Debug.DrawRay(gameObject.transform.position, target.transform.position - gameObject.transform.position, Color.red);

        if (Physics.Linecast(gameObject.transform.position, target.transform.position - gameObject.transform.position, out var hit,1 << groundLayer))
        {
            Debug.Log(hit.collider.name);
            agent.stoppingDistance = 2f;
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
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
                targetInRange = false;
            }
        }
        else
        {
            targetInRange = true;
        }
    }

    private void CalculateAnimation(Vector3 lastPosition)
    {
        float velocirtZ = Vector3.Dot(lastPosition.normalized, transform.forward);
        float velocirtX = Vector3.Dot(lastPosition.normalized, transform.right);


        if (velocirtZ < 1f | velocirtX < 1f)
        {
            animator.SetFloat("VelocityZ", velocirtZ, 0.1f, Time.deltaTime);
            animator.SetFloat("VelocityX", velocirtX, 0.1f, Time.deltaTime);
        }


        AnimationClip currentClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;

        if (currentClip.name == "Fast Run" | currentClip.name == "Idle" | currentClip.name == "Left Strafe" | currentClip.name == "Right Strafe" | currentClip.name == "Running Backward")
        {

            if (movementMagnitude > 0.05f)
            {
                float speedMultiplier = (movementMagnitude * Time.deltaTime) / (currentClip.length * currentClip.frameRate);
                animator.SetFloat("Speed Multiplier", speedMultiplier >= 0.05f ? speedMultiplier : 1f, 0.1f, Time.deltaTime);
            }

        }

    }

    void Update()
    {
        agent.speed = character.speed;
        Vector3 currentPosition = gameObject.transform.position;
        Vector3 moveDirection = currentPosition - lastPosition;
        movementMagnitude = moveDirection.magnitude;
        CalculateAnimation(moveDirection);
        lastPosition = currentPosition;


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
