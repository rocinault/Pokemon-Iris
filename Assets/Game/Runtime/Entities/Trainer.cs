using UnityEngine;

using Umbreon;

namespace Iris
{
    internal class Trainer : Combatant
    {
        [SerializeField]
        private PokemonRuntimeSet m_PokemonRuntimeSet;

        private Pokemon m_ActivePokemon;

        private void Awake()
        {
            CreateStartupPokemonParty();
        }

        private void CreateStartupPokemonParty()
        {
            var pokemon = new Pokemon(m_Asset);
            m_PokemonRuntimeSet.Add(pokemon);

            SetActivePokemon(pokemon);
        }

        internal void SetActivePokemon(Pokemon pokemon)
        {
            m_ActivePokemon = pokemon;
            m_ActivePokemon.isActive = true;
        }

        internal bool TryGetActivePokemon(out Pokemon pokemon)
        {
            pokemon = default;

            if (m_ActivePokemon != null)
            {
                pokemon = m_ActivePokemon;
                return true;
            }

            return false;
        }
    }
}
