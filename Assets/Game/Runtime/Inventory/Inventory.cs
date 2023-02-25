using UnityEngine;

using Arbok;

namespace Iris
{
    internal sealed class Inventory : MonoBehaviour
    {
        [SerializeField]
        private ScriptableItem[] m_StartingItems;

        [SerializeField]
        private ItemRuntimeSet m_ItemRuntimeSet;

        private void Awake()
        {
            AddStartUpItemsToRuntimeSet();
        }

        private void AddStartUpItemsToRuntimeSet()
        {
            int length = m_StartingItems.Length;

            for (int i = 0; i < length; i++)
            {
                m_ItemRuntimeSet.Add(m_StartingItems[i].CreateItemSpec());
            }
        }
    }
}
