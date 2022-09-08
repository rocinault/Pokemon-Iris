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
        private Trainer m_Player;

        [SerializeField]
        private Trainer m_Enemy;

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

        internal bool TryGetPlayerActivePokemon(out Pokemon pokemon)
        {
            return m_Player.TryGetActivePokemon(out pokemon);
        }

        internal bool TryGetEnemyActivePokemon(out Pokemon pokemon)
        {
            return m_Enemy.TryGetActivePokemon(out pokemon);
        }

        internal void PrintTextCharByChar(string message)
        {
            m_GraphicsInterface.PrintTextCharByChar(message);
        }

        internal void AddFightMoveToRuntimeSet<T>(T item) where T : AbilitySpec
        {
            m_Player.TryGetActivePokemon(out var instigator);
            m_Enemy.TryGetActivePokemon(out var target);

            var fight = new Fight(this, instigator, target, item);

            m_MoveRuntimeSet.Add(fight);
        }

        internal MoveRuntimeSet GetMoveRuntimeSet()
        {
            return m_MoveRuntimeSet;
        }
    }
}