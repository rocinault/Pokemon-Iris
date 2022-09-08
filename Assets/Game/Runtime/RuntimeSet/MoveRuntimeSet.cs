using UnityEngine;

using Golem;
using Umbreon;

namespace Iris
{
    [CreateAssetMenu]
    internal sealed class MoveRuntimeSet : RuntimeSet<Move>
    {
        internal void Sort()
        {
            m_Collection.Sort();
        }
    
    }
}
