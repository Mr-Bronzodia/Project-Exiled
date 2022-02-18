using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public SkillVariables baseStats;

    private bool isFirstCast = true;
    public float timer = 0f;


    public void SetUp(SkillVariables stats)
    {
        GameObject skill = Instantiate(gameObject, stats.caster.transform.position, stats.caster.transform.rotation);

        skill.GetComponent<Counter>().baseStats = stats;
    }

    private void Disable()
    {
        baseStats.caster.GetComponent<Character>().isCountering = false;
        Destroy(gameObject);
    }

    private void Update()
    {
        if (isFirstCast)
        {
            baseStats.caster.GetComponent<Character>().isCountering = true;
            timer = Time.time + baseStats.duration;
            isFirstCast = false;
        }

        if (timer < Time.time)
        {
            Disable();
        }
    }

}
