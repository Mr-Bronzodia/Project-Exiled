using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerSkillManager;

public class CharacterUI : MonoBehaviour
{
    public GameObject Health;
    public GameObject Mana;

    public List<GameObject> SkillSlots;

    private List<InventoryManager> skills;


    private void Start()
    {
        skills = gameObject.GetComponent<PlayerSkillManager>().inventory;
    }

    public void UpdateCooldown()
    {
        if (skills.Count == 0)
        {
            skills = gameObject.GetComponent<PlayerSkillManager>().inventory;

            for (int i = 0; i < skills.Count; i++)
            {
                SkillSlots[i].GetComponent<Image>().sprite = skills[i].GetIcon();
            }
        }

        for (int i = 0; i < skills.Count; i++)
        {
            if (!skills[i].CanCast())
            {
                GameObject mask = SkillSlots[i].transform.GetChild(0).gameObject;
                mask.GetComponent<Image>().fillAmount = -1 * ((((Time.time - skills[i].nextCast) / skills[i].stats.cooldown) + 1) - 1);
            }
        }
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        Health.GetComponent<Image>().fillAmount = currentHealth / maxHealth;
    }

    public void UpdateMana(float currentMana, float maxMana)
    {
        Mana.GetComponent<Image>().fillAmount = currentMana / maxMana;
    }
}
