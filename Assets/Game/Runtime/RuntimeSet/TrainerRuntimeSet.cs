using System;

using UnityEngine;

using Golem;
using Umbreon;

namespace Iris
{
    [CreateAssetMenu(fileName = "ScriptableObject/RuntimeSet/TrainerSet", menuName = "TrainerRuntimeSet")]
    internal sealed class TrainerRuntimeSet : RuntimeSet<TrainerSet>
    {
        internal static Pokemon CreatePokemonFromSet(TrainerSet set)
        {
            return new Pokemon(set.asset, null, set.level);
        }
    }

    [Serializable]
    internal sealed class TrainerSet
    {
        [SerializeField]
        internal ScriptablePokemon asset;

        [SerializeField, Range(1, 100)]
        internal uint level;
    }
}
