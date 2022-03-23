using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Character;

public class Hit_Detection : MonoBehaviour
{
    private Character caster;

    private void Start()
    {
        caster = gameObject.GetComponent<Fireball>().baseStats.caster.GetComponent<Character>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (caster.tagManager.isHostile(collision.gameObject.tag) | collision.gameObject.tag == "Wall")
        {
            gameObject.GetComponent<Fireball>().OnHitDetected(collision.gameObject, collision);
        }
    }


}
