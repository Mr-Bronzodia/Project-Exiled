using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Charm;

public class MergeContext : MonoBehaviour
{
    private List<CharmItem> charms;
    public List<GameObject> mergeSlots;
    public GameObject resultSlot;

    private void Start()
    {
        charms = new List<CharmItem>(new CharmItem[2]);
    }

    public void CloseContext()
    {
        gameObject.SetActive(false);
    }

    public void AddCharmToMerge(CharmItem charm)
    {
        charms.Add(charm);
    }
}
