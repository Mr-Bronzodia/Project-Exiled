
using System;
using UnityEngine;
using static Charm;

[Serializable]
public class ButtonData
{
    private GameObject parent;
    private CharmItem charm;

    public ButtonData(GameObject parent, CharmItem charm)
    {
        this.parent = parent;
        this.charm = charm;

        if (parent.GetComponent<HoverText>() != null)
        {
            SetHoverTextData(parent);
        }
    }

    private void SetHoverTextData(GameObject parent)
    {
        parent.GetComponent<HoverText>().SetData(this);
    }

    public GameObject GetParent()
    {
        return parent;
    }

    public CharmItem GetCharm()
    {
        return charm;
    }
}
