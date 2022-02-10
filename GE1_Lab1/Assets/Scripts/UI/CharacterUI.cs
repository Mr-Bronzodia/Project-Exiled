using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public GameObject Health;
    public GameObject Mana;


    private float maxMana;
    private float maxHealth;

    private void Start()
    {
        maxHealth = gameObject.GetComponent<Character>().maxHealth;
        maxMana = gameObject.GetComponent<Character>().maxMana;
    }


    public void UpdateHealth(float currentHealth)
    {
        Health.GetComponent<Image>().fillAmount = currentHealth / maxHealth;
    }

    public void UpdateMana(float currentMana)
    {
        Mana.GetComponent<Image>().fillAmount = currentMana / maxMana;
    }
}
