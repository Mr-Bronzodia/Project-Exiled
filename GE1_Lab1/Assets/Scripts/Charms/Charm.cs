using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Character;
using static PlayerSkillManager;

public class Charm : MonoBehaviour
{
    public GameObject hoverText;
    public CharmItem item;

    private void Start()
    {
        hoverText.GetComponent<TMP_Text>().SetText("Charm of " + item.type.ToString());
    }

    public void SetItem(CharmItem item)
    {
        this.item = item;
    }

    public void PickUp(GameObject pickCharacter)
    {
        pickCharacter.GetComponent<Character>().AddCharm(item);

        if (TagManager.isNPC(pickCharacter.tag))
        {
            item.Apply(pickCharacter.GetComponent<NPCSkillManager>().inventory[0]);
        }

        Destroy(gameObject);
    }

    [Serializable]
    public class CharmItem
    {
        private System.Random rnd;

        public enum charmType
        {
            Speed,
            Spread,
            Damege,
            Quantity,
            Range,
            Cooldown,
            Reflection,
            Chains,
            Mana,
            Duration,
        }

        private static List<charmType> charmTypes = new List<charmType>() {
            charmType.Speed,
            charmType.Spread,
            charmType.Damege,
            charmType.Quantity,
            charmType.Range,
            charmType.Cooldown,
            charmType.Reflection,
            charmType.Chains,
            charmType.Mana,
            charmType.Duration
        };

        public int level;
        public charmType type;

        public CharmItem(int level)
        {
            rnd = new System.Random();
            this.level = level;
        }

        private void Generate()
        {
            this.type = charmTypes[rnd.Next(0, charmTypes.Count)];
        }

        public void Apply(InventoryManager skill)
        {
            SkillVariables statistics = skill.stats;

            switch (this.type)
            {
                case charmType.Speed:
                    statistics.speed += this.level;
                    break;
                case charmType.Spread:
                    statistics.spread += this.level;
                    break;
                case charmType.Damege:
                    statistics.damage += this.level;
                    break;
                case charmType.Quantity:
                    statistics.quantityMultiplier += this.level;
                    break;
                case charmType.Range:
                    statistics.range += this.level;
                    break;
                case charmType.Cooldown:
                    statistics.cooldown -= this.level;
                    break;
                case charmType.Reflection:
                    statistics.totalBounces += this.level;
                    break;
                case charmType.Chains:
                    statistics.totalChains += this.level;
                    break;
                case charmType.Mana:
                    statistics.damage -= this.level;
                    break;
                case charmType.Duration:
                    statistics.duration += this.level;
                    break;
                default:
                    Debug.Log("Faild to apply");
                    break;
                 
            }
        }
    }
}
