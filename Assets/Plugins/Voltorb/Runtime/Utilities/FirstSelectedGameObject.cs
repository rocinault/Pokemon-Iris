using UnityEngine;
using UnityEngine.EventSystems;

namespace Voltorb
{
    [RequireComponent(typeof(SelectableNavigation))]
    internal sealed class FirstSelectedGameObject : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField]
        private SelectableNavigation m_Navigation;

        [SerializeField]
        private bool m_LastSelectedObject;

        private int m_SelectedIndex;

        public void OnSelect(BaseEventData eventData)
        {
            m_Navigation.SetSelectable(eventData, m_SelectedIndex);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (m_LastSelectedObject)
            {
                m_SelectedIndex = m_Navigation.selectedIndex;
            }
        }
    }
}
