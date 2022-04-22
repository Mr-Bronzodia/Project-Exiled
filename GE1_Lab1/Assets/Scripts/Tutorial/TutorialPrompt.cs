using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialPrompt : MonoBehaviour
{
    public string TutText;
    public GameObject TutBox;
    public bool IsOneTimePrompt;

    private TMP_Text text;

    private void Start()
    {
        text = TutBox.GetComponentInChildren<TMP_Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is CharacterController)
        {
            TutBox.SetActive(true);
            text.text = TutText;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TutBox.SetActive(false);

        if (IsOneTimePrompt)
        {
            Destroy(gameObject);
        }
    }

}
