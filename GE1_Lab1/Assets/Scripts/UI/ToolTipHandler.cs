using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ToolTipHandler : MonoBehaviour
{
    public GameObject TitleObject;
    public GameObject LevelObject;
    public GameObject IconObject;
    public GameObject DescriptionObject;

    private static Dictionary<int, string> tierColour = new Dictionary<int, string>()
    {
        {1, "#d9d9d2" },
        {2, "#5CFF5C" },
        {3, "#5C5CFF" },
        {4, "#880ED4" },
        {5, "#FFFF2E" },
        {6, "#FFA500" },
        {7, "#A36A00" },
    };


    public void SetTitle(string title, int level)
    {
        Regex r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);


        string readyTitle = r.Replace(title, " ");

        if (level >= 7)
        {
            TitleObject.GetComponent<TMP_Text>().text = String.Format("<color={0}>", tierColour[7]) + readyTitle + " </color>";
        }
        else
        {
            TitleObject.GetComponent<TMP_Text>().text = String.Format("<color={0}>", tierColour[level]) + readyTitle + " </color>";
        }
        
    }

    public void SetLevel(int level)
    {
        LevelObject.GetComponent<TMP_Text>().text = "<color=#323330>Level: " + level + "</color>";
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
