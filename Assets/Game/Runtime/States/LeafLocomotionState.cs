using UnityEngine;

namespace Iris
{
	internal abstract class LeafLocomotionState : LeafStateBehaviour
	{
		protected static Vector2 s_Input;
		protected static Vector2 s_Target;
		protected static Vector2 s_Direction;

		protected const float kGridSize = 1f;

		protected const float kWalkForce = 3.625f;
		protected const float kRunForce = 8f;

		protected virtual void Start()
		{
            s_Target = m_Rigidbody2D.position;
        }

		protected virtual void Update()
		{

        }

        protected void CaptureInputAndClampAxis()
		{
			s_Input = new Vector2(Input.GetAxisRaw(kHorizontalAxis), Input.GetAxisRaw(kVerticalAxis));

			if (s_Input.y != 0f && s_Input.x != 0f)
			{
				s_Input.x = 0f;
			}

			//s_Input = Vector2.right;
		}

		protected void SetSpriteAnimatorParameters()
		{
			m_Animator.SetAnimatorRootDirection(s_Input.x, s_Input.y);
		}

		protected void SetDirectionToInputIfNotZero()
		{
			if (s_Input.sqrMagnitude != 0f)
			{
				s_Direction = s_Input;
            }
		}

        protected void CalculateMovementTarget()
        {
            s_Target = new Vector2(m_Rigidbody2D.position.x + (s_Input.x * kGridSize), m_Rigidbody2D.position.y + (s_Input.y * kGridSize));
		}

        protected virtual void FixedUpdate()
        {
           
        }

        protected bool CheckTransitionToIdleState()
        {
			return Vector2.Distance(m_Rigidbody2D.position, s_Target) <= float.Epsilon;
        }

		protected bool CheckTransitionToWalkState()
		{
            return s_Input.sqrMagnitude != 0f;
        }
    }
}
