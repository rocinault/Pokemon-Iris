using UnityEngine;
using UnityEngine.EventSystems;

namespace Voltorb
{
    internal sealed class TabButtonNavigation : SelectableNavigation
    {
        public override void SetSelectable(BaseEventData eventData, int selectedIndex)
        {
            base.SetSelectable(eventData, selectedIndex);

            m_Selectables[m_SelectedIndex].Select();
        }

        protected override void OnMoveLeft(AxisEventData eventData)
        {
            m_Selectables[m_SelectedIndex].OnDeselect(eventData);
            m_SelectedIndex = Mathf.Clamp(m_SelectedIndex - 1, 0, m_Selectables.Length - 1);
            m_Selectables[m_SelectedIndex].OnSelect(eventData);

            m_Selectables[m_SelectedIndex].Select();
        }

        protected override void OnMoveRight(AxisEventData eventData)
        {
            m_Selectables[m_SelectedIndex].OnDeselect(eventData);
            m_SelectedIndex = Mathf.Clamp(m_SelectedIndex + 1, 0, m_Selectables.Length - 1);
            m_Selectables[m_SelectedIndex].OnSelect(eventData);

            m_Selectables[m_SelectedIndex].Select();
        }
    }
}
