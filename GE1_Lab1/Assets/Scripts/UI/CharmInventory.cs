using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Charm;

public class CharmInventory : MonoBehaviour
{
    public List<GameObject> itemSlots;
    private Character character;

    private void Start()
    {
        character = gameObject.GetComponent<Character>();
    }

    public void Refresh()
    {
        List<CharmItem> charms = character.GetCharms();

        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (charms.ElementAtOrDefault(i) != null)
            {
                AddElement(itemSlots[i], charms[i]);
            }
            else
            {
                RemoveElement(itemSlots[i]);
            }
        }
    }

    private void AddElement(GameObject slot, CharmItem charm) 
    {
        Image slotImage = slot.GetComponentsInChildren<Image>()[1];

        slotImage.enabled = true;
        slotImage.sprite = charm.GetIcon();

        ButtonData buttonData = new ButtonData(slot, charm);

        slot.GetComponentInChildren<Button>().enabled = true;

        CharmButtonHandler buttonHandler = slot.GetComponentInChildren<CharmButtonHandler>();

        buttonHandler.SetData(buttonData);
        buttonHandler.SetButtons(true);
    }

    private void RemoveElement(GameObject slot)
    {
        Image slotImage = slot.GetComponentsInChildren<Image>()[1];

        slotImage.sprite = null;
        slotImage.enabled = false;

        slot.GetComponentInChildren<Button>().enabled = false;

        slot.GetComponentInChildren<CharmButtonHandler>().SetButtons(false);
    }

}
