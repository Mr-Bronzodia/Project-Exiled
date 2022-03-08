using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject toolTipTemplate;

    private ToolTipHandler toolTip;
    private ButtonData data;

    private void Start()
    {
        toolTip = toolTipTemplate.GetComponent<ToolTipHandler>();
        Debug.Log("Test");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (data != null)
        {
            Debug.Log("Hovered over " + data.GetParent() + " with charm: " + data.GetCharm().type);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (data != null)
        {
            Debug.Log("Exited over " + data.GetParent());
        }    
    }

    public void SetData(ButtonData data)
    {
        this.data = data;
    }
}
