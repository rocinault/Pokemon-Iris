using UnityEngine;
using UnityEngine.UI;

using Golem;
using Voltorb;

namespace Iris
{
    internal sealed class MovesMenu : Menu
    {
        [SerializeField]
        private Button m_FightButton;

        [SerializeField]
        private Button m_BagButton;

        [SerializeField]
        private Button m_PokemonButton;

        [SerializeField]
        private Button m_RunButton;

        protected override void AddListeners()
        {
            m_FightButton.onClick.AddListener(OnFightButtonClicked);
            m_BagButton.onClick.AddListener(OnBagButtonClicked);
            m_PokemonButton.onClick.AddListener(OnPokemonButtonClicked);
            m_RunButton.onClick.AddListener(OnRunButtonClicked);
        }

        private void OnFightButtonClicked()
        {
            var args = MoveButtonClickedEventArgs.CreateEventArgs(MoveSelection.fight);
            EventSystem.instance.Invoke(args);
        }

        private void OnBagButtonClicked()
        {
            var args = MoveButtonClickedEventArgs.CreateEventArgs(MoveSelection.bag);
            EventSystem.instance.Invoke(args);
        }

        private void OnPokemonButtonClicked()
        {
            var args = MoveButtonClickedEventArgs.CreateEventArgs(MoveSelection.pokemon);
            EventSystem.instance.Invoke(args);
        }

        private void OnRunButtonClicked()
        {
            var args = MoveButtonClickedEventArgs.CreateEventArgs(MoveSelection.run);
            EventSystem.instance.Invoke(args);
        }

        protected override void RemoveListeners()
        {
            m_FightButton.onClick.RemoveListener(OnFightButtonClicked);
            m_BagButton.onClick.RemoveListener(OnBagButtonClicked);
            m_PokemonButton.onClick.RemoveListener(OnPokemonButtonClicked);
            m_RunButton.onClick.RemoveListener(OnRunButtonClicked);
        }
    }
}