using UnityEngine;

using Umbreon;

namespace Iris
{
    internal class Trainer : MonoBehaviour
    {
        [SerializeField]
        private ScriptablePokemon m_Asset;

        [SerializeField]
        private PokemonRuntimeSet m_PokemonRuntimeSet;

        [SerializeField]
        private Combatant m_Combatant;

        private void Awake()
        {
            CreateStartupPokemon();
        }

        private void CreateStartupPokemon()
        {
            var pokemon = new Pokemon(m_Asset);

            m_Combatant.pokemon = pokemon;
            m_PokemonRuntimeSet.Add(pokemon);
        }

        internal Pokemon GetFirstPokemonThatIsNotFainted()
        {
            for (int i = 0; i < m_PokemonRuntimeSet.Count(); i++)
            {
                var pokemon = m_PokemonRuntimeSet[i];

                if (pokemon != null && pokemon.health.value > 0)
                {
                    return pokemon;
                }
            }

            throw new System.Exception("No non-fainted pokemon found!");
        }
    }
}
