using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Voltorb
{
    public class SelectableButton : Selectable
    {
        [SerializeField]
        private UnityEvent<GameObject> m_OnSelect;

        [SerializeField]
        private UnityEvent<GameObject> m_OnDeselect;

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            m_OnSelect?.Invoke(gameObject);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            m_OnDeselect?.Invoke(gameObject);
        }
    }

    public abstract class SelectableButton<T> : SelectableButton
    {
        public abstract void BindProperties(T properties);
        public abstract void BindPropertiesToNull();
    }
}
