using UnityEngine;
using UnityEngine.EventSystems;

namespace Iris
{
    internal static class UnityEventSystemUtility
    {
        internal static EventSystem current
        {
            get
            {
                if (m_Current == null)
                {
                    m_Current = EventSystem.current;
                }

                return m_Current;
            }
        }

        private static EventSystem m_Current;

        internal static void SetSelectedGameObject(GameObject selected)
        {
            current.SetSelectedGameObject(selected);
        }
    }
}
