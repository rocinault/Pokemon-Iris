using UnityEngine;

namespace Voltorb
{
    public abstract class Graphic : MonoBehaviour
    {
        public virtual bool isAnimating
        {
            get => m_IsAnimating;
        }

        protected internal bool m_IsAnimating;

        public virtual System.Collections.IEnumerator Show()
        {
            gameObject.SetActive(true);
            yield break;
        }

        public virtual System.Collections.IEnumerator Hide()
        {
            gameObject.SetActive(false);
            yield break;
        }
    }

    public abstract class Graphic<T> : Graphic where T : GraphicProperties
    {
        public virtual void SetProperties(T props)
        {

        }
    }
}