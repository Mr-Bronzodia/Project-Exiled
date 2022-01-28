using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    private float range = 10f;
    private const float VIEW_OFFSET = 1f;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        Vector3 viewOffset = gameObject.transform.TransformDirection(Vector3.forward) * VIEW_OFFSET;
        Vector3 viewVector = gameObject.transform.position + viewOffset;
        Debug.DrawRay(gameObject.transform.position + viewOffset, target.transform.position - gameObject.transform.position - viewOffset, Color.red);

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
            agent.SetDestination(target.transform.position);
        }


        
    }
}
