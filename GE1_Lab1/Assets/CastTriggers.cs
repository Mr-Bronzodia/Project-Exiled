using System;
using UnityEngine;
using static Character;
using static PlayerSkillManager;

public class CastTriggers : MonoBehaviour
{
    private InventoryManager skill;
    private Character character;

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
            skill.ActiveAbility();
        }
    }
    public void SetSkill(InventoryManager skill)
    {
        this.skill = skill;
    }
}
