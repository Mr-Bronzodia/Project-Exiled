using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Charm;
using static PlayerSkillManager;

public class SkilContextControler : MonoBehaviour
{
    public List<GameObject> SkillButtons;
    public GameObject SkillPanel;

    public Dictionary<InventoryManager, List<CharmItem>> appliedCharms;
    private List<GameObject> SkillPanelButtons;
    private GameObject SkillPanelIcon;
    private InventoryManager currentContext;

    private void Start()
    {
        appliedCharms = gameObject.GetComponentInParent<PlayerSkillManager>().GetAppliedCharms();

        for (int i = 0; i < appliedCharms.Count; i++)
        {
            Image skillIcon = SkillButtons[i].GetComponentsInChildren<Image>()[1];

            skillIcon.enabled = true;
            skillIcon.sprite = appliedCharms.ElementAt(i).Key.GetIcon();

            ContextButtonData newContext = new ContextButtonData(appliedCharms.ElementAt(i).Key, appliedCharms.ElementAt(i).Value);
            SkillButtons[i].GetComponent<ContextButton>().SetData(newContext);
        }

        SkillContexPanel skillContex = SkillPanel.GetComponent<SkillContexPanel>();
        SkillPanelButtons = skillContex.ActiveSlotObject;
        SkillPanelIcon = skillContex.SkillIconObject;

        SetContext(SkillButtons[0].GetComponent<ContextButton>().GetData());
    }

    public InventoryManager GetCurrentContext()
    {
        return currentContext;
    }

    public void SetContext(ContextButtonData data)
    {
        currentContext = data.skill;
        Image SkillContextIcon = SkillPanelIcon.GetComponentsInChildren<Image>()[1];

        SkillContextIcon.enabled = true;
        SkillContextIcon.sprite = data.skill.GetIcon();

        RefreshContext();
    }

    public void RefreshContext()
    {
        for (int i = 0; i < SkillPanelButtons.Count; i++)
        {
            if (appliedCharms[GetCurrentContext()].ElementAtOrDefault(i) != null)
            {
                SkillPanelButtons[i].GetComponentsInChildren<Image>()[1].enabled = true;
                SkillPanelButtons[i].GetComponentsInChildren<Image>()[1].sprite = appliedCharms[GetCurrentContext()][i].GetIcon();
                ButtonData SkillContextButtonToolTip = new ButtonData(SkillPanelButtons[i], appliedCharms[GetCurrentContext()][i]);
                SkillPanelButtons[i].GetComponent<RemoveButtonControler>().SetData(SkillContextButtonToolTip);
            }
            else
            {
                SkillPanelButtons[i].GetComponentsInChildren<Image>()[1].sprite = null;
                SkillPanelButtons[i].GetComponentsInChildren<Image>()[1].enabled = false;
                ButtonData SkillContextButtonToolTip = new ButtonData(SkillPanelButtons[i], null);
                SkillPanelButtons[i].GetComponent<RemoveButtonControler>().SetData(SkillContextButtonToolTip);
            }
        }
    }


    public class ContextButtonData
    {
        public InventoryManager skill;
        public List<CharmItem> appliedCharm;

        public ContextButtonData(InventoryManager skill, List<CharmItem> appliedCharm)
        {
            this.skill = skill;
            this.appliedCharm = appliedCharm;
        }
    }
}
