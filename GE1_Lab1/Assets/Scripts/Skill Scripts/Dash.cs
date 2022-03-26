using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public SkillVariables baseStats;
    public GameObject smokePrefab;

    public void SetUp(SkillVariables stats)
    {
        GameObject skill = Instantiate(gameObject, stats.caster.transform);

        skill.GetComponent<Dash>().baseStats = stats;
        skill.GetComponent<Dash>().ActivateDash();
    }

  private void ActivateDash()
    {
        RaycastHit ray;
        Physics.Raycast(baseStats.caster.transform.position, baseStats.caster.transform.TransformDirection(Vector3.forward) * baseStats.range, out ray, baseStats.range);

        Instantiate(smokePrefab, baseStats.caster.transform.position, baseStats.caster.transform.rotation);
        CharacterController movmentControler = baseStats.caster.GetComponent<CharacterController>();

        if (ray.collider == null)
        {
            movmentControler.Move(baseStats.caster.transform.TransformDirection(Vector3.forward) * baseStats.range);
            Instantiate(smokePrefab, baseStats.caster.transform.position, baseStats.caster.transform.rotation);
        }
        else
        {
            movmentControler.Move(baseStats.caster.transform.TransformDirection(Vector3.forward) * (ray.distance * 0.9f));
            Instantiate(smokePrefab, baseStats.caster.transform.position, baseStats.caster.transform.rotation);
        }



        Destroy(gameObject);
    }
}
