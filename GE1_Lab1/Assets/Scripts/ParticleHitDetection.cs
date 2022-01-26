using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHitDetection : MonoBehaviour
{

    private void OnParticleCollision(GameObject other)
    {

        Debug.Log(gameObject.name);
    }
}
