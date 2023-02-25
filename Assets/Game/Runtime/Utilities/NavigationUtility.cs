using UnityEngine;

namespace Iris
{
    internal sealed class NavigationUtility : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_LastNavigationObject;

        [SerializeField]
        private GameObject m_CurrentNavigationObject;

        internal void OnShow()
        {
            UnityEventSystemUtility.SetSelectedGameObject(m_CurrentNavigationObject);
        }

        internal void OnHide()
        {
            UnityEventSystemUtility.SetSelectedGameObject(m_LastNavigationObject);
        }
    }
}
