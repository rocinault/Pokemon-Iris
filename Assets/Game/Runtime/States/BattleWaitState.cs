using System;
using System.Collections;

using UnityEngine;

using Golem;
using Slowbro;

namespace Iris
{
    internal sealed class BattleWaitState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly GameBattleStateBehaviour m_StateBehaviour;

        private BattleCoordinator m_Coordinator;
        private BattleGraphicsInterface m_Interface;

        private IEnumerator m_BouncePokemonAndStatsPanelAsync;

        public BattleWaitState(T uniqueID, GameBattleStateBehaviour stateBehaviour) : base(uniqueID)
        {
            m_StateBehaviour = stateBehaviour;

            m_BouncePokemonAndStatsPanelAsync = BouncePokemonAndStatsPanelWhileWaiting();
        }

        public override void Enter()
        {
            m_Coordinator = m_StateBehaviour.GetBattleCoordinator();
            m_Interface = m_StateBehaviour.GetBattleGraphicsInterface();

            m_Interface.SetActive<PlayerTrainerPanel>(false);
            m_Interface.SetActive<PlayerPokemonPanel>(true);

            EventSystem.instance.AddListener<MoveButtonClickedEventArgs>(OnMoveButtonClicked);
            EventSystem.instance.AddListener<AbilityButtonClickedEventArgs>(OnAbilityButtonClicked);

            PrintPokemonNameAndShowMovesMenu();

            m_StateBehaviour.StartCoroutine(m_BouncePokemonAndStatsPanelAsync);
        }

        private void PrintPokemonNameAndShowMovesMenu()
        {
            m_Coordinator.GetPlayerActivePokemon(out var combatant);

            string message = string.Concat($"What will {combatant.name} do?");

            m_Interface.PrintCompletedText(message);
            m_Interface.Show<MovesMenu>();
        }

        private IEnumerator BouncePokemonAndStatsPanelWhileWaiting()
        {
            while (true)
            {
                yield return m_Interface.BouncePokemonAndStatsPanelWhileWaiting();
            }
        }

        private void OnMoveButtonClicked(MoveButtonClickedEventArgs args)
        {
            var selection = args.selection;

            switch (selection)
            {
                case MoveSelection.Fight:
                    HideMovesMenuAndShowAbilitiesMenu();
                    break;
                case MoveSelection.Pokemon:
                    TransitionIntoMenuGameMode();
                    break;
                default:
                    break;
            }
        }

        private void HideMovesMenuAndShowAbilitiesMenu()
        {
            m_Interface.Hide<MovesMenu>();
            m_Interface.Show<AbilitiesMenu>();
        }

        private void TransitionIntoMenuGameMode()
        {
            Exit();

            GameCoordinator.instance.EnterGameMode(GameCoordinator.instance.menuState);
        }

        private void OnAbilityButtonClicked(AbilityButtonClickedEventArgs args)
        {
            m_StateBehaviour.AddFightMoveToRuntimeSet(args.abilitySpec);
            m_StateBehaviour.ChangeState(BattleState.Action);
        }

        public override void Exit()
        {
            m_StateBehaviour.StopCoroutine(m_BouncePokemonAndStatsPanelAsync);

            ClearTextAndHideAbilitiesMenu();

            EventSystem.instance.RemoveListener<MoveButtonClickedEventArgs>(OnMoveButtonClicked);
            EventSystem.instance.RemoveListener<AbilityButtonClickedEventArgs>(OnAbilityButtonClicked);
        }

        private void ClearTextAndHideAbilitiesMenu()
        {
            m_Interface.CleanupTextProcessorAndClearText();
            m_Interface.Hide<AbilitiesMenu>();
        }
    }
}