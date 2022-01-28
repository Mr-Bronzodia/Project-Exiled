using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject mainCamera;

    void Update()
    {
        gameObject.transform.LookAt(mainCamera.transform);
    }
}
