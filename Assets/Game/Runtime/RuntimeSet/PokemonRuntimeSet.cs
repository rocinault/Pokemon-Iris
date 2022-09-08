using UnityEngine;

using Golem;
using Umbreon;

namespace Iris
{
    [CreateAssetMenu]
    internal sealed class PokemonRuntimeSet : RuntimeSet<Pokemon>
    {
        private const int kMaxNumberOfPartyMembers = 6;

        public override void Add(Pokemon item)
        {
            if (m_Collection.Count < kMaxNumberOfPartyMembers)
            {
                Debug.Log($"Added {item.name} to {name}");

                base.Add(item);
            }
        }
    }
}
