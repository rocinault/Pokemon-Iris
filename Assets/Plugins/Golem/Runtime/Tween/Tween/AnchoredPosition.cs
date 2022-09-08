using UnityEngine;

namespace Golem
{
    public sealed class AnchoredPosition : TweenCoroutine
    {
        private readonly RectTransform m_RectTransform;

        private Vector3 m_StartPosition;
        private Vector3 m_EndPosition;

        private Vector3 m_TargetPosition;

        public override bool keepWaiting => Update();

        public AnchoredPosition(RectTransform rectTransform)
        {
            m_RectTransform = rectTransform;
        }

        protected override void Initialise()
        {
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

            m_RectTransform.anchoredPosition = new Vector2(LerpAndRoundToNearestDecimal(m_StartPosition.x, m_EndPosition.x, Easing.Resolve(m_EasingType, time)),
                LerpAndRoundToNearestDecimal(m_StartPosition.y, m_EndPosition.y, Easing.Resolve(m_EasingType, time)));

            return IsComplete();
        }

        private static float LerpAndRoundToNearestDecimal(float a, float b, float time)
        {
            return Mathf.RoundToInt(Mathf.LerpUnclamped(a, b, time) * 100f) / 100f;
        }

        public AnchoredPosition SetDuration(float duration)
        {
            m_Duration = duration;
            return this;
        }

        public AnchoredPosition SetStart(Vector3 startPosition)
        {
            m_StartPosition = startPosition;
            return this;
        }

        public AnchoredPosition SetTarget(Vector3 targetPosition)
        {
            m_TargetPosition = targetPosition;
            return this;
        }
    }
}
