using UnityEngine;

namespace Mew
{
	[CreateAssetMenu(fileName = "new-blend-tree", menuName = "ScriptableObjects/Mew/Animation", order = 150)]
	public sealed class SimpleDirectionalBlendTree : ScriptableObject
	{
		[SerializeField]
		internal SpriteAnimation up;

		[SerializeField]
		internal SpriteAnimation down;

		[SerializeField]
		internal SpriteAnimation left;

		[SerializeField]
		internal SpriteAnimation right;
	}
}
