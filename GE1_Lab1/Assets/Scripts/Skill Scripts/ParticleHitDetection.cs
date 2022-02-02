using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHitDetection : MonoBehaviour
{

    private void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        GetComponent<ParticleSystem>().GetCollisionEvents(other, collisionEvents);
        gameObject.transform.parent.gameObject.GetComponent<FireballBehaviour>().OnHitDetected(other, collisionEvents);
    }
}
