using System;
using UnityEngine;
using static Character;
using static PlayerSkillManager;

public class CastTriggers : MonoBehaviour
{
    private InventoryManager skill;
    private Character character;
    private Animator animator;
    private const float LonsgestAnimLenght = 2.7f; // Hardcoded

    private void Start()
    {
        character = gameObject.GetComponentInParent<Character>();
    }

    public void ExecuteAction()
    {
        if (TagManager.isNPC(skill.stats.caster.tag))
        {
            skill.stats.caster.transform.LookAt(skill.stats.target.transform);
            skill.ActiveAbility();
        }
        else if (TagManager.isCharacter(skill.stats.caster.tag))
        {
            if (skill.skill.name == "Counter" & !character.isCountering)
            {
                skill.ActiveAbility();
            }
            else if (skill.skill.name == "Counter" & character.counterProjectileCount == 0)
            {
                animator.SetTrigger("Break Counter");

            }
            else if (!(skill.skill.name == "Counter"))
            {
                float castspeedModifier = skill.stats.cooldown < LonsgestAnimLenght ? LonsgestAnimLenght / skill.stats.cooldown : 1f;
                animator.SetFloat("Cast Speed Multiplier", castspeedModifier);
                skill.ActiveAbility();
            }
            
        }
    }

    public void Die()
    {
        character.Die();
    }

    public void SetSkill(InventoryManager skill)
    {
        this.skill = skill;
        animator = skill.stats.caster.GetComponentInChildren<Animator>();
    }
}
