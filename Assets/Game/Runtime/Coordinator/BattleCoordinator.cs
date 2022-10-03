using UnityEngine;

using Golem;
using Umbreon;

namespace Iris
{
    internal sealed class BattleCoordinator : MonoBehaviour
    {
        [SerializeField]
        private BattleGraphicsInterface m_Interface;

        [SerializeField]
        private MoveRuntimeSet m_MoveRuntimeSet;

        [SerializeField]
        private Summoner m_Player;

        [SerializeField]
        private Combatant m_PlayerCombatant;

        [SerializeField]
        private Summoner m_Enemy;

        [SerializeField]
        private Combatant m_EnemyCombatant;

        internal void SetPlayerActivePokemon()
        {
            m_PlayerCombatant.affinity = Affinity.Friendly;
            m_PlayerCombatant.pokemon = m_Player.GetFirstPokemonThatIsNotFainted();
        }

        internal void GetPlayerCombatant(out Combatant combatant)
        {
            combatant = m_PlayerCombatant;
        }

        internal void GetPlayerActivePokemon(out Pokemon pokemon)
        {
            pokemon = m_PlayerCombatant.pokemon;
        }

        internal void SetEnemyActivePokemon()
        {
            m_EnemyCombatant.affinity = Affinity.Hostile;
            m_EnemyCombatant.pokemon = m_Enemy.GetFirstPokemonThatIsNotFainted();
        }

        internal void GetEnemyCombatant(out Combatant combatant)
        {
            combatant = m_EnemyCombatant;
        }

        internal void GetEnemyActivePokemon(out Pokemon pokemon)
        {
            pokemon = m_EnemyCombatant.pokemon;
        }

        internal void SetEnemyStatPanelProperties()
        {
            var props = PokemonGraphicProperties.CreateProperties(m_EnemyCombatant.pokemon);
            m_Interface.SetProperties(typeof(EnemyStatsPanel).Name, props);
            m_Interface.SetProperties(typeof(EnemyPokemonPanel).Name, props);
        }

        internal void SetPlayerStatPanelAndAbilityMenuProperties()
        {
            var props = PokemonGraphicProperties.CreateProperties(m_PlayerCombatant.pokemon);
            m_Interface.SetProperties(typeof(PlayerStatsPanel).Name, props);
            m_Interface.SetProperties(typeof(PlayerPokemonPanel).Name, props);
            m_Interface.SetProperties(typeof(AbilitiesMenu).Name, props);
            m_Interface.SetProperties(typeof(LevelUpMenu).Name, props);
        }

    }
}