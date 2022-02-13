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

    private float maxMana;
    private float maxHealth;

    private void Start()
    {
        maxHealth = gameObject.GetComponent<Character>().maxHealth;
        maxMana = gameObject.GetComponent<Character>().maxMana;
        skills = gameObject.GetComponent<PlayerSkillManager>().inventory;
    }

    public void UpdateCooldown()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (!skills[i].CanCast())
            {
                GameObject mask = SkillSlots[i].transform.GetChild(0).gameObject;
                mask.GetComponent<Image>().fillAmount = -1 * ((((Time.time - skills[i].nextCast) / skills[i].stats.cooldown) + 1) - 1);
                Debug.Log("updating");
            }
        }
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
