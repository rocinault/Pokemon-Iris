using UnityEngine;
using UnityEngine.UI;

using Golem;
using Voltorb;

namespace Iris
{
    internal sealed class PartyOptionsMenu : Menu
    {
        [SerializeField]
        private Button m_SummaryButton;

        [SerializeField]
        private Button m_SwitchButton;

        [SerializeField]
        private Button m_ReturnButton;

        protected override void AddListeners()
        {
            m_SummaryButton.onClick.AddListener(OnSummaryButtonClicked);
            m_SwitchButton.onClick.AddListener(OnSwitchButtonClicked);
            m_ReturnButton.onClick.AddListener(OnReturnButtonClicked);
        }

        private void OnSummaryButtonClicked()
        {
            var args = PartyOptionsButtonClickedEventArgs.CreateEventArgs(PartySelection.Summary, null, null);
            EventSystem.instance.Invoke(args);
        }

        private void OnSwitchButtonClicked()
        {
            var args = PartyOptionsButtonClickedEventArgs.CreateEventArgs(PartySelection.Switch, null, null);
            EventSystem.instance.Invoke(args);
        }

        private void OnReturnButtonClicked()
        {
            var args = PartyOptionsButtonClickedEventArgs.CreateEventArgs(PartySelection.Return, null, null);
            EventSystem.instance.Invoke(args);
        }

        protected override void RemoveListeners()
        {
            m_SummaryButton.onClick.RemoveListener(OnSummaryButtonClicked);
            m_SwitchButton.onClick.RemoveListener(OnSwitchButtonClicked);
            m_ReturnButton.onClick.RemoveListener(OnReturnButtonClicked);
        }
    }
}
