using System;
using System.Collections;

using UnityEngine;

using Golem;
using Umbreon;

namespace Iris
{
    internal sealed class BattleActionState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly GameBattleStateBehaviour m_StateBehaviour;

        private BattleGraphicsInterface m_Interface;

        public BattleActionState(T uniqueID, GameBattleStateBehaviour stateBehaviour) : base(uniqueID)
        {
            m_StateBehaviour = stateBehaviour;
        }

        public override void Enter()
        {
            m_Interface = m_StateBehaviour.GetBattleGraphicsInterface();

            var moveRuntimeSet = m_StateBehaviour.GetMoveRuntimeSet();
            moveRuntimeSet.Sort();

            m_Interface.CleanupTextProcessorAndClearText();
            m_StateBehaviour.StartCoroutine(RunAllSelectedActionsInPriority());
        }

        private IEnumerator RunAllSelectedActionsInPriority()
        {
            var moveRuntimeSet = m_StateBehaviour.GetMoveRuntimeSet();

            for (int i = 0; i < moveRuntimeSet.Count(); i++)
            {
                yield return moveRuntimeSet[i].Run();

                var target = moveRuntimeSet[i].target;

                if (target.pokemon.isFainted)
                {
                    switch (target.affinity)
                    {
                        case Affinity.Hostile:
                            m_StateBehaviour.ChangeState(BattleState.Won);
                            yield break;
                        case Affinity.Friendly:
                            m_StateBehaviour.ChangeState(BattleState.Lost);
                            yield break;
                    }
                }
            }

            m_StateBehaviour.ChangeState(BattleState.Wait);
        }

        public override void Exit()
        {
            var moveRuntimeSet = m_StateBehaviour.GetMoveRuntimeSet();
            moveRuntimeSet.Clear();

            m_StateBehaviour.StopCoroutine(RunAllSelectedActionsInPriority());
            m_Interface.CleanupTextProcessorAndClearText();
        }
    }
}
