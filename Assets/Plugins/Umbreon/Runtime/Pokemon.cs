using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Umbreon
{
    public sealed class Pokemon
    {
        public readonly ScriptablePokemon asset;

        public readonly string name;

        public Attribute health;
        public Attribute attack;
        public Attribute defence;
        public Attribute speed;

        public bool isActive;
        public bool isAlive;

        public uint level;

        private readonly AbilitySpec[] m_Abilities = new AbilitySpec[kMaxNumberOfAbilities];

        private const uint kMaxNumberOfAbilities = 4;

        public Pokemon(ScriptablePokemon asset)
        {
            this.asset = asset;

            name = asset.name;

            level = 5;

            CreateStartupAbilities(asset);
            CreateStartupAttributes(asset);
        }

        private void CreateStartupAbilities(ScriptablePokemon asset)
        {
            int index = 0;
            var abilities = asset.abilities;

            for (int i = 0; i < abilities.Length; i++)
            {
                uint levelRequired = abilities[i].levelRequired;

                if (levelRequired <= level)
                {
                    m_Abilities[index] = abilities[i].ability.CreateAbilitySpec();

                    index++;

                    if (index >= kMaxNumberOfAbilities)
                    {
                        index = 0;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void CreateStartupAttributes(ScriptablePokemon asset)
        {
            health = new Attribute(CalculateHealthAttributeValue(asset.health));
            attack= new Attribute(CalculateAttributeValue(asset.attack));
            defence = new Attribute(CalculateAttributeValue(asset.defence));
            speed = new Attribute(CalculateAttributeValue(asset.speed));

            Debug.Log($"{name} HP{health.value} ATK:{attack.value} DEF:{defence.value} SPD:{speed.value}");
        }

        public bool TryGetAbility(int index, out AbilitySpec spec)
        {
            spec = null;

            if (index > kMaxNumberOfAbilities && m_Abilities[index] != null)
            {
                spec = m_Abilities[index];
                return true;
            }

            return false;
        }

        public AbilitySpec[] GetAllAbilities()
        {
            return m_Abilities;
        }

        private float CalculateHealthAttributeValue(float value)
        {
            return Mathf.Floor((2f * value + UnityEngine.Random.Range(0, 32) + Mathf.Floor(0f / 4f)) * level / 100) + level + 10f;
        }

        private float CalculateAttributeValue(float value)
        {
            return Mathf.Floor((Mathf.Floor((2f * value + UnityEngine.Random.Range(0, 32) + Mathf.Floor(0f / 4f)) * level / 100) + 5f) * 1f);
        }
    }
}
