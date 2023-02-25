using UnityEngine;

using Arbok;
using Golem;
using Voltorb;

namespace Iris
{
    internal sealed class ItemSubMenu : SubMenu<ItemSpec>
    {
        private ItemSpec m_SelectedItemToUse;

        public override void SetProperties(ItemSpec properties)
        {
            m_SelectedItemToUse = properties;
        }

        public void OnConfirmButtonClicked()
        {
            EventSystem.instance.Invoke(ItemSelectedEventArgs.Create(m_SelectedItemToUse));
        }
    }
}