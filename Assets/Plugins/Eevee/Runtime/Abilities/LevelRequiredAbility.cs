using System;

using UnityEngine;

namespace Eevee
{
    [Serializable]
    internal struct LevelRequiredAbility
    {
        [SerializeField]
        private ScriptableAbility m_Ability;

        [SerializeField]
        private int m_LevelRequired;

        internal ScriptableAbility ability
        {
            get => m_Ability;
        }

        internal int levelRequired
        {
            get => m_LevelRequired;
        }
    }
}
