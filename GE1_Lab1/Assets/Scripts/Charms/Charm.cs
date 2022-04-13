using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Character;
using static PlayerSkillManager;
using System.Text.RegularExpressions;

public class Charm : MonoBehaviour
{
    public GameObject hoverText;
    public CharmItem item;

    private void Start()
    {
        Regex r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

        string readyName = r.Replace(item.type.ToString(), " ");

        hoverText.GetComponent<TMP_Text>().SetText("Charm Of " + readyName);
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
            item.WasApplied = true;
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
            ManaCost,
            Duration,
            Health,
            Mana,
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
            charmType.ManaCost,
            charmType.Duration,
            charmType.Health,
            charmType.Mana,
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
            {charmType.ManaCost, "CharmPlaceholder" },
            {charmType.Duration, "CharmPlaceholder" },
            {charmType.Health, "CharmPlaceholder" },
            {charmType.Mana, "CharmPlaceholder" },
        };

        private static Dictionary<charmType, string> descriptinons = new Dictionary<charmType, string>()
        {
            {charmType.Spread, "<color=#000000>Decreases the spread of projectiles</color>\n<color=#323330>Affects:</color>\n<color=#EB5e34>Fireball</color>" },
            {charmType.Speed, "<color=#000000>Increases the speed of object</color>\n<color=#323330>Affects:</color>\n<color=#eb5e34>Fireball</color>" },
            {charmType.Damege, "<color=#000000>Increases The damage of object</color>\n<color=#323330>Affects:</color>\n<color=#eb5e34>Fireball</color>" },
            {charmType.Quantity, "<color=#000000>Increases The number of objects</color>\n<color=#323330>Affects:</color>\n<color=#EB5E34>Fireball</color>\n<color=#545352>Shadow Clone</color>\n<color=#df1ee6>Counter</color>" },
            {charmType.Range, "<color=#000000>Increases the range of object</color>\n<color=#323330>Affects:</color>\nFireball\n<color=#1eb4e6>Dash</color>" },
            {charmType.Cooldown, "<color=#000000>Decreases the cooldown of a skill</color>\n<color=#323330>Affects:</color>\n<color=#eb5e34>Fireball</color>\n<color=#545352>Shadow Clone</color>\n<color=#df1ee6>Counter</color>\n<color=#1eb4e6>Dash</color>" },
            {charmType.Reflection, "<color=#000000>Projectiles reflect off walls</color>\n<color=#323330>Affects:</color>:\n<color=#eb5e34>Fireball</color>" },
            {charmType.Chains, "<color=#000000>Increase number of projectiles that the initial projectile will split to</color>\n<color=#323330>Affects:</color>\n<color=#eb5e34>Fireball</color>" },
            {charmType.ManaCost, "<color=#000000>Decreases the mana requiroments of a skill</color>\n<color=#323330>Affects:</color>\n<color=#eb5e34>Fireball</color>\n<color=#545352>Shadow Clone</color>\n<color=#df1ee6>Counter</color>\n<color=#1eb4e6>Dash</color>" },
            {charmType.Duration, "<color=#000000>Increases the duration of a skill</color>\n<color=#323330>Affects:</color>\n<color=#df1ee6>Counter</color>" },
            {charmType.Health, "<color=#000000>Increases Character Health</color>\n<color=#323330>Affects:</color>\n<color=#a8324e>Character</color>" },
            {charmType.Mana, "<color=#000000>Increases Character Mana</color>\n<color=#323330>Affects:</color>\n<color=#a8324e>Character</color>" },

        };

        private static Dictionary<charmType, float> lootTable = new Dictionary<charmType, float>()
        {
            {charmType.Health, 0.2f },
            {charmType.Damege, 0.5f  },
            {charmType.Spread, 0.8f },
            {charmType.Speed, 0.8f  },
            {charmType.Quantity, 0.9f },
            {charmType.Range, 0.2f },
            {charmType.Cooldown, 0.9f },
            {charmType.Reflection, 0.8f },
            {charmType.Chains, 0.9f },
            {charmType.ManaCost, 0.6f },
            {charmType.Duration, 0.8f },
            {charmType.Mana, 0.5f },
        };

        public int level;
        public charmType type;
        public bool WasApplied;
        private Character owner;

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

        public void SetOwner(Character owner)
        {
            this.owner = owner;
        }

        public Character GetOwner()
        {
            return owner;
        }

        public string GetDescription()
        {
            return descriptinons[this.type];
        }

        public Sprite GetIcon()
        {
            return Resources.Load<Sprite>(icons[this.type]);
        }

        public static List<charmType> GetAllAvailibleTypes()
        {
            return charmTypes;
        }

        public static Dictionary<charmType, float> GetLootTable()
        {
            return lootTable;
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
                    statistics.damage += this.level * 33.3F;
                    break;
                case charmType.Quantity:
                    statistics.quantityMultiplier += this.level;
                    break;
                case charmType.Range:
                    statistics.range += this.level;
                    break;
                case charmType.Cooldown:

                    if (statistics.cooldown - level / 10 > 0.5)
                    {
                        statistics.cooldown -= this.level / 10;
                    }
                    else
                    {
                        statistics.cooldown = 0.5f;
                    }

                    break;
                case charmType.Reflection:
                    statistics.totalBounces += this.level;
                    break;
                case charmType.Chains:
                    statistics.totalChains += this.level;
                    break;
                case charmType.ManaCost:
                    statistics.manaCost -= this.level;
                    break;
                case charmType.Duration:
                    statistics.duration += this.level;
                    break;
                case charmType.Health:
                    owner.maxHealth += this.level * 33.3f;
                    owner.currentHealth = owner.maxHealth;
                    break;
                case charmType.Mana:
                    owner.maxMana += this.level * 10;
                    owner.maxMana = owner.currentMana;
                    break;
                default:
                    Debug.LogError("Faild to apply Charm");
                    break;

            }

            if (statistics != null)
            {
                statistics.manaCost += level * 1.8f;
            }
        }

        public void Discharge(InventoryManager skill)
        {
            SkillVariables statistics = skill.stats;

            switch (this.type)
            {
                case charmType.Speed:
                    statistics.speed -= this.level;
                    break;
                case charmType.Spread:
                    statistics.spread += this.level;
                    break;
                case charmType.Damege:
                    statistics.damage -= this.level;
                    break;
                case charmType.Quantity:
                    statistics.quantityMultiplier += this.level;
                    break;
                case charmType.Range:
                    statistics.range -= this.level;
                    break;
                case charmType.Cooldown:
                    statistics.cooldown += this.level / 10;
                    break;
                case charmType.Reflection:
                    statistics.totalBounces -= this.level;
                    break;
                case charmType.Chains:
                    statistics.totalChains -= this.level;
                    break;
                case charmType.ManaCost:
                    statistics.manaCost += this.level;
                    break;
                case charmType.Duration:
                    statistics.duration -= this.level;
                    break;
                case charmType.Health:
                    owner.maxHealth -= this.level * 10;
                    owner.currentHealth = owner.maxHealth;
                    break;
                case charmType.Mana:
                    owner.maxMana -= this.level * 10;
                    owner.currentMana = owner.maxMana;
                    break;
                default:
                    Debug.LogError("Faild to discharge Charm");
                    break;

            }
        }
    }
}