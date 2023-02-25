using UnityEngine;

namespace Iris
{
    internal sealed class Player : Character
    {
        private Vector2 m_Input = Vector2.zero;

        private static int s_Horizontal = Animator.StringToHash(kHorizontalAxis);
        private static int s_Vertical = Animator.StringToHash(kVerticalAxis);

        private const string kHorizontalAxis = "Horizontal";
        private const string kVerticalAxis = "Vertical";

        protected override void Update()
        {
            //transform.position += Vector3.right * 4f * Time.deltaTime;

            if (Vector2.Distance(m_Rigidbody2D.position, m_Target) <= float.Epsilon)
            {
                CaptureInputAndClampAxis();
            }
        }

        private void CaptureInputAndClampAxis()
        {
            m_Input = new Vector2(Input.GetAxisRaw(kHorizontalAxis), Input.GetAxisRaw(kVerticalAxis));

            if (m_Input.y != 0f)
            {
                m_Input.x = 0f;
            }
        }


    }
}
