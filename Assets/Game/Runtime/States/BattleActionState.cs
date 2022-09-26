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

            for (int i = 0; i < moveRuntimeSet.Count(); i++)
            {
                yield return moveRuntimeSet[i].Run();

                var target = moveRuntimeSet[i].target;

                if (target.pokemon.isFainted)
                {
                    switch (target.affinity)
                    {
                        case Affinity.Hostile:
                            m_Coordinator.ChangeState(BattleState.Won);
                            yield break;
                        case Affinity.Friendly:
                            m_Coordinator.ChangeState(BattleState.Lost);
                            yield break;
                    }
                }
            }

            m_Coordinator.ChangeState(BattleState.Wait);
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
