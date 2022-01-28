using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public GameObject UI;

    void Start()
    {
        currentHealth = maxHealth;
        UI.SetActive(false);
    }

    private void UpdateUI()
    {
        if (gameObject.tag == "Enemy")
        {
            if (!UI.activeSelf)
            {
                UI.SetActive(true);
            }
            UI.GetComponentInChildren<Slider>().value = currentHealth / maxHealth;
        }
    }

    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        UpdateUI();
    }
}
