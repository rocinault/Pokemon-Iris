using System;

using UnityEngine;

using Golem;

namespace Iris
{
    internal sealed class BattleWaitState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly BattleGraphicsInterface m_GraphicsInterface;
        private readonly BattleCoordinator m_Coordinator;

        public BattleWaitState(T uniqueID, BattleGraphicsInterface graphicsInterface, BattleCoordinator coordinator) : base(uniqueID)
        {
            m_GraphicsInterface = graphicsInterface;
            m_Coordinator = coordinator;
        }

        public override void Enter()
        {
            EventSystem.instance.AddListener<MoveButtonClickedEventArgs>(OnMoveButtonClicked);
            EventSystem.instance.AddListener<AbilityButtonClickedEventArgs>(OnAbilityButtonClicked);

            PrintPokemonNameAndShowMovesMenu();
        }

        private void PrintPokemonNameAndShowMovesMenu()
        {
            m_Coordinator.GetPlayerActiveCombatant(out var combatant);

            string message = string.Concat($"What will {combatant.pokemon.name.ToUpper()} do?");

            m_GraphicsInterface.PrintCompletedText(message);
            m_GraphicsInterface.Show<MovesMenu>();
        }

        private void OnMoveButtonClicked(MoveButtonClickedEventArgs args)
        {
            var selection = args.selection;

            switch (selection)
            {
                case MoveSelection.fight:
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
            m_Coordinator.ChangeState(BattleState.action);
        }

        public override void Exit()
        {
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