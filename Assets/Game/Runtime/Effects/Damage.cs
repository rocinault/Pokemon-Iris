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

            public override void ApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
            {
                float power = asset.container.power;
                float attack = instigator.pokemon.attack.value;
                float defence = target.pokemon.defence.value;

                int critical = ((1f / 4f * 100f) > (Random.value * 100f)) ? 2 : 1;
                //int critical = ((1f / 16f * 100f) > (Random.value * 100f)) ? 2 : 1;
                float random = Random.Range(85f, 100f) / 100;

                float damage = Mathf.Floor((((2f * instigator.pokemon.level / 5f + 2f) * power * attack / defence / 50f) + 2f) * critical * random);

                Debug.Log($"CRIT: {critical}, RNG: {random}, DMG: {damage}");

                target.pokemon.health.value = Mathf.Max(target.pokemon.health.value - damage, 0f);

                if (critical > 1)
                {
                    result.message += string.Concat($"A critical hit!\n");
                }

                var args = DamageEventArgs.CreateEventArgs(target);
                EventSystem.instance.Invoke(args);
            }

        }

    }
}
