using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveButtonControler : MonoBehaviour
{
    public GameObject SkillContextControlerObject;

    private ButtonData data;
    private SkilContextControler SkillContextControler;


    private void Start()
    {
        SkillContextControler = SkillContextControlerObject.GetComponent<SkilContextControler>();
    }

    public void onCilck()
    {
        Debug.Log("pre if");
        if (data != null)
        {
            Debug.Log("Removed");
            gameObject.GetComponentInParent<PlayerSkillManager>().RemoveCharmFromActive(SkillContextControler.GetCurrentContext(), data.GetCharm());
            SkillContextControler.RefreshContext();
        }
    }

    public void SetData(ButtonData data)
    {
        this.data = data;
    }

    public ButtonData GetData()
    {
        return data;
    }
}