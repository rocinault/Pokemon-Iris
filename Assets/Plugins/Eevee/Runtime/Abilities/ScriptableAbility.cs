using UnityEngine;

namespace Eevee
{
    public abstract class ScriptableAbility : ScriptableObject
    {
        public abstract AbilitySpec CreateAbilitySpec();
    }
}
