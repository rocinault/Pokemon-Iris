using System;
using System.Collections;

using UnityEngine;

using Golem;
using Umbreon;

namespace Iris
{
    internal sealed class BattleActionState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly BattleGraphicsInterface m_GraphicsInterface;
        private readonly BattleCoordinator m_Coordinator;

        public BattleActionState(T uniqueID, BattleGraphicsInterface graphicsInterface, BattleCoordinator coordinator) : base(uniqueID)
        {
            m_GraphicsInterface = graphicsInterface;
            m_Coordinator = coordinator;
        }

        public override void Enter()
        {
            var moveRuntimeSet = m_Coordinator.GetMoveRuntimeSet();
            moveRuntimeSet.Sort();

            m_GraphicsInterface.CleanupTextProcessorAndClearText();
            m_Coordinator.StartCoroutine(RunAllSelectedActionsInPriority());
        }

        private IEnumerator RunAllSelectedActionsInPriority()
        {
            var moveRuntimeSet = m_Coordinator.GetMoveRuntimeSet();

            int count = moveRuntimeSet.Count();

            for (int i = 0; i < count; i++)
            {
                yield return moveRuntimeSet[i].Run();

                var target = moveRuntimeSet[i].target;

                if (target.pokemon.health.value <= 0)
                {
                    switch (target.affinity)
                    {
                        case Affinity.hostile:
                            m_Coordinator.ChangeState(BattleState.won);
                            yield break;
                        case Affinity.friendly:
                            m_Coordinator.ChangeState(BattleState.lost);
                            yield break;
                    }
                }
            }

            m_Coordinator.ChangeState(BattleState.wait);
        }

        public override void Exit()
        {
            var moveRuntimeSet = m_Coordinator.GetMoveRuntimeSet();
            moveRuntimeSet.Clear();

            m_Coordinator.StopCoroutine(RunAllSelectedActionsInPriority());
            m_GraphicsInterface.CleanupTextProcessorAndClearText();
        }
    }
}
