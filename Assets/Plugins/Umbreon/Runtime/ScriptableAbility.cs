using UnityEngine;

namespace Umbreon
{
    public abstract class ScriptableAbility : ScriptableObject
    {
        [SerializeField]
        private string m_AbilityName;

        [SerializeField]
        private uint m_Power;

        [SerializeField]
        private uint m_Accuracy;

        [SerializeField]
        private uint m_Cost;

        [SerializeField]
        private uint m_Cooldown;

        public string abilityName
        {
            get => m_AbilityName;
        }

        public uint power
        {
            get => m_Power;
        }

        public uint accuracy
        {
            get => m_Accuracy;
        }

        internal uint cost
        {
            get => m_Cost;
        }

        internal uint cooldown
        {
            get => m_Cooldown;
        }

        public abstract AbilitySpec CreateAbilitySpec();
    }
}