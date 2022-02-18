using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmClick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        gameObject.GetComponentInParent<Charm>().PickUp(other.gameObject);
    }
}
