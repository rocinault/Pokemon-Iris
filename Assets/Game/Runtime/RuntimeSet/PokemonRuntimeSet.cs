using UnityEngine;

using Golem;
using Umbreon;

namespace Iris
{
    [CreateAssetMenu(fileName = "ScriptableObject/RuntimeSet/Pokemon", menuName = "PokemonRuntimeSet")]
    internal sealed class PokemonRuntimeSet : RuntimeSet<Pokemon>
    {
        private const int kMaxNumberOfPartyMembers = 6;

        public override void Add(Pokemon item)
        {
            if (m_Collection.Count < kMaxNumberOfPartyMembers)
            {
                base.Add(item);
            }
        }

        internal bool TryGetActivePokemon(out Pokemon pokemon)
        {
            pokemon = null;

            for (int i = 0; i < Count(); i++)
            {
                pokemon = m_Collection[i];

                if (pokemon.activeSelf)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
