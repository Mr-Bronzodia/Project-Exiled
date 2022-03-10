using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SkilContextControler;

public class ContextButton : MonoBehaviour
{
    private ContextButtonData contextButtonData;

    public void SetData(ContextButtonData contextButtonData)
    {
        this.contextButtonData = contextButtonData;
    }

    public ContextButtonData GetData()
    {
        return contextButtonData;
    }

    public void Onclick()
    {
        gameObject.GetComponentInParent<SkilContextControler>().SetContext(contextButtonData);
    }
}
