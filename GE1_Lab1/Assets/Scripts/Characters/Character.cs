using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public GameObject UI;

    private List<GameObject> nearCharacters;

    void Start()
    {
        Physics.IgnoreCollision(GetComponent<SphereCollider>(), GetComponent<CharacterController>());
        currentHealth = maxHealth;
        UI.SetActive(false);
        nearCharacters = new List<GameObject>();
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
