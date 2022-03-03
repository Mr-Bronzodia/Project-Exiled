using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Charm;

public class CharmInventory : MonoBehaviour
{
    public List<GameObject> itemSlots;

    public void Open()
    {
        List<CharmItem> charms = gameObject.GetComponent<Character>().GetCharms();

        for (int i = 0; i < charms.Count; i++)
        {
            itemSlots[i].GetComponentsInChildren<Image>()[1].enabled = true;
            itemSlots[i].GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("CharmPlaceholder");
        }
    }

}
