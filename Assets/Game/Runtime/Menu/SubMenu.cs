using UnityEngine;

using Voltorb;

namespace Iris
{
    internal abstract class SubMenu : Menu
    {
        [SerializeField]
        private NavigationUtility m_NavigationUtility;

        public override void Show()
        {
            base.Show();

            m_NavigationUtility.OnShow();
        }

        public override void Hide()
        {
            base.Hide();

            m_NavigationUtility.OnHide();
        }
    }

    internal abstract class SubMenu<T> : SubMenu
    {
        public abstract void SetProperties(T properties);
    }
}
