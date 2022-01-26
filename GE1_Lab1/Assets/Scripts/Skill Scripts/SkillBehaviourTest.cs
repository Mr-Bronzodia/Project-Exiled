using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBehaviourTest : MonoBehaviour
{
    private Vector3 target;
    private Vector3 shootPosition;
    private float range;

    public void Setup(Vector3 target, float range)
    {
        this.target = target;
        this.range = range;
        shootPosition = transform.position;
    }

    public void Update()
    {

        if (target != null)
        {
            transform.position += target.normalized * 10 * Time.deltaTime;
            Debug.DrawRay(shootPosition, target * range, Color.blue);

            if (Vector3.Distance(shootPosition, transform.position - target * range) <= 0.1)
            {
                Destroy(gameObject);
            }
        }
    }

}
