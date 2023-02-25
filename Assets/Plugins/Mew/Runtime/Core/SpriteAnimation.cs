using UnityEngine;

namespace Mew
{
    [CreateAssetMenu(fileName = "sprite-animation", menuName = "ScriptableObjects/Mew/Animation", order = 150)]
    internal sealed class SpriteAnimation : ScriptableObject
    {
        [SerializeField]
        private AnimationFrame[] m_Frames;
    }
}