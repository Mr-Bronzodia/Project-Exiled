using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipHandler : MonoBehaviour
{
    public GameObject TitleObject;
    public GameObject LevelObject;
    public GameObject IconObject;
    public GameObject DescriptionObject;

    public void SetTitle(string title)
    {
        TitleObject.GetComponent<TMP_Text>().text = title;
    }

    public void SetLevel(int level)
    {
        LevelObject.GetComponent<TMP_Text>().text = "Level: " + level;
    }

    public void SetIcon(Sprite icon)
    {
        IconObject.GetComponent<Image>().sprite = icon;
    }

    public void SetDescription(string description)
    {
        DescriptionObject.GetComponent<TMP_Text>().text = description;
    }


}
