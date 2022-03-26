using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowClone : MonoBehaviour
{
    public GameObject prefab;
    public SkillVariables baseStats;
    public GameObject lightningPrefabl;
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
            Vector3 spawnPosition = new Vector3(baseStats.caster.transform.position.x + Random.Range(-10, 10), baseStats.caster.transform.position.y, baseStats.caster.transform.position.z + Random.Range(-10, 10));

            Instantiate(lightningPrefabl, spawnPosition + new Vector3(0, 14, 0), Quaternion.identity);
            GameObject clone = Instantiate(prefab, spawnPosition, baseStats.caster.transform.rotation);
            clone.GetComponent<Pathfinding>().SetCommander(baseStats.caster, i);

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
