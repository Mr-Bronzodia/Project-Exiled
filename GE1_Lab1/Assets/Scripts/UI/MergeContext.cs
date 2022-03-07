using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Charm;

public class MergeContext : MonoBehaviour
{
    public List<CharmItem> charms;
    public List<GameObject> mergeSlots;
    public GameObject resultSlot;
    public GameObject mergeButton;

    private int nextSlotToUse;
    public CharmItem resultCharm;

    private void Start()
    {
        charms = new List<CharmItem>();
        nextSlotToUse = 0;
    }

    public void OpenContext()
    {
        gameObject.SetActive(true);
    }

    public void CloseContext()
    {
        gameObject.SetActive(false);
    }

    public void AddCharmToMerge(CharmItem charm)
    {
        if (charms.Count >= 2)
        {
            charms.RemoveAt(1);
        }

        if (nextSlotToUse == 0)
        {
            SetSlot(mergeSlots[nextSlotToUse], charm);

            charms.Insert(nextSlotToUse, charm);

            nextSlotToUse = 1;
        }
        else if (nextSlotToUse == 1)
        {
            SetSlot(mergeSlots[nextSlotToUse], charm);

            charms.Insert(nextSlotToUse, charm);

            nextSlotToUse = 0;
        }

        if (charms.Count == 2)
        {
            CheckIfCompatable();
        }

        Debug.Log("Added: " + charm.type);
    }

    private void SetSlot(GameObject slot, CharmItem charm)
    {
        Image slotImage = slot.GetComponentsInChildren<Image>()[1];

        slotImage.enabled = true;
        slotImage.sprite = charm.GetIcon();

        slot.GetComponentInChildren<Button>().enabled = true;
    }

    private void RemoveFromSlot(GameObject slot)
    {
        Image slotImage = slot.GetComponentsInChildren<Image>()[1];

        slotImage.sprite = null;
        slotImage.enabled = false;
        

        slot.GetComponentInChildren<Button>().enabled = false;
    }

    private void CheckIfCompatable()
    {
        if (charms[0].type == charms[1].type & charms[0].level == charms[1].level)
        {
            resultCharm = new CharmItem(charms[0].level + 1);
            resultCharm.type = charms[0].type;

            SetSlot(resultSlot, resultCharm);

            mergeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            resultCharm = null;
            RemoveFromSlot(resultSlot);
            mergeButton.GetComponent<Button>().interactable = true;
        }
    }
}
