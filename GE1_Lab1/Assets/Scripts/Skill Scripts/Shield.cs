using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public SkillVariables baseStats;
    public GameObject shieldPrefab;
    public float timer;

    private SkillVariables currentStats;
    private Animator animator;
    private GameObject activeShield;

    public void SetUp(SkillVariables stats)
    {
        GameObject shieldObject = Instantiate(shieldPrefab, stats.caster.transform.position + new Vector3(0, 2.2f, 0), Quaternion.identity);
        GameObject skillObject = Instantiate(gameObject, stats.caster.transform);
        Shield skill = skillObject.GetComponent<Shield>();
        skill.SetStats(stats);
        skill.SetDuration(stats.duration);
        skill.SetActiveShield(shieldObject);
        Debug.Log("all set");
    }

    public void SetDuration(float duration)
    {
        timer = Time.time + duration;
    }

    public void SetActiveShield(GameObject shield)
    {
        activeShield = shield;
    }

    public void SetStats(SkillVariables stats)
    {
        baseStats = stats;
    }

    private void BreakShield()
    {
        Destroy(activeShield);
        Destroy(gameObject);
    }



    private void Update()
    {
        Debug.Log(timer + " Time: " + Time.time);
        if (Time.time > timer)
        {
            Debug.Log("Exec");
            BreakShield();
        }
    }
}
