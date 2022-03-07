using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmButtonHandler : MonoBehaviour
{
    private ButtonData buttonData;
    public GameObject RemoveButton;
    public GameObject MergeButton;

    public GameObject MergeContext;


    private MergeContext context;
    private Character character;
    private CharmInventory inventory;

    private void Start()
    {
        character = gameObject.GetComponentInParent<Character>();
        inventory = gameObject.GetComponentInParent<CharmInventory>();
        context = MergeContext.GetComponent<MergeContext>();
    }

    public void SetData(ButtonData data)
    {
        buttonData = data;
    }

    public void OnClc()
    {
        Debug.Log(buttonData.GetParent().name);
        Debug.Log(buttonData.GetCharm().type);
        Debug.Log(buttonData.GetCharm().level);
    }

    public void SetButtons(bool state)
    {
        RemoveButton.SetActive(state);
        MergeButton.SetActive(state);
    }

    public void RemoveCharm()
    {
        character.RemoveCharmFromInventory(buttonData.GetCharm());

        inventory.Refresh();
    }

    public void AddToMergeContext()
    {
        if (!MergeContext.activeSelf)
        {
            context.OpenContext();
        }

        context.AddCharmToMerge(buttonData.GetCharm(), MergeButton);
    }

}
