using UnityEngine;
using Golem;
using Mew;

namespace Iris
{
	internal abstract class LeafStateBehaviour : StateBehaviour<LeafState>
	{
		protected SpriteAnimator m_Animator;
		protected Rigidbody2D m_Rigidbody2D;

		protected Leaf m_Owner;

		protected const string kHorizontalAxis = "Horizontal";
		protected const string kVerticalAxis = "Vertical";

		protected virtual void Awake()
		{
			m_Animator = GetComponentInParent<SpriteAnimator>();
			m_Rigidbody2D = GetComponentInParent<Rigidbody2D>();

			m_Owner = GetComponentInParent<Leaf>();
		}
	}
}
