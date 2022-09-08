using UnityEngine;

namespace Golem
{
    public sealed class Position : TweenCoroutine
    {
        private readonly Transform m_Transform;

        private Vector3 m_StartPosition;
        private Vector3 m_EndPosition;

        private Vector3 m_TargetPosition;

        public override bool keepWaiting => Update();

        public Position(Transform transform)
        {
            m_Transform = transform;
        }

        protected override void Initialise()
        {
            m_StartPosition = m_Transform.position;

            switch (m_Space)
            {
                case Space.World:
                    m_EndPosition = m_TargetPosition;
                    break;
                case Space.Self:
                    m_EndPosition = m_StartPosition + m_TargetPosition;
                    break;
            }
        }

        protected override bool Update()
        {
            base.Update();

            m_Transform.position = new Vector3(Mathf.LerpUnclamped(m_StartPosition.x, m_EndPosition.x, Easing.Resolve(m_EasingType, time)),
                Mathf.LerpUnclamped(m_StartPosition.y, m_EndPosition.y, Easing.Resolve(m_EasingType, time)));

            return IsComplete();
        }

        public Position SetDuration(float duration)
        {
            m_Duration = duration;
            return this;
        }

        public Position SetTarget(Vector3 targetPosition)
        {
            m_TargetPosition = targetPosition;
            return this;
        }
    }
}