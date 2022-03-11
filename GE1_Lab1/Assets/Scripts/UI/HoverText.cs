using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Charm;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject toolTipTemplate;

    private ToolTipHandler toolTip;
    private ButtonData data;
    private RectTransform tooltipTransform;
    private const int TooltipOffset = 10;

    private void Start()
    {
        toolTip = toolTipTemplate.GetComponent<ToolTipHandler>();
        tooltipTransform = toolTipTemplate.GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {  
        if (data != null & data.GetCharm() != null)
        {
            CharmItem charm = data.GetCharm();
            toolTipTemplate.SetActive(true);

            toolTipTemplate.transform.position = new Vector3(data.GetParent().transform.position.x, data.GetParent().transform.position.y - (tooltipTransform.rect.height / 2 + TooltipOffset));

            toolTip.SetTitle("CharmOf" + charm.type, charm.level);
            toolTip.SetLevel(charm.level);
            toolTip.SetIcon(charm.GetIcon());
            toolTip.SetDescription(charm.GetDescription());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (data != null)
        {
            toolTipTemplate.SetActive(false);
        }    
    }

    public void SetData(ButtonData data)
    {
        this.data = data;
    }
}
