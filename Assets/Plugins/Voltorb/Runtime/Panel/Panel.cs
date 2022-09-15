using UnityEngine;

namespace Voltorb
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class Panel : Graphic
    {
        protected internal RectTransform rectTransform
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
    }

    [RequireComponent(typeof(RectTransform))]
    public abstract class Panel<T> : Graphic<T> where T : GraphicProperties
    {
        protected internal RectTransform rectTransform
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
    }
}
