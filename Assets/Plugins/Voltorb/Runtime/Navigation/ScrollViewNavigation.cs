using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Voltorb
{
    internal sealed class ScrollViewNavigation : SelectableNavigation
    {
        protected override void OnMoveUp(AxisEventData eventData)
        {
            m_Selectables[m_SelectedIndex].OnDeselect(eventData);
            m_SelectedIndex = Mathf.Clamp(m_SelectedIndex - 1, 0, m_Selectables.Length - 1);
            m_Selectables[m_SelectedIndex].OnSelect(eventData);
        }

        protected override void OnMoveDown(AxisEventData eventData)
        {
            int index = Mathf.Clamp(m_SelectedIndex + 1, 0, m_Selectables.Length - 1);

            if (IsTargetSelectableInteractable(index))
            {
                m_Selectables[m_SelectedIndex].OnDeselect(eventData);
                m_SelectedIndex = index;
                m_Selectables[m_SelectedIndex].OnSelect(eventData);
            }
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            m_Selectables[m_SelectedIndex].Select();
        }
    }
}
