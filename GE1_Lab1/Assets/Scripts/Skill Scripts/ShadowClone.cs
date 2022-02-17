using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowClone : MonoBehaviour
{
    public GameObject prefab;
    public SkillVariables baseStats;


    public void SetUp(SkillVariables stats)
    {
        GameObject skill = Instantiate(gameObject, stats.caster.transform);

        skill.GetComponent<ShadowClone>().baseStats = stats;
        skill.GetComponent<ShadowClone>().Spawn();

        Destroy(skill);
    }

    public void Spawn()
    {
        GameObject clone = Instantiate(prefab, baseStats.caster.transform.position, baseStats.caster.transform.rotation);

        clone.GetComponent<Pathfinding>().SetCommander(baseStats.caster);
    }
}
