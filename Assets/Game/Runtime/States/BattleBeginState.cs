using System;
using System.Collections;

using UnityEngine;

using Golem;

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
            CoroutineUtility.StartCoroutine(WildPokemonEncounterSequence());
        }

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
    }
}
