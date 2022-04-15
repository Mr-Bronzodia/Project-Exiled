using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToSpawn : MonoBehaviour
{
    public GameObject TeleportLocation;


    private void OnTriggerEnter(Collider other)
    {
        if (other is CharacterController)
        {
            other.gameObject.transform.position = TeleportLocation.transform.position;
        }
    }
}
