using System;
using UnityEngine;

namespace Mew
{
	[Serializable]
	public sealed class SpriteAnimation
	{
		[SerializeField]
		internal Sprite[] sprites;

        internal Sprite this[int index]
        {
            get => sprites[index];
        }

        internal float length
        {
            get => sprites.Length;
        }
    }
}
