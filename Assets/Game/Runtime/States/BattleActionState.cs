using System;

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
            RunAllSelectedActionsInPriority();   
        }

        private void RunAllSelectedActionsInPriority()
        {
            // Activate the ability, check whether it was successful
            // -> display the correct dialogue.
            // -> Perform the tweening and effects.

            var moveRuntimeSet = m_Coordinator.GetMoveRuntimeSet();

            moveRuntimeSet.Sort();

            var sequence = new SequenceCoroutine();

            sequence.Build(moveRuntimeSet.ToArray());

            CoroutineUtility.StartCoroutine(sequence.Run());
        }

    }
}
