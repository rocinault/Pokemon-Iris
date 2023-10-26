using UnityEngine;

namespace Mew
{
	[RequireComponent(typeof(SpriteRenderer))]
	public sealed class SpriteAnimator : MonoBehaviour
	{
        private SimpleDirectionalBlendTree m_SpriteBlendTree;
        private SpriteAnimation m_SpriteAnimation;
		private SpriteRenderer m_SpriteRenderer;

        private Vector3 m_RootDirection = Vector3.down;
        private Vector3 m_LastFramePosition = Vector3.zero;

        private float m_AnimationTime;

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
            m_AnimationTime += (transform.position - m_LastFramePosition).magnitude * 2f;

            m_SpriteRenderer.sprite = m_SpriteAnimation[Mathf.FloorToInt(m_AnimationTime % m_SpriteAnimation.length)];

            m_LastFramePosition = transform.position;
        }

        public void Play(SimpleDirectionalBlendTree blendTree)
        {
            m_SpriteBlendTree = blendTree;

            if (m_RootDirection == Vector3.up)
            {
                m_SpriteAnimation = m_SpriteBlendTree.up;
            }
            else if (m_RootDirection == Vector3.down)
            {
                m_SpriteAnimation = m_SpriteBlendTree.down;
            }
            else if (m_RootDirection == Vector3.left)
            {
                m_SpriteAnimation = m_SpriteBlendTree.left;
            }
            else if (m_RootDirection == Vector3.right)
            {
                m_SpriteAnimation = m_SpriteBlendTree.right;
            }
        }

        public void SetAnimatorRootDirection(float horizontal, float vertcial)
        {
            m_RootDirection = new Vector3(horizontal, vertcial);
        }
    }
}
