using UnityEngine;

namespace Voltorb
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class Panel : Graphic
    {
        internal protected RectTransform rectTransform
        {
            get
            {
                if (m_RectTransform == null)
                {
                    m_RectTransform = GetComponent<RectTransform>();
                }

                return m_RectTransform;
            }
        }

        private RectTransform m_RectTransform;

        protected virtual void AnimateEnterTransition()
        {

        }

        protected virtual void AnimateExitTransition()
        {

        }
    }

    [RequireComponent(typeof(RectTransform))]
    public abstract class Panel<T> : Graphic<T> where T : GraphicProperties
    {
        internal protected RectTransform rectTransform
        {
            get
            {
                if (m_RectTransform == null)
                {
                    m_RectTransform = GetComponent<RectTransform>();
                }

                return m_RectTransform;
            }
        }

        private RectTransform m_RectTransform;

        protected bool m_IsDone = true;

        protected virtual void AnimateEnterTransition()
        {

        }

        protected virtual void AnimateExitTransition()
        {
            
        }
    }
}
