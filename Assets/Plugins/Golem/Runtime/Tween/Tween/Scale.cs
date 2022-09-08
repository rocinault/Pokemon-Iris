using UnityEngine;

namespace Golem
{
    public sealed class Scale : TweenCoroutine
    {
        private readonly Transform m_Transform;

        private Vector3 m_StartScale;
        private Vector3 m_EndScale;

        private Vector3 m_TargetScale;

        public override bool keepWaiting => Update();

        public Scale(Transform transform)
        {
            m_Transform = transform;
        }

        protected override void Initialise()
        {
            switch (m_Space)
            {
                case Space.World:
                    m_EndScale = m_TargetScale;
                    break;
                case Space.Self:
                    m_EndScale = m_StartScale + m_TargetScale;
                    break;
            }
        }

        protected override bool Update()
        {
            base.Update();

            m_Transform.localScale = new Vector3(Mathf.LerpUnclamped(m_StartScale.x, m_EndScale.x, Easing.Resolve(m_EasingType, time)),
                Mathf.LerpUnclamped(m_StartScale.y, m_EndScale.y, Easing.Resolve(m_EasingType, time)), 1f);

            return IsComplete();
        }

        public Scale SetDuration(float duration)
        {
            m_Duration = duration;
            return this;
        }

        public Scale SetStart(Vector3 startScale)
        {
            m_StartScale = startScale;
            return this;
        }

        public Scale SetTarget(Vector3 targetScale)
        {
            m_TargetScale = targetScale;
            return this;
        }
    }
}
