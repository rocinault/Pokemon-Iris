using UnityEngine;
using Mew;

namespace Iris
{
    internal sealed class LeafWalkState : LeafLocomotionState
    {
        public override LeafState UniqueId => LeafState.Walk;

        [SerializeField]
        private SimpleDirectionalBlendTree m_Walk;

        public override void Enter()
        {
            base.Enter();

            m_Animator.Play(m_Walk);
        }

        protected override void Update()
        {
            if (CheckTransitionToIdleState())
            {
                m_Owner.ChangeState(LeafState.Idle);
            }
        }

        protected override void FixedUpdate()
        {
            m_Rigidbody2D.MovePosition(Vector2.MoveTowards(m_Rigidbody2D.position, s_Target, kRunForce * Time.fixedDeltaTime));
        }
    }
}
