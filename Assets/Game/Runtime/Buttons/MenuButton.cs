using System;

using UnityEngine;
using UnityEngine.Events;

using Voltorb;

namespace Iris
{
    internal sealed class MenuButton : SelectableButton
    {
        [SerializeField]
        private MenuButtonClickedEvent m_OnClick;

        public override void Select()
        {
            m_OnClick?.Invoke();
        }

        [Serializable]
        private sealed class MenuButtonClickedEvent : UnityEvent
        {

        }
    }
}
