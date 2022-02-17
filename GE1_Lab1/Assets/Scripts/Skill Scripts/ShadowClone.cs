using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowClone : MonoBehaviour
{
    public GameObject prefab;
    public SkillVariables baseStats;
    private bool isFirstUpdate = true;

    private List<GameObject> clones;

    public void SetUp(SkillVariables stats)
    {
        GameObject skill = Instantiate(gameObject, stats.caster.transform);

        skill.GetComponent<ShadowClone>().baseStats = stats;
        skill.GetComponent<ShadowClone>().Spawn();

    }

    public void Spawn()
    {
        clones = new List<GameObject>();

        for (int i = 0; i < baseStats.quantityMultiplier; i++)
        {
            GameObject clone = Instantiate(prefab, baseStats.caster.transform.position, baseStats.caster.transform.rotation);
            clone.GetComponent<Pathfinding>().SetCommander(baseStats.caster);

            clones.Add(clone);
        }
    }

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            foreach (GameObject clone in clones)
            {
                clone.GetComponent<NPCSkillManager>().inventory[0].stats = baseStats.caster.GetComponent<PlayerSkillManager>().inventory[0].stats.Clone();
            }

            Destroy(gameObject);
        }
    }

}
