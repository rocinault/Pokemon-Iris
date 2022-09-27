using UnityEngine;

using Golem;
using Umbreon;

namespace Iris
{
    internal sealed class BattleCoordinator : MonoBehaviour
    {
        [SerializeField]
        private BattleGraphicsInterface m_GraphicsInterface;

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

        private static readonly StateMachine<BattleState> s_StateMachine = new StateMachine<BattleState>();

        private void Awake()
        {
            CreateBattleStates();
            SetBattleEnterState();
        }

        private void CreateBattleStates()
        {
            var states = new IState<BattleState>[]
            {
                new BattleBeginState<BattleState>(BattleState.Begin, m_GraphicsInterface, this),
                new BattleWaitState<BattleState>(BattleState.Wait, m_GraphicsInterface, this),
                new BattleActionState<BattleState>(BattleState.Action, m_GraphicsInterface, this),
                new BattleWonState<BattleState>(BattleState.Won, m_GraphicsInterface, this),
                new BattleLostState<BattleState>(BattleState.Lost, m_GraphicsInterface, this)
            };

            s_StateMachine.AddStatesToStateMachine(states);
        }

        private void SetBattleEnterState()
        {
            s_StateMachine.SetCurrentStateID(BattleState.Begin);
        }

        private void Start()
        {
            StartBattleStateMachine();
        }

        private void StartBattleStateMachine()
        {
            s_StateMachine.Start();
        }

        internal void ChangeState(BattleState stateToTransitionInto)
        {
            s_StateMachine.ChangeState(stateToTransitionInto);
        }

        internal void SetPlayerActivePokemon()
        {
            m_PlayerCombatant.affinity = Affinity.Friendly;
            m_PlayerCombatant.pokemon = m_Player.GetFirstPokemonThatIsNotFainted();
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

        internal void GetEnemyActivePokemon(out Pokemon pokemon)
        {
            pokemon = m_EnemyCombatant.pokemon;
        }

        internal void SetEnemyStatPanelProperties()
        {
            var props = PokemonGraphicProperties.CreateProperties(m_EnemyCombatant.pokemon);
            m_GraphicsInterface.SetProperties(typeof(EnemyStatsPanel).Name, props);
            m_GraphicsInterface.SetProperties(typeof(EnemyPokemonPanel).Name, props);
        }

        internal void SetPlayerStatPanelAndAbilityMenuProperties()
        {
            var props = PokemonGraphicProperties.CreateProperties(m_PlayerCombatant.pokemon);
            m_GraphicsInterface.SetProperties(typeof(PlayerStatsPanel).Name, props);
            m_GraphicsInterface.SetProperties(typeof(PlayerPokemonPanel).Name, props);
            m_GraphicsInterface.SetProperties(typeof(AbilitiesMenu).Name, props);
            m_GraphicsInterface.SetProperties(typeof(LevelUpMenu).Name, props);
        }

        internal void AddFightMoveToRuntimeSet<T>(T item) where T : AbilitySpec
        {
            m_MoveRuntimeSet.Add(new Fight(m_GraphicsInterface, m_PlayerCombatant, m_EnemyCombatant, item));

            // Enemy Attack
            m_MoveRuntimeSet.Add(new Fight(m_GraphicsInterface, m_EnemyCombatant, m_PlayerCombatant, m_EnemyCombatant.pokemon.GetAllAbilities()[0]));
        }

        internal MoveRuntimeSet GetMoveRuntimeSet()
        {
            return m_MoveRuntimeSet;
        }
    }
}