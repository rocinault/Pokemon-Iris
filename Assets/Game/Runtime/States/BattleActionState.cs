using System;
using System.Collections;

using UnityEngine;

using Golem;

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
            m_Coordinator.StartCoroutine(RunAllSelectedActionsInPriority());
        }

        private IEnumerator RunAllSelectedActionsInPriority()
        {
            var moveRuntimeSet = m_Coordinator.GetMoveRuntimeSet();
            moveRuntimeSet.Sort();

            for (int i = 0; i < moveRuntimeSet.Count(); i++)
            {
                yield return moveRuntimeSet[i].Run();
            }
        }

    }
}
