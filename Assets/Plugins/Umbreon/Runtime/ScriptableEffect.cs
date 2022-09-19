using System;

using UnityEngine;

namespace Umbreon
{
    public enum EffectType
    {
        direct,
        temporary
    }

    public enum AttributeModifierType
    {
        target,
        self
    }

    public abstract class ScriptableEffect : ScriptableObject
    {
        public abstract EffectSpec CreateEffectSpec(ScriptableAbility asset);
    }

    [Serializable]
    public struct Container
    {
        [SerializeField]
        public EffectType type;

        [SerializeField]
        public uint power;

        [SerializeField]
        public uint accuracy;

        [SerializeField]
        public EffectModifiers[] modifiers;

        //[SerializeField]
        //internal uint duration;
    }

    [Serializable]
    public struct EffectModifiers
    {
        [SerializeField]
        internal AttributeType attribute;

        [SerializeField]
        internal AttributeModifierType type;

        [SerializeField]
        internal uint multiplier;
    }
}
