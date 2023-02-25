using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Arbok;
using Voltorb;

namespace Iris
{
    internal sealed class ItemButton : SelectableButton<ItemSpec>
    {
        [SerializeField]
        private ItemButtonClickedEvent m_OnClick;

        [SerializeField]
        private Text m_ItemName;

        [SerializeField]
        private Text m_ItemCount;

        private ItemSpec m_Item;

        public override void BindProperties(ItemSpec item)
        {
            m_Item = item;

            m_ItemName.text = string.Concat($"{m_Item.asset.itemName}");
            m_ItemCount.text = string.Concat($"x {m_Item.count}");

            interactable = true;
        }

        public override void BindPropertiesToNull()
        {
            m_Item = null;

            m_ItemName.text = string.Empty;
            m_ItemCount.text = string.Empty;

            interactable = false;
        }

        public override void Select()
        {
            m_OnClick?.Invoke(m_Item);
        }

        [Serializable]
        private sealed class ItemButtonClickedEvent : UnityEvent<ItemSpec>
        {

        }
    }
}
