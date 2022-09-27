using System;
using System.Collections;

using UnityEngine;

using Golem;
using Slowbro;

namespace Iris
{
    internal sealed class BattleWaitState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly BattleGraphicsInterface m_GraphicsInterface;
        private readonly BattleCoordinator m_Coordinator;

        private readonly IEnumerator m_Coroutine;

        public BattleWaitState(T uniqueID, BattleGraphicsInterface graphicsInterface, BattleCoordinator coordinator) : base(uniqueID)
        {
            m_GraphicsInterface = graphicsInterface;
            m_Coordinator = coordinator;

            m_Coroutine = BouncePokemonAndStatsPanelWhileWaiting();
        }

        public override void Enter()
        {
            EventSystem.instance.AddListener<MoveButtonClickedEventArgs>(OnMoveButtonClicked);
            EventSystem.instance.AddListener<AbilityButtonClickedEventArgs>(OnAbilityButtonClicked);

            PrintPokemonNameAndShowMovesMenu();

            m_Coordinator.StartCoroutine(m_Coroutine);
        }

        private void PrintPokemonNameAndShowMovesMenu()
        {
            m_Coordinator.GetPlayerActivePokemon(out var combatant);

            string message = string.Concat($"What will {combatant.name} do?");

            m_GraphicsInterface.PrintCompletedText(message);
            m_GraphicsInterface.Show<MovesMenu>();
        }

        private IEnumerator BouncePokemonAndStatsPanelWhileWaiting()
        {
            while (true)
            {
                yield return m_GraphicsInterface.BouncePokemonAndStatsPanelWhileWaiting();
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
                default:
                    break;
            }
        }

        private void HideMovesMenuAndShowAbilitiesMenu()
        {
            m_GraphicsInterface.Hide<MovesMenu>();
            m_GraphicsInterface.Show<AbilitiesMenu>();
        }

        private void OnAbilityButtonClicked(AbilityButtonClickedEventArgs args)
        {
            m_Coordinator.AddFightMoveToRuntimeSet(args.abilitySpec);
            m_Coordinator.ChangeState(BattleState.Action);
        }

        public override void Exit()
        {
            m_Coordinator.StopCoroutine(m_Coroutine);

            ClearTextAndHideAbilitiesMenu();

            EventSystem.instance.RemoveListener<MoveButtonClickedEventArgs>(OnMoveButtonClicked);
            EventSystem.instance.RemoveListener<AbilityButtonClickedEventArgs>(OnAbilityButtonClicked);
        }

        private void ClearTextAndHideAbilitiesMenu()
        {
            m_GraphicsInterface.CleanupTextProcessorAndClearText();
            m_GraphicsInterface.Hide<AbilitiesMenu>();
        }
    }
}