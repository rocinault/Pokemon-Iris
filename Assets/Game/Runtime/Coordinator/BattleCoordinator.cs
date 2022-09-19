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
        private Trainer m_PlayerTrainer;

        [SerializeField]
        private Combatant m_PlayerCombatant;

        [SerializeField]
        private Trainer m_EnemyTrainer;

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
                new BattleBeginState<BattleState>(BattleState.begin, m_GraphicsInterface, this),
                new BattleWaitState<BattleState>(BattleState.wait, m_GraphicsInterface, this),
                new BattleActionState<BattleState>(BattleState.action, m_GraphicsInterface, this),
                new BattleWonState<BattleState>(BattleState.won, m_GraphicsInterface, this),
                new BattleLostState<BattleState>(BattleState.lost, m_GraphicsInterface, this)
            };

            s_StateMachine.AddStatesToStateMachine(states);
        }

        private void SetBattleEnterState()
        {
            s_StateMachine.SetCurrentStateID(BattleState.begin);
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

        internal void GetPlayerActiveCombatant(out Combatant combatant)
        {
            combatant = m_PlayerCombatant;
        }

        internal void GetEnemyActiveCombatant(out Combatant combatant)
        {
            combatant = m_EnemyCombatant;
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

/*

        internal void GetPlayerActivePokemon(out Pokemon pokemon)
        {
            pokemon = m_PlayerCombatant.pokemon;
        }

        internal void GetEnemyActivePokemon(out Pokemon pokemon)
        {
            pokemon = m_EnemyCombatant.pokemon;
        }

 */ 