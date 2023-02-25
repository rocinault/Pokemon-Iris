using UnityEngine;

namespace Eevee
{
    public abstract class AbilitySpec
    {
        public readonly ScriptableAbility asset;

        public AbilitySpec(ScriptableAbility asset)
        {
            this.asset = asset;
        }
    }
}
