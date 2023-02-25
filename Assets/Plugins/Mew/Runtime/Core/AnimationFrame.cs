using System;

using UnityEngine;

namespace Mew
{
    [Serializable]
    internal sealed class AnimationFrame
    {
        [SerializeField]
        private Sprite m_Sprite;

        internal Sprite sprite
        {
            get => m_Sprite;
        }
    }
}