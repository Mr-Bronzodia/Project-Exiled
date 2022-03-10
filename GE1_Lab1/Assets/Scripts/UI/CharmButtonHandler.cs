using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmButtonHandler : MonoBehaviour
{
    private ButtonData buttonData;
    public GameObject RemoveButton;
    public GameObject MergeButton;
    public GameObject MergeContext;
    public GameObject SkillContextControler;


    private MergeContext context;
    private Character character;
    private CharmInventory inventory;
    private SkilContextControler skillContext;

    private void Start()
    {
        character = gameObject.GetComponentInParent<Character>();
        inventory = gameObject.GetComponentInParent<CharmInventory>();
        context = MergeContext.GetComponent<MergeContext>();
        skillContext = SkillContextControler.GetComponent<SkilContextControler>();
    }

    public void SetData(ButtonData data)
    {
        buttonData = data;
    }

    public void OnClc()
    {
        gameObject.GetComponentInParent<PlayerSkillManager>().AddCharmToActive(skillContext.GetCurrentContext(), buttonData.GetCharm());
        skillContext.RefreshContext();
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
            MergeContext.SetActive(true);
        }

        ButtonData ItemToMerge = new ButtonData(MergeButton, buttonData.GetCharm());
        context.AddCharmToMerge(ItemToMerge);
    }

}
