using UnityEngine;
using UnityEngine.EventSystems;

namespace Voltorb
{
    [RequireComponent(typeof(RectTransform))]
    internal sealed class MenuCursorNavigation : MonoBehaviour
    {
        private RectTransform rectTransform
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

        public void Select()
        {

        }

        public void OnSelect(GameObject selectedObject)
        {
            rectTransform.anchoredPosition = selectedObject.GetComponent<RectTransform>().anchoredPosition;
        }

        public void OnDeselect(GameObject lastSelectedObject)
        {

        }
    }
}
