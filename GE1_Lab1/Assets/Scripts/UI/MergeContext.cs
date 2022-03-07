using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Charm;

public class MergeContext : MonoBehaviour
{
    public List<GameObject> mergeSlots;
    public GameObject resultSlot;
    public GameObject mergeButton;

    private List<CharmItem> charms;
    private int nextSlotToUse;
    private CharmItem resultCharm;
    private List<GameObject> buttonsToLock;
    private Character character;



    private void Start()
    {
        charms = new List<CharmItem>();
        buttonsToLock = new List<GameObject>();
        nextSlotToUse = 0;
        character = gameObject.GetComponentInParent<Character>();
    }

    public void OpenContext()
    {
        gameObject.SetActive(true);
    }

    public void CloseContext()
    {
        ClearContext();
        gameObject.SetActive(false);
    }

    public void AddCharmToMerge(CharmItem charm, GameObject buttonToLock)
    {
        buttonsToLock.Insert(0, buttonToLock);
        buttonToLock.GetComponent<Button>().interactable = false;


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

        
        if (charms.Count > 2)
        {
            charms.RemoveAt(2);

            buttonsToLock[2].GetComponent<Button>().interactable = true;
            buttonsToLock.RemoveAt(2);
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
            mergeButton.GetComponent<Button>().interactable = false;
        }
    }

    public void Merge()
    {
        character.RemoveCharmFromInventory(charms[0]);
        character.RemoveCharmFromInventory(charms[1]);
        character.AddCharm(resultCharm);
        ClearContext();
    }

    private void ClearContext()
    {
        RemoveFromSlot(resultSlot);

        foreach (GameObject slot in mergeSlots)
        {
            RemoveFromSlot(slot);
        }

        mergeButton.GetComponent<Button>().interactable = false;

        charms.Clear();

        nextSlotToUse = 0;

        resultCharm = null;

        foreach(GameObject button in buttonsToLock)
        {
            button.GetComponent<Button>().interactable = true;
        }

        buttonsToLock.Clear();
    }
}
