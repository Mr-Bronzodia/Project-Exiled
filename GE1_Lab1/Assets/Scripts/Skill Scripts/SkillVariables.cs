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
}
