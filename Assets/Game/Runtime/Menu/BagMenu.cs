using UnityEngine;

using Arbok;
using Voltorb;

namespace Iris
{
    internal sealed class BagMenu : Menu
    {
        [SerializeField]
        private ItemRuntimeSet m_ItemRuntimeSet;

        [SerializeField]
        private ItemButton[] m_ItemButtons;

        private ItemType m_SelectedItemType;

        private void Start()
        {
            SetItemButtonProperties();
        }

        private void SetItemButtonProperties()
        {
            var items = m_ItemRuntimeSet.GetItemsByType(m_SelectedItemType);

            for (int i = 0; i < m_ItemButtons.Length; i++)
            {
                var itemButton = m_ItemButtons[i];

                if (items.Length > i)
                {
                    itemButton.BindProperties(items[i]);
                }
                else
                {
                    itemButton.BindPropertiesToNull();
                }
            }
        }

        public void OnTabButtonClicked(ItemType itemType)
        {
            if (m_SelectedItemType != itemType)
            {
                m_SelectedItemType = itemType;
                SetItemButtonProperties();
            }
        }
    }
}
