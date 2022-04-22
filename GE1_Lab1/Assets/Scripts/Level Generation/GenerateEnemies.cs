using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GenerateEnemies : MonoBehaviour
{
    private float monsterDensity;
    private Slider densitySlider;
    private TMP_Text levelObject;
    private int monsterLevel;

    public GameObject levelHandleObject;
    public GameObject Teleport;
    

    private void Start()
    {
        densitySlider = gameObject.GetComponentInChildren<Slider>();
        levelObject = gameObject.GetComponentsInChildren<TMP_Text>()[3];
    }

    public void PrepareMap()
    {
        TeleportColider teleport = Teleport.GetComponent<TeleportColider>();

        monsterDensity = densitySlider.value;
        monsterLevel = Int32.Parse(levelObject.text);

        teleport.GenerateAndOpen(monsterLevel, monsterDensity);
    }

    public void AddLevel(int num)
    {
        monsterLevel = Int32.Parse(levelObject.text);

        if ((monsterLevel + num) <= 0)
        {
            monsterLevel = 0;
        }
        else
        {
            monsterLevel += num;
        }
        
        levelObject.text = Convert.ToString(monsterLevel);
    }

    public void onSliderUpdate()
    {
        levelHandleObject.GetComponent<TMP_Text>().text = String.Format("{0:0.0}", densitySlider.value);
    }
}
