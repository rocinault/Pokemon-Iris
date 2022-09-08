using UnityEngine;

using Umbreon;

namespace Iris
{
    [CreateAssetMenu]
    internal sealed class Tackle : ScriptableAbility
    {
        public override AbilitySpec CreateAbilitySpec()
        {
            return new TackleAbilitySpec(this);
        }

        private sealed class TackleAbilitySpec : AbilitySpec
        {
            public TackleAbilitySpec(ScriptableAbility asset) : base(asset)
            {

            }

            public override bool keepWaiting => IsComplete();

            protected override void ActivateAbility(Pokemon instigator, Pokemon target)
            {
                Debug.Log($"{instigator.name} used tackle on {target.name}");

                m_IsComplete = true;
            }
        }
    }
}
