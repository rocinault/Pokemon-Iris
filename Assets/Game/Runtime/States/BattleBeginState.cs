using System;
using System.Collections;

using UnityEngine;

using Golem;
using Slowbro;

namespace Iris
{
    internal sealed class BattleBeginState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly BattleGraphicsInterface m_GraphicsInterface;
        private readonly BattleCoordinator m_Coordinator;

        private readonly WaitForSeconds m_DelayForHalfSecond = new WaitForSeconds(kDelayForHalfSecond);

        private const float kDelayForHalfSecond = 0.5f;

        public BattleBeginState(T uniqueID, BattleGraphicsInterface graphicsInterface, BattleCoordinator coordinator) : base(uniqueID)
        {
            m_GraphicsInterface = graphicsInterface;
            m_Coordinator = coordinator;
        }

        public override void Enter()
        {
            m_GraphicsInterface.HideAll();
            m_GraphicsInterface.CleanupTextProcessorAndClearText();

            m_Coordinator.SetEnemyStatPanelProperties();
            m_Coordinator.SetPlayerStatPanelAndAbilityMenuProperties();

            m_Coordinator.StartCoroutine(WildPokemonBattleStartSequence());
        }

        private IEnumerator WildPokemonBattleStartSequence()
        {
            yield return new Parallel(m_Coordinator,
                m_GraphicsInterface.ShowEnumerator<PlayerPanel>(),
                m_GraphicsInterface.ShowEnumerator<PlayerTrainerPanel>(),
                m_GraphicsInterface.ShowEnumerator<EnemyPanel>(),
                m_GraphicsInterface.ShowEnumerator<EnemyPokemonPanel>());

            yield return new Parallel(m_Coordinator, PrintEncounterNameCharByChar(),
                m_GraphicsInterface.ShowEnumerator<EnemyStatsPanel>());

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            yield return new Parallel(m_Coordinator, PrintSummonNameCharByChar(),
                m_GraphicsInterface.HideEnumerator<PlayerTrainerPanel>());

            yield return new Parallel(m_Coordinator,
                m_GraphicsInterface.ShowEnumerator<PlayerPokemonPanel>(),
                m_GraphicsInterface.ShowEnumerator<PlayerStatsPanel>());

            yield return m_DelayForHalfSecond;

            m_Coordinator.ChangeState(BattleState.Wait);
        }

        private IEnumerator PrintEncounterNameCharByChar()
        {
            m_Coordinator.GetEnemyActivePokemon(out var combatant);

            string message = string.Concat($"A wild {combatant.name.ToUpper()} appeared!");

            yield return m_GraphicsInterface.TypeTextCharByChar(message);
        }

        private IEnumerator PrintSummonNameCharByChar()
        {
            m_Coordinator.GetPlayerActivePokemon(out var combatant);

            string message = string.Concat($"Go! {combatant.name.ToUpper()}!");

            yield return m_GraphicsInterface.TypeTextCharByChar(message);
        }

        public override void Exit()
        {
            m_Coordinator.StopCoroutine(WildPokemonBattleStartSequence());
        }
    }
}