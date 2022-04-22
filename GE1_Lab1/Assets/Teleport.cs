using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject DropOff;

    private void OnTriggerEnter(Collider other)
    {
        if (other is CharacterController)
        {
            other.gameObject.transform.position = DropOff.transform.position;
        }
    }
}
