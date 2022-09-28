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

            m_Coordinator.SetEnemyActivePokemon();
            m_Coordinator.SetPlayerActivePokemon();

            m_Coordinator.SetEnemyStatPanelProperties();
            m_Coordinator.SetPlayerStatPanelAndAbilityMenuProperties();

            m_Coordinator.StartCoroutine(WildPokemonBattleStartSequence());
        }

        private IEnumerator WildPokemonBattleStartSequence()
        {
            yield return new Parallel(m_Coordinator,
                m_GraphicsInterface.ShowAsync<PlayerPanel>(),
                m_GraphicsInterface.ShowAsync<PlayerTrainerPanel>(),
                m_GraphicsInterface.ShowAsync<EnemyPanel>(),
                m_GraphicsInterface.ShowAsync<EnemyPokemonPanel>());

            yield return new Parallel(m_Coordinator, PrintEncounterNameCharByChar(),
                m_GraphicsInterface.ShowAsync<EnemyStatsPanel>());

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            yield return new Parallel(m_Coordinator, PrintSummonNameCharByChar(),
                m_GraphicsInterface.HideAsync<PlayerTrainerPanel>());

            yield return new Parallel(m_Coordinator,
                m_GraphicsInterface.ShowAsync<PlayerPokemonPanel>(),
                m_GraphicsInterface.ShowAsync<PlayerStatsPanel>());

            yield return m_DelayForHalfSecond;

            m_Coordinator.ChangeState(BattleState.Wait);
        }

        private IEnumerator PrintEncounterNameCharByChar()
        {
            m_Coordinator.GetEnemyActivePokemon(out var combatant);

            string message = string.Concat($"A wild {combatant.name} appeared!");

            yield return m_GraphicsInterface.TypeTextCharByChar(message);
        }

        private IEnumerator PrintSummonNameCharByChar()
        {
            m_Coordinator.GetPlayerActivePokemon(out var combatant);

            string message = string.Concat($"Go! {combatant.name}!");

            yield return m_GraphicsInterface.TypeTextCharByChar(message);
        }

        public override void Exit()
        {
            m_Coordinator.StopCoroutine(WildPokemonBattleStartSequence());
        }
    }
}