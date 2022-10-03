using UnityEngine;

using Golem;

namespace Iris
{
    internal sealed class GameMenuStateBehaviour : StateBehaviour<GameMode>
    {
        [SerializeField]
        private PokemonRuntimeSet m_PokemonRuntimeSet;

        [SerializeField]
        private MoveRuntimeSet m_MoveRuntimeSet;

        [SerializeField]
        private GameObjectRuntimeSet m_GameObjectRuntimeSet;

        private PartyGraphicsInterface m_Interface;

        public override GameMode uniqueID => GameMode.Party;

        public override void Enter()
        {
            m_Interface = m_GameObjectRuntimeSet.GetComponentFromRuntimeSet<PartyGraphicsInterface>();
            m_Interface.SetPartyMenuProperties(PartyGraphicProperties.CreateProperties(m_PokemonRuntimeSet.ToArray()));

            AddListeners();
        }

        private void AddListeners()
        {
            EventSystem.instance.AddListener<PartyPokemonButtonClickedEventArgs>(OnPartyPokemonButtonClicked);
            EventSystem.instance.AddListener<PartyOptionsButtonClickedEventArgs>(OnPartyOptionsButtonClicked);
        }

        private void OnPartyPokemonButtonClicked(PartyPokemonButtonClickedEventArgs args)
        {
            m_Interface.Show<PartyOptionsMenu>();
        }

        private void OnPartyOptionsButtonClicked(PartyOptionsButtonClickedEventArgs args)
        {
            switch (args.selection)
            {
                case PartySelection.Summary:
                    ProcessPokemonSummaryRequest(args);
                    break;
                case PartySelection.Switch:
                    ProcessPokemonSwitchRequest(args);
                    break;
                case PartySelection.Return:
                    HidePokemonOptionsMenuAndShowParty();
                    break;
            }
        }

        private void ProcessPokemonSummaryRequest(PartyOptionsButtonClickedEventArgs args)
        {
            Debug.Log("pokemon summary selected");
        }

        private void ProcessPokemonSwitchRequest(PartyOptionsButtonClickedEventArgs args)
        {
            GameCoordinator.instance.ExitGameMode();
        }

        private void HidePokemonOptionsMenuAndShowParty()
        {
            m_Interface.Hide<PartyOptionsMenu>();
            m_Interface.Show<PartyPokemonMenu>();
        }

        public override void Exit()
        {
            RemoveListeners();   
        }

        private void RemoveListeners()
        {
            EventSystem.instance.RemoveListener<PartyOptionsButtonClickedEventArgs>(OnPartyOptionsButtonClicked);
            EventSystem.instance.RemoveListener<PartyPokemonButtonClickedEventArgs>(OnPartyPokemonButtonClicked);
        }
    }
}
