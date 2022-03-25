using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    public GameObject impactPrefab;
    private int maxHits;
    private int currentHits = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (currentHits >= maxHits)
        {
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.name.Contains("Fireball"))
        {
            Destroy(other.gameObject);
            GameObject impact = Instantiate(impactPrefab, other.gameObject.transform.position + (other.gameObject.transform.TransformDirection(Vector3.forward) * 0.1f), other.gameObject.transform.rotation * Quaternion.Euler(0, 180, 0));
            currentHits++;
        }
    }


    public void SetMaxHits(int hits)
    {
        maxHits = hits;
    }
}
