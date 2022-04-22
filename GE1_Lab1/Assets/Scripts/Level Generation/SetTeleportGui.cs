using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTeleportGui : MonoBehaviour
{
    public GameObject gui;

    public void SetGui()
    {
        switch (gui.activeInHierarchy)
        {
            case true:
                gui.SetActive(false);
                break;
            case false:
                gui.SetActive(true);
                break;
        }
    }
}
