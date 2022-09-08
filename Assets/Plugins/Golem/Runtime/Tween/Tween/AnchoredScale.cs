using UnityEngine;

namespace Golem
{
    public sealed class AnchoredScale : TweenCoroutine
    {
        private readonly RectTransform m_RectTransform;

        private Vector3 m_StartScale;
        private Vector3 m_EndScale;

        private Vector3 m_TargetScale;

        public override bool keepWaiting => Update();

        public AnchoredScale(RectTransform rectTransform)
        {
            m_RectTransform = rectTransform;
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

            m_RectTransform.localScale = new Vector3(LerpAndRoundToNearestDecimal(m_StartScale.x, m_EndScale.x, Easing.Resolve(m_EasingType, time)),
                LerpAndRoundToNearestDecimal(m_StartScale.y, m_EndScale.y, Easing.Resolve(m_EasingType, time)), 1f);

            return IsComplete();
        }

        private static float LerpAndRoundToNearestDecimal(float a, float b, float time)
        {
            return Mathf.RoundToInt(Mathf.LerpUnclamped(a, b, time) * 100f) / 100f;
        }

        public AnchoredScale SetDuration(float duration)
        {
            m_Duration = duration;
            return this;
        }

        public AnchoredScale SetStart(Vector3 startScale)
        {
            m_StartScale = startScale;
            return this;
        }

        public AnchoredScale SetTarget(Vector3 targetScale)
        {
            m_TargetScale = targetScale;
            return this;
        }
    }
}
