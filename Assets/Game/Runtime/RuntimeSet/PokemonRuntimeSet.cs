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
    }
}
