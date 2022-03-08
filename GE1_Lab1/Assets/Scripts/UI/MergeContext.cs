using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Charm;

public class MergeContext : MonoBehaviour
{
    public List<GameObject> mergeSlots;
    public GameObject resultSlot;
    public GameObject mergeButton;

    private List<ButtonData> itemsToMerge;
    private ButtonData resultMerge;
    private Character character;
    private void Start()
    {
        character = gameObject.GetComponentInParent<Character>();
    }

    public void CloseContext()
    {
        ClearContext();
        gameObject.SetActive(false);
    }

    public void AddCharmToMerge(ButtonData button)
    {
        if (itemsToMerge == null)
        {
            itemsToMerge = new List<ButtonData>();
        }

        itemsToMerge.Insert(0, button);

        itemsToMerge[0].GetParent().GetComponent<Button>().interactable = false;

        ButtonData mergeSlot0 = new ButtonData(mergeSlots[0], itemsToMerge[0].GetCharm());
        SetSlot(mergeSlot0.GetParent(), mergeSlot0.GetCharm());

        if (itemsToMerge.ElementAtOrDefault(1) != null)
        {
            ButtonData mergeSlot1 = new ButtonData(mergeSlots[1], itemsToMerge[1].GetCharm());
            SetSlot(mergeSlot1.GetParent(), mergeSlot1.GetCharm());
            CheckIfCompatable();
        }

        if (itemsToMerge.Count == 3)
        {
            itemsToMerge[2].GetParent().GetComponent<Button>().interactable = true;
            itemsToMerge.RemoveAt(2);
        }

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
        CharmItem item0 = itemsToMerge[0].GetCharm();
        CharmItem item1 = itemsToMerge[1].GetCharm();


        if (item0.type == item1.type & item0.level == item1.level)
        {
            CharmItem mergetCharm = new CharmItem(item0.level + 1);
            mergetCharm.type = item0.type;
            resultMerge = new ButtonData(resultSlot, mergetCharm);

            SetSlot(resultMerge.GetParent(), resultMerge.GetCharm());

            mergeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            RemoveFromSlot(resultSlot);
            mergeButton.GetComponent<Button>().interactable = false;
        }
    }

    public void Merge()
    {
        foreach(ButtonData item in itemsToMerge)
        {
            character.RemoveCharmFromInventory(item.GetCharm());
        }

        character.AddCharm(resultMerge.GetCharm());

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

        foreach(ButtonData button in itemsToMerge)
        {
            button.GetParent().GetComponent<Button>().interactable = true;
        }

        itemsToMerge.Clear();

    }
}
