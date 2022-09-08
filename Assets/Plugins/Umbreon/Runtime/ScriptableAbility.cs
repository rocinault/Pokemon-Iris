using UnityEngine;

namespace Umbreon
{
    public abstract class ScriptableAbility : ScriptableObject
    {
        [SerializeField]
        private uint m_Cost;

        [SerializeField]
        private uint m_Cooldown;

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