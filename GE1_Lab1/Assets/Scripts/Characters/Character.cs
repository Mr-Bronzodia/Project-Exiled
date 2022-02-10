using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public float maxMana;
    private float currentMana;
    public float manaRegenAmount = 0.01f;
    public float healRegenAmount = 0.01f;

    private float timer = 0;

    private const float REGEN_INTERVAL = 1;

    private List<GameObject> nearCharacters;


    void Start()
    {
        Physics.IgnoreCollision(GetComponent<SphereCollider>(), GetComponent<CharacterController>());
        currentHealth = maxHealth;
        currentMana = maxMana;
        nearCharacters = new List<GameObject>();
    }

    private void UpdateUI()
    {
        if (gameObject.tag == "Player")
        {
            CharacterUI UI = gameObject.GetComponent<CharacterUI>();

            UI.UpdateHealth(currentHealth);
            UI.UpdateMana(currentMana);
        }
        else if (gameObject.tag == "Enemy")
        {
            EnemyUI UI = gameObject.GetComponent<EnemyUI>();

            UI.UpdateHealth(currentHealth);
        }
    }

    private void Update()
    {
        if (currentMana < maxMana | currentHealth < maxHealth)
        {
            timer += Time.deltaTime;

            if (timer <= REGEN_INTERVAL)
            {
                if (currentMana + manaRegenAmount > maxMana)
                {
                    currentMana = maxMana;
                }
                else
                {
                    currentMana += manaRegenAmount;
                }

                if (currentHealth + healRegenAmount > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                else
                {
                    currentHealth += healRegenAmount;
                }

                timer = 0;
                UpdateUI();
            }
        }
    }

    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        UpdateUI();
    }

    public float GetMana()
    {
        return currentMana;
    }

    public void UseMana(float cost)
    {
        if (currentMana - cost <= 0)
        {
           currentMana = 0;
        }
        else
        {
            currentMana -= cost;
        }

        UpdateUI();
    }

    

    public List<GameObject> GetNearCharacters()
    {
        return nearCharacters;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ally" | other.tag == "Enemy")
        {
            nearCharacters.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ally" | other.tag == "Enemy")
        {
            nearCharacters.Remove(other.gameObject);
        }
    }
}
