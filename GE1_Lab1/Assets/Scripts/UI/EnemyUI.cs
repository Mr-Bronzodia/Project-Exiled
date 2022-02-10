using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public GameObject UI;

    private float maxHealth;
    private Slider healthBar;

    private void Start()
    {
        maxHealth = gameObject.GetComponent<Character>().maxHealth;
        healthBar = UI.GetComponentInChildren<Slider>();
        UI.SetActive(false);
    }

    public void UpdateHealth(float currentHealth)
    {
        if (!UI.activeInHierarchy)
        {
            UI.SetActive(true);
        }

        healthBar.value = currentHealth / maxHealth;

        if (maxHealth == currentHealth)
        {
            UI.SetActive(false);
        }
    }
}
