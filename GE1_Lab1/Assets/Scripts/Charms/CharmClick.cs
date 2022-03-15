using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmClick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider | other is CharacterController)
        {
            gameObject.GetComponentInParent<Charm>().PickUp(other.gameObject);
        }

    }
}
