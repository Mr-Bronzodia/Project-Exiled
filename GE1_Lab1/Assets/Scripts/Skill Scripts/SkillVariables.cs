using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillVariables : MonoBehaviour
{

    public int range;
    public GameObject caster;
    public GameObject target;
    public float projectileSpeed;
    public float damage;
    public float cooldown;
    public int numberOfProjectiles;
    public float spread;
    public int totalBounces;
    public int totalChains;
    public bool isAutoTargeted;
    public float manaCost;

    public void Clone(SkillVariables clone)
    {
        clone.range = range;
        clone.caster = caster;
        clone.target = target;
        clone.projectileSpeed = projectileSpeed;
        clone.damage = damage;
        clone.cooldown = cooldown;
        clone.numberOfProjectiles = numberOfProjectiles;
        clone.spread = spread;
        clone.totalBounces = totalBounces;
        clone.totalChains = totalChains;
        clone.isAutoTargeted = isAutoTargeted;
    }
}
