using UnityEngine;
using UnityEngine.UI;

namespace Umbreon
{
    public enum Affinity
    {
        friendly,
        hostile
    }

    [RequireComponent(typeof(RectTransform), typeof(Image))]
    public class Combatant : MonoBehaviour
    {
        public Affinity affinity
        {
            get => m_Affinity;
            set => m_Affinity = value;
        }

        private Affinity m_Affinity;

        public Pokemon pokemon
        {
            get
            {
#if UNITY_EDITOR
                if (m_Pokemon == null)
                {
                    throw new System.Exception($"Pokemon has not been set");
                }
#endif
                return m_Pokemon;
            }
            set
            {
                m_Pokemon = value;
            }
        }

        private Pokemon m_Pokemon;

        public RectTransform rectTransform
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

        public Image image
        {
            get
            {
                if (m_Image == null)
                {
                    m_Image = GetComponent<Image>();
                }

                return m_Image;
            }
        }

        private Image m_Image;
    }
}