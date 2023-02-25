using System;

using UnityEngine;

using Arbok;
using Golem;

namespace Iris
{
    [CreateAssetMenu(fileName = "item-runtime-set", menuName = "ScriptableObjects/Iris/RuntimeSet/Item", order = 150)]
    internal sealed class ItemRuntimeSet : RuntimeSet<ItemSpec>
    {
        private const uint kMaxNumberOfItems = 999;
        private const uint kMinNumberOfItems = 1;

        public override void Add(ItemSpec item)
        {
            if (item.count < kMaxNumberOfItems)
            {
                if (TryGetItemByName(ref item))
                {
                    item.count++;
                }
                else
                {
                    m_Collection.Add(item);
                }
            }
        }

        public override void Remove(ItemSpec item)
        {
            if (item.count > kMinNumberOfItems)
            {
                item.count--;
            }
            else
            {
                if (TryGetItemByName(ref item))
                {
                    m_Collection.Remove(item);
                }
            }
        }
        
        internal ItemSpec[] GetItemsByType(ItemType match)
        {
            return Array.FindAll(m_Collection.ToArray(), (x) => x.asset.itemType == match);
        }

        private bool TryGetItemByName(ref ItemSpec item)
        {
            var items = GetItemsByType(item.asset.itemType);

            for (int i = 0; i < items.Length; i++)
            {
                if (string.Equals(item.asset.name, items[i].asset.name))
                {
                    item = items[i];
                    return true;
                }
            }

            return false;
        }
    }
}
