using UnityEngine;
using UnityEngine.UI;

namespace Voltorb
{
    public abstract class Menu : MonoBehaviour
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

    public abstract class Menu<T> : Menu
    {
        public abstract void SetProperties(T properties);
    }
}
