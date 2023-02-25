using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Voltorb
{
    public class TabButton : Selectable
    {
        [SerializeField]
        private UnityEvent m_OnSelect;

        [SerializeField]
        private UnityEvent m_OnDeselect;

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            m_OnSelect?.Invoke();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            m_OnDeselect?.Invoke();
        }
    }
}
