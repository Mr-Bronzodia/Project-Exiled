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

        private static Dictionary<charmType, string> icons = new Dictionary<charmType, string>()
        {
            {charmType.Spread, "CharmPlaceholder" },
            {charmType.Speed, "CharmPlaceholder" },
            {charmType.Damege, "CharmPlaceholder" },
            {charmType.Quantity, "CharmPlaceholder" },
            {charmType.Range, "CharmPlaceholder" },
            {charmType.Cooldown, "CharmPlaceholder" },
            {charmType.Reflection, "CharmPlaceholder" },
            {charmType.Chains, "CharmPlaceholder" },
            {charmType.Mana, "CharmPlaceholder" },
            {charmType.Duration, "CharmPlaceholder" },
        };

        private static Dictionary<charmType, string> descriptinons = new Dictionary<charmType, string>() 
        {
            {charmType.Spread, "Decreases the spread of projectiles\nCompatible With:\nFireball" },
            {charmType.Speed, "Increases the speed of object\nCompatible With:\nFireball\nCharacter" },
            {charmType.Damege, "Increases The damage of object\nCompatible With:\nFireball" },
            {charmType.Quantity, "Increases The number of objects\nCompatible With:\nFireball\nShadow Clone\nCounter" },
            {charmType.Range, "Increases the range of object\nCompatible With:\nFireball\nDash" },
            {charmType.Cooldown, "Decreases the cooldown of a skill\nCompatible With:\nFireball\nShadow Clone\nCounter\nDash" },
            {charmType.Reflection, "Projectiles reflect off walls\nCompatible With:\nFireball" },
            {charmType.Chains, "Increase number of projectiles that the initial projectile will split to\nCompatible With:\nFireball" },
            {charmType.Mana, "Decreases the mana requiroments of a skill\nCompatible With:\nFireball\nShadow Clone\nCounter\nDash" },
            {charmType.Duration, "Increases the duration of a skill\nCompatible With:\nCounter" },

        };

        public int level;
        public charmType type;
        public bool WasApplied;

        public CharmItem(int level)
        {
            this.level = level;
        }

        public CharmItem Generate()
        {
            System.Random rnd = new System.Random(GetHashCode());
            this.type = charmTypes[rnd.Next(0, charmTypes.Count)];

            return this;
        }

        public string GetDescription()
        {
            return descriptinons[this.type];
        }

        public Sprite GetIcon()
        {
            return Resources.Load<Sprite>(icons[this.type]);
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
                    statistics.spread -= this.level;
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
                    statistics.cooldown -= this.level / 10;
                    break;
                case charmType.Reflection:
                    statistics.totalBounces += this.level;
                    break;
                case charmType.Chains:
                    statistics.totalChains += this.level;
                    break;
                case charmType.Mana:
                    statistics.manaCost -= this.level;
                    break;
                case charmType.Duration:
                    statistics.duration += this.level;
                    break;
                default:
                    Debug.LogError("Faild to apply Charm");
                    break;
                 
            }
        }
    }
}
