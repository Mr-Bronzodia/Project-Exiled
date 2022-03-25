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
    private GameObject caster;
    private CharacterController casterController;

    public void SetUp(SkillVariables stats)
    {
        GameObject shieldObject = Instantiate(shieldPrefab, stats.caster.transform.position + new Vector3(0, 2.2f, 0), Quaternion.identity);
        shieldObject.GetComponent<ShieldBehaviour>().SetMaxHits(stats.quantityMultiplier);
        GameObject skillObject = Instantiate(gameObject, stats.caster.transform);
        Shield skill = skillObject.GetComponent<Shield>();
        skill.SetStats(stats);
        skill.SetDuration(stats.duration);
        skill.SetActiveShield(shieldObject);
        skill.SetCaster(stats.caster);
    }

    public void SetDuration(float duration)
    {
        timer = Time.time + duration;
    }

    public void SetCaster(GameObject caster)
    {
        this.caster = caster;
        casterController = caster.GetComponent<CharacterController>();
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
        if (Time.time > timer)
        {
            BreakShield();
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0) 
        {
            BreakShield();
        }

    }
}
