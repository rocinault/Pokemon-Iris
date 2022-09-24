using UnityEngine;

namespace Umbreon
{
    public sealed class Pokemon
    {
        public readonly ScriptablePokemon asset;

        public readonly string name;

        public Statistic health = new Statistic();
        public Statistic attack = new Statistic();
        public Statistic defence = new Statistic();
        public Statistic speed = new Statistic();

        public Statistic evasion = new Statistic();
        public Statistic accuracy = new Statistic();

        public bool isActive;
        public bool isAlive;

        public uint level;
        public int experience;

        private readonly AbilitySpec[] m_Abilities = new AbilitySpec[kMaxNumberOfAbilities];

        private const uint kMaxNumberOfAbilities = 4;

        public Pokemon(ScriptablePokemon asset)
        {
            this.asset = asset;

            name = asset.name;
            level = asset.level;

            experience = Mathf.FloorToInt(Mathf.Pow(level, 3f));

            CreateStartupAbilities(asset);
            CreateStartupStats(asset);
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

        private void CreateStartupStats(ScriptablePokemon asset)
        {
            health.iv = Random.Range(1, 32);
            health.value = health.maxValue = CalculateHealthStatValue(asset.health);

            attack.iv = Random.Range(1, 32);
            attack.value = attack.maxValue = CalculateStatValue(asset.attack, attack);

            defence.iv = Random.Range(1, 32);
            defence.value = defence.maxValue = CalculateStatValue(asset.defence, defence);

            speed.iv = Random.Range(1, 32);
            speed.value = speed.maxValue = CalculateStatValue(asset.speed, speed);

            Debug.Log($"{name} HP{health.value} ATK:{attack.value} DEF:{defence.value} SPD:{speed.value}");
        }

        public void LevelUp()
        {
            level++;

            health.value = Mathf.Round(CalculateHealthStatValue(asset.health) * (health.value / health.maxValue));
            health.maxValue = CalculateHealthStatValue(asset.health);

            attack.value = attack.maxValue = CalculateStatValue(asset.attack, attack);
            defence.value = defence.maxValue = CalculateStatValue(asset.defence, defence);
            speed.value = speed.maxValue = CalculateStatValue(asset.speed, speed);

            Debug.Log($"{name} HP{health.maxValue} ATK:{attack.value} DEF:{defence.value} SPD:{speed.value}");
        }

        public bool TryGetStatistic(StatisticType type, out Statistic statistic)
        {
            switch (type)
            {
                case StatisticType.health:
                    statistic = health;
                    break;
                case StatisticType.attack:
                    statistic = attack;
                    break;
                case StatisticType.defence:
                    statistic = defence;
                    break;
                case StatisticType.speed:
                    statistic = speed;
                    break;
                default:
                    statistic = null;
                    break;
            }

            return statistic != null;
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

        private float CalculateHealthStatValue(float baseValue)
        {
            return Mathf.Floor((2f * baseValue + health.iv + Mathf.Floor(health.ev / 4f)) * level / 100) + level + 10f;
        }

        private float CalculateStatValue(float baseValue, Statistic stat)
        {
            return Mathf.Floor((Mathf.Floor((2f * baseValue + stat.iv + Mathf.Floor(stat.ev / 4f)) * level / 100) + 5f) * 1f);
        }
    }
}
