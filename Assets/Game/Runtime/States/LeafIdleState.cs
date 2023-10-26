using UnityEngine;
using Mew;

namespace Iris
{
    internal sealed class LeafIdleState : LeafLocomotionState
    {
        public override LeafState UniqueId => LeafState.Idle;

        [SerializeField]
        private SimpleDirectionalBlendTree m_Idle;

        public override void Enter()
        {
            base.Enter();

            m_Animator.Play(m_Idle);
        }

        protected override void Update()
        {
            CaptureInputAndClampAxis();

            if (CheckTransitionToWalkState())
            {
                SetSpriteAnimatorParameters();
                SetDirectionToInputIfNotZero();
                CalculateMovementTarget();

                m_Owner.ChangeState(LeafState.Walk);
            }
        }
    }
}
