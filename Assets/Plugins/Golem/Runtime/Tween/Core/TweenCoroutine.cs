using UnityEngine;

namespace Golem
{
    public abstract class TweenCoroutine : YieldCoroutine
    {
        protected EasingType m_EasingType = EasingType.linear;
        protected Space m_Space = Space.World;

        protected float m_Duration = 1f;
        protected float m_TimeElapsed = 0f;

        protected float time => Mathf.Min(m_TimeElapsed / m_Duration, 1.0f);

        public TweenCoroutine()
        {

        }

        protected override void Initialise()
        {
            m_TimeElapsed = 0f;
        }

        protected override bool Update()
        {
            m_TimeElapsed += Time.deltaTime;

            return IsComplete();
        }

        protected virtual bool IsComplete()
        {
            return m_TimeElapsed < m_Duration;
        }

        public TweenCoroutine SetEasing(EasingType easing)
        {
            m_EasingType = easing;
            return this;
        }

        public TweenCoroutine SetSpace(Space space)
        {
            m_Space = space;
            return this;
        }
    }
}