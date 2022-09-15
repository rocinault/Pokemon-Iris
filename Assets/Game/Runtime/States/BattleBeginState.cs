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

        public BattleBeginState(T uniqueID, BattleGraphicsInterface graphicsInterface, BattleCoordinator coordinator) : base(uniqueID)
        {
            m_GraphicsInterface = graphicsInterface;
            m_Coordinator = coordinator;
        }

        public override void Enter()
        {
            m_GraphicsInterface.HideAll();
            m_GraphicsInterface.CleanupTextProcessorAndClearText();

            SetEnemyStatPanelProperties();
            SetPlayerStatPanelAndAbilityMenuProperties();

            m_Coordinator.StartCoroutine(WildPokemonBattleStartSequence());
        }

        internal IEnumerator WildPokemonBattleStartSequence()
        {
            yield return new Parallel(m_Coordinator,
                m_GraphicsInterface.ShowEnumerator<PlayerPanel>(),
                m_GraphicsInterface.ShowEnumerator<PlayerTrainerPanel>(),
                m_GraphicsInterface.ShowEnumerator<EnemyPanel>(),
                m_GraphicsInterface.ShowEnumerator<EnemyPokemonPanel>());

            //yield return new WaitForSeconds(0.15f);

            yield return new Parallel(m_Coordinator,
                m_GraphicsInterface.ShowEnumerator<EnemyStatsPanel>(),
                PrintEncounterNameCharByChar());

            //yield return new WaitForSeconds(0.5f);

            yield return new Sequence(m_Coordinator,
                m_GraphicsInterface.HideEnumerator<PlayerTrainerPanel>(),
                m_GraphicsInterface.ShowEnumerator<PlayerPokemonPanel>(),
                m_GraphicsInterface.ShowEnumerator<PlayerStatsPanel>()
                );

            m_Coordinator.ChangeState(BattleState.wait);
        }

        private void SetEnemyStatPanelProperties()
        {
            m_Coordinator.TryGetEnemyActivePokemon(out var pokemon);

            var props = PokemonGraphicProperties.CreateProperties(pokemon);
            m_GraphicsInterface.SetProperties(typeof(EnemyStatsPanel).Name, props);
            m_GraphicsInterface.SetProperties(typeof(EnemyPokemonPanel).Name, props);
        }

        private void SetPlayerStatPanelAndAbilityMenuProperties()
        {
            m_Coordinator.TryGetPlayerActivePokemon(out var pokemon);

            var props = PokemonGraphicProperties.CreateProperties(pokemon);
            m_GraphicsInterface.SetProperties(typeof(PlayerStatsPanel).Name, props);
            m_GraphicsInterface.SetProperties(typeof(PlayerPokemonPanel).Name, props);
            m_GraphicsInterface.SetProperties(typeof(AbilitiesMenu).Name, props);
        }

        private IEnumerator PrintEncounterNameCharByChar()
        {
            m_Coordinator.TryGetEnemyActivePokemon(out var pokemon);

            string message = string.Concat($"A wild {pokemon.name.ToUpper()} appeared!");

            yield return m_GraphicsInterface.TypeTextCharByChar(message);
        }
    }
}

/*

            var sequence = new Sequence();

            sequence.Build(
                new Parallel().Build(
                    (Routine)m_GraphicsInterface.Show<PlayerPanel>(),
                    (Routine)m_GraphicsInterface.Show<EnemyPanel>()),
                (Routine)m_GraphicsInterface.Show<EnemyStatsPanel>()
                //new Slowbro.WaitUntil(PrintEncounterNameCharByChar())

                );

            m_Coordinator.StartCoroutine(sequence.Run());

var sequence = new Sequence();

            sequence.Build(
                m_GraphicsInterface.ShowEnumerator<PlayerPanel>(),
                m_GraphicsInterface.ShowEnumerator<EnemyPanel>()
                );

            m_Coordinator.StartCoroutine(sequence.Run());

var sequence = new Sequence();

            sequence.Build(
                (Routine)m_GraphicsInterface.Show<PlayerPanel>(),
                (Routine)m_GraphicsInterface.Show<EnemyPanel>()
                );

            m_Coordinator.StartCoroutine(sequence.Run());
 */

/*

m_Coordinator.StartCoroutine(WildPokemonEncounterSequence());

        // Todo find way to get the timing right without adding too much spaghetti code.
        private IEnumerator WildPokemonEncounterSequence()
        {
            ClearTextHideAllPanelsAndMenus();

            SetEnemyStatPanelProperties();
            SetPlayerStatPanelAndAbilityMenuProperties();

            ShowPlayerAndEnemyPanels();

            yield return new WaitForSeconds(1.5f);

            ShowEnemyStatsPanel();
            PrintEncounterNameCharByChar();

            yield return new WaitForSeconds(1f);

            HidePlayerTrainerAndSendOutPokemon();

            yield return new WaitForSeconds(1.25f);

            ShowPlayerPokemonAndStatsPanel();

            yield return new WaitForSeconds(0.5f);

            m_Coordinator.ChangeState(BattleState.wait);
        }

        private void ClearTextHideAllPanelsAndMenus()
        {
            m_GraphicsInterface.CleanupTextProcessorAndClearText();
            m_GraphicsInterface.HideAll();
        }

        private void SetEnemyStatPanelProperties()
        {
            m_Coordinator.TryGetEnemyActivePokemon(out var pokemon);

            var props = PokemonGraphicProperties.CreateProperties(pokemon);
            m_GraphicsInterface.SetProperties(typeof(EnemyStatsPanel).Name, props);
            m_GraphicsInterface.SetProperties(typeof(EnemyPokemonPanel).Name, props);
        }

        private void SetPlayerStatPanelAndAbilityMenuProperties()
        {
            m_Coordinator.TryGetPlayerActivePokemon(out var pokemon);

            var props = PokemonGraphicProperties.CreateProperties(pokemon);
            m_GraphicsInterface.SetProperties(typeof(PlayerStatsPanel).Name, props);
            m_GraphicsInterface.SetProperties(typeof(PlayerPokemonPanel).Name, props);
            m_GraphicsInterface.SetProperties(typeof(AbilitiesMenu).Name, props);
        }

        private void ShowPlayerAndEnemyPanels()
        {
            m_GraphicsInterface.Show<PlayerPanel>();
            m_GraphicsInterface.Show<PlayerTrainerPanel>();

            m_GraphicsInterface.Show<EnemyPanel>();
            m_GraphicsInterface.Show<EnemyPokemonPanel>();
        }

        private void ShowEnemyStatsPanel()
        {
            m_GraphicsInterface.Show<EnemyStatsPanel>();
        }

        private void PrintEncounterNameCharByChar()
        {
            m_Coordinator.TryGetEnemyActivePokemon(out var pokemon);

            string message = string.Concat($"A wild {pokemon.name.ToUpper()} appeared!");

            m_GraphicsInterface.PrintTextCharByChar(message);
        }

        private void HidePlayerTrainerAndSendOutPokemon()
        {
            m_GraphicsInterface.Hide<PlayerTrainerPanel>();
        }

        private void ShowPlayerPokemonAndStatsPanel()
        {
            m_GraphicsInterface.Show<PlayerPokemonPanel>();
            m_GraphicsInterface.Show<PlayerStatsPanel>();
        }
 */ 