using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Character;

public class Hit_Detection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (TagManager.isCharacter(collision.gameObject.tag) | collision.gameObject.tag == "Wall")
        {
            gameObject.GetComponent<Fireball>().OnHitDetected(collision.gameObject, collision);
        }
    }


}
