using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public SkillVariables baseStats;

    public void SetUp(SkillVariables stats)
    {
        GameObject skill = Instantiate(gameObject, stats.caster.transform);

        skill.GetComponent<Dash>().baseStats = stats;
        skill.GetComponent<Dash>().ActivateDash();

        Destroy(skill);
    }

  private void ActivateDash()
    {
        RaycastHit ray;
        Physics.Raycast(baseStats.caster.transform.position, baseStats.caster.transform.TransformDirection(Vector3.forward) * baseStats.range, out ray, baseStats.range);

        if (ray.collider == null)
        {
            baseStats.caster.transform.position += baseStats.caster.transform.TransformDirection(Vector3.forward) * baseStats.range;
        }
        else
        {
            baseStats.caster.transform.position += baseStats.caster.transform.TransformDirection(Vector3.forward) * (ray.distance * 0.9f);
        }
        
    }
}
