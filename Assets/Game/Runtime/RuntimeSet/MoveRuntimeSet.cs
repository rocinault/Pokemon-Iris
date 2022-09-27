using UnityEngine;

using Golem;
using Umbreon;

namespace Iris
{
    [CreateAssetMenu(fileName = "ScriptableObject/RuntimeSet/Move", menuName = "MoveRuntimeSet")]
    internal sealed class MoveRuntimeSet : RuntimeSet<Move>
    {
        internal void Sort()
        {
            m_Collection.Sort();
        }
    
    }
}
