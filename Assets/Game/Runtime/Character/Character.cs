using UnityEngine;

using Mew;

namespace Iris
{
    internal abstract class Character : MonoBehaviour
    {
        protected Rigidbody2D m_Rigidbody2D;
        protected SpriteAnimator m_SpriteAnimator;

        protected Vector2 m_Target = Vector2.zero;
        protected Vector2 m_Direction = Vector2.down;

        protected const float kWalkSpeed = 4f;
        protected const float kGridSize = 1f;

        protected virtual void Awake()
        {
            m_Rigidbody2D = GetComponentInChildren<Rigidbody2D>();
            m_SpriteAnimator = GetComponentInChildren<SpriteAnimator>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            
        }

        protected virtual void FixedUpdate()
        {
            
        }

    }
}
