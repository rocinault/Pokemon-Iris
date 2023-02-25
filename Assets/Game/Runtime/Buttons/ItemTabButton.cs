using System;

using UnityEngine;
using UnityEngine.Events;

using Arbok;
using Voltorb;

namespace Iris
{
    internal sealed class ItemTabButton : TabButton
    {
        [SerializeField]
        private ItemTabButtonClickedEvent m_OnClick;

        [SerializeField]
        private ItemType m_ItemTabType;

        public override void Select()
        {
            m_OnClick?.Invoke(m_ItemTabType);
        }

        [Serializable]
        private class ItemTabButtonClickedEvent : UnityEvent<ItemType>
        {

        }
    }
}
