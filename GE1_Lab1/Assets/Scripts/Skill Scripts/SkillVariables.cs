using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillVariables
{

    public int range;
    public GameObject caster;
    public GameObject target;
    public float speed;
    public float damage;
    public float cooldown;
    public int quantityMultiplier;
    public float spread;
    public int totalBounces;
    public int totalChains;
    public bool isAutoTargeted;
    public float manaCost;
    public float duration;

    public SkillVariables Clone()
    {
        SkillVariables clone = new SkillVariables();

        clone.range = range;
        clone.caster = caster;
        clone.target = target;
        clone.speed = speed;
        clone.damage = damage;
        clone.cooldown = cooldown;
        clone.quantityMultiplier = quantityMultiplier;
        clone.spread = spread;
        clone.totalBounces = totalBounces;
        clone.totalChains = totalChains;
        clone.isAutoTargeted = isAutoTargeted;
        clone.manaCost = manaCost;
        clone.duration = duration;

        return clone;
    }
}
