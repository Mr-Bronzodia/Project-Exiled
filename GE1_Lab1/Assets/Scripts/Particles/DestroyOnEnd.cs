using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DestroyOnEnd : MonoBehaviour
{

    VisualEffect visualEffect;
    public float timeToKill;


    private void Start()
    {
        visualEffect = gameObject.GetComponent<VisualEffect>();
        Destroy(gameObject, timeToKill);
    }

}
