using UnityEngine;

namespace Voltorb
{
    public abstract class Graphic : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }

    public abstract class Graphic<T> : Graphic where T : GraphicProperties
    {
        public virtual void SetProperties(T props)
        {

        }
    }
}
