using System;

using UnityEngine;

namespace Umbreon
{
    public enum EffectType
    {
        Direct,
        Temporary
    }

    public enum AttributeModifierType
    {
        Add,
        Subtract
    }

    public sealed class ScriptableEffect : ScriptableObject
    {
        [SerializeField]
        private Container m_Container = new Container();

        public Container container
        {
            get => m_Container;
        }
    }

    [Serializable]
    public struct Container
    {
        [SerializeField]
        internal EffectType Type;

        [SerializeField]
        internal EffectModifiers[] Modifiers;

        [SerializeField]
        internal float Duration;
    }

    [Serializable]
    public struct EffectModifiers
    {
        [SerializeField]
        internal AttributeType Attribute;

        [SerializeField]
        internal AttributeModifierType Type;

        [SerializeField]
        internal float Multiplier;
    }
}
