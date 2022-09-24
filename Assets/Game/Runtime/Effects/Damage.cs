using UnityEngine;

using Golem;
using Umbreon;

namespace Iris
{
    [CreateAssetMenu]
    internal sealed class Damage : ScriptableEffect
    {
        public override EffectSpec CreateEffectSpec(ScriptableAbility asset)
        {
            return new DamageSpec(asset);
        }

        private sealed class DamageSpec : EffectSpec
        {
            public DamageSpec(ScriptableAbility asset) : base(asset)
            {
                
            }

            public override void PostApplyEffectSpec(Pokemon instigator, Pokemon target, ref SpecResult result)
            {
                float power = asset.container.power;
                float attack = instigator.attack.valueModified;
                float defence = target.defence.valueModified;

                int critical = ((1f / 4f * 100f) > (Random.value * 100f)) ? 2 : 1;
                //int critical = ((1f / 16f * 100f) > (Random.value * 100f)) ? 2 : 1;
                float random = Random.Range(85f, 100f) / 100f;

                float damage = Mathf.Floor((((2f * instigator.level / 5f + 2f) * power * attack / defence / 50f) + 2f) * critical * random);

                Debug.Log($"CRIT: {critical}, RNG: {random}, DMG: {damage}");

                target.health.value = Mathf.Max(target.health.value - damage, 0f);

                if (critical > 1)
                {
                    result.message += string.Concat($"A critical hit!\n");
                }

                //EventSystem.instance.Invoke(DamagedEventArgs.CreateEventArgs(target));
            }
        }

    }
}
