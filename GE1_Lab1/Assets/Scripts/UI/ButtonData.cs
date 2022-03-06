
using System;
using UnityEngine;
using static Charm;

[Serializable]
public class ButtonData
{
    private GameObject parent;
    private Sprite icon;
    private CharmItem charm;

    public ButtonData(GameObject parent, CharmItem charm)
    {
        this.parent = parent;
        this.charm = charm;
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
