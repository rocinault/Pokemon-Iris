using UnityEngine;

using Umbreon;
using Voltorb;

namespace Iris
{
    internal sealed class BattleGraphicsInterface : GraphicalUserInterface
    {
        [SerializeField]
        private MovesMenu m_MovesMenu;

        [SerializeField]
        private AbilitiesMenu m_AbilitiesMenu;

        [SerializeField]
        private PlayerPanel m_PlayerPanel;

        [SerializeField]
        private PlayerStatsPanel m_PlayerStatsPanel;

        [SerializeField]
        private PlayerTrainerPanel m_PlayerTrainerPanel;

        [SerializeField]
        private PlayerPokemonPanel m_PlayerPokemonPanel;

        [SerializeField]
        private EnemyPanel m_EnemyPanel;

        [SerializeField]
        private EnemyStatsPanel m_EnemyStatsPanel;

        [SerializeField]
        private EnemyPokemonPanel m_EnemyPokemonPanel;

        [SerializeField]
        private TextProcessor m_TextProcessor;

        protected override void BindSceneGraphicReferences()
        {
            Add(m_MovesMenu);
            Add(m_AbilitiesMenu);

            Add(m_PlayerPanel);
            Add(m_PlayerStatsPanel);
            Add(m_PlayerTrainerPanel);
            Add(m_PlayerPokemonPanel);

            Add(m_EnemyPanel);
            Add(m_EnemyStatsPanel);
            Add(m_EnemyPokemonPanel);
        }

        internal void CleanupTextProcessorAndClearText()
        {
            m_TextProcessor.CleanupAndClearAllText();
        }

        internal System.Collections.IEnumerator TypeTextCharByChar(string text)
        {
            yield return m_TextProcessor.TypeTextCharByChar(text);
        }

        internal void PrintTextCharByChar(string text)
        {
            m_TextProcessor.PrintTextCharByChar(text);
        }

        internal void PrintCompletedText(string text)
        {
            m_TextProcessor.PrintCompletedText(text);
        }

        
    }
}

/*

            Show<PlayerPanel>();
            Show<EnemyPanel>();

            yield return new Parallel().Build(
                    m_PlayerPanel.rectTransform.Move(new Vector3(256f, 0f), Vector3.zero, 1.35f, Space.World, EasingType.linear),
                    m_EnemyPanel.rectTransform.Move(new Vector3(-256f, 0f), Vector3.zero, 1.35f, Space.World, EasingType.linear)).Run();

            Show<EnemyStatsPanel>();

            yield return new Parallel().Build(
                m_EnemyStatsPanel.rectTransform.Move(new Vector3(-128f, 0f), Vector3.zero, 0.5f, Space.World, EasingType.linear)
                ).Run();

 */ 