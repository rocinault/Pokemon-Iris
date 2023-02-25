using UnityEngine;

namespace Mew
{
    public sealed class SpriteAnimator : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] m_Sprites;

        private SpriteRenderer m_SpriteRenderer;

        private Vector3 m_LastFramePosition;

        private int m_CurrentFrame;

        private void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            m_LastFramePosition = transform.position;
        }

        private void Update()
        {
            if (Vector2.Distance(m_LastFramePosition, transform.position) >= 0.5f)
            {
                if (m_LastFramePosition != transform.position)
                {
                    m_LastFramePosition = transform.position;

                    m_CurrentFrame++;

                    if (m_CurrentFrame > m_Sprites.Length - 1)
                    {
                        m_CurrentFrame = 0;
                    }

                    m_SpriteRenderer.sprite = m_Sprites[m_CurrentFrame];
                }
            }
        }
    }
}