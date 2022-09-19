using UnityEngine;

namespace Umbreon
{
    public abstract class EffectSpec
    {
        public readonly ScriptableAbility asset;

        public EffectSpec(ScriptableAbility asset)
        {
            this.asset = asset;
        }

        public virtual void PreApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
        {
            CanApplyEffectSpec(instigator, target, ref result);
        }

        public abstract void ApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result);

        public virtual void PostApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
        {

        }

        protected virtual bool CanApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
        {
            return CheckAbilityAccuracy(instigator.pokemon, target.pokemon, ref result);
        }

        private bool CheckAbilityAccuracy(Pokemon instigator, Pokemon target, ref SpecResult result)
        {
            result.success = (Random.value * 100f) < asset.container.accuracy;

            if (!result.success)
            {
                result.message += string.Concat($"{instigator.name.ToUpper()}'s attack missed!\n");
            }

            return result.success;
        }
    }
}
