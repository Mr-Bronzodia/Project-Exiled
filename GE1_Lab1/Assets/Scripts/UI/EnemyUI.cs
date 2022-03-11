using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public GameObject UI;

    private Slider healthBar;

    private void Start()
    {
        healthBar = UI.GetComponentInChildren<Slider>();
        UI.SetActive(false);
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (!UI.activeInHierarchy)
        {
            UI.SetActive(true);
        }

        healthBar.value = currentHealth / maxHealth;

        if (currentHealth == maxHealth)
        {
            UI.SetActive(false);
        }
    }
}
