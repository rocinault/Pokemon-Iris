using UnityEngine;

namespace Eevee
{
    public sealed class PokemonSpec
    {
        public readonly ScriptablePokemon asset;

        //public readonly Statistic health;
        //public readonly Statistic attack;
        //public readonly Statistic defence;
        //public readonly Statistic speed;

        //public Summoner owner;

        public readonly string name;

        public bool activeSelf;
        public bool isFainted;

        public int level;
        public int experience;

        private readonly AbilitySpec[] m_Abilities = new AbilitySpec[kMaxNumberOfAbilities];

        private const int kMaxNumberOfAbilities = 4;

        private PokemonSpec(ScriptablePokemon asset, int lv)
        {
            this.asset = asset;

            //health = new Statistic();
            //attack = new Statistic();
            //defence = new Statistic();
            //speed = new Statistic();

            name = asset.pokemonName;

            activeSelf = false;
            isFainted = false;

            level = lv;
            experience = Mathf.FloorToInt(Mathf.Pow(lv, 3f));

            CreateStartupAbilities();
            CreateStartupStatistics();
        }

        //private PokemonSpec(ScriptablePokemon asset, Summoner owner, int lv) : this(asset, lv)
        //{
        //    this.owner = owner;
        //}

        private void CreateStartupAbilities()
        {
            int index = 0;

            for (int i = 0; i < asset.abilities.Length; i++)
            {
                int levelRequired = asset.abilities[i].levelRequired;

                if (levelRequired <= level)
                {
                    m_Abilities[index] = asset.abilities[i].ability.CreateAbilitySpec();

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

        private void CreateStartupStatistics()
        {
            //float value = 0f;

            //health.iv = Random.Range(1, 32);

            //value = Mathf.Floor((2f * asset.health + health.iv + Mathf.Floor(health.ev / 4f)) * level / 100) + level + 10f;

            //health.value = value;
            //health.maxValue = value;

            //attack.iv = Random.Range(1, 32);

            //value = Mathf.Floor((Mathf.Floor((2f * asset.attack + attack.iv + Mathf.Floor(attack.ev / 4f)) * level / 100) + 5f) * 1f);

            //attack.value = value;
            //attack.maxValue = value;

            //defence.iv = Random.Range(1, 32);

            //value = Mathf.Floor((Mathf.Floor((2f * asset.defence + defence.iv + Mathf.Floor(defence.ev / 4f)) * level / 100) + 5f) * 1f);

            //defence.value = value;
            //defence.maxValue = value;

            //speed.iv = Random.Range(1, 32);

            //value = Mathf.Floor((Mathf.Floor((2f * asset.speed + speed.iv + Mathf.Floor(speed.ev / 4f)) * level / 100) + 5f) * 1f);

            //speed.value = value;
            //speed.maxValue = value;

            //Debug.Log($"HP{health.value} ATK:{attack.value} DEF:{defence.value} SPD:{speed.value}");
        }

        public bool TryGetAbility(int index, out AbilitySpec ability)
        {
            ability = null;

            if (m_Abilities[index] != null)
            {
                ability = m_Abilities[index];
                return true;
            }

            return false;
        }

        public AbilitySpec[] GetAllAbilities()
        {
            return m_Abilities;
        }

        public static PokemonSpec CreatePokemonSpec(ScriptablePokemon asset, int level)
        {
            return new PokemonSpec(asset, level);
        }

        //public static PokemonSpec Create(ScriptablePokemon asset, Summoner owner, int level)
        //{
        //    return new PokemonSpec(asset, owner, level);
        //}
    }
}
