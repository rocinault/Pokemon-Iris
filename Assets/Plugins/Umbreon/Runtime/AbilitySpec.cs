using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Umbreon
{
    public abstract class AbilitySpec
    {
        public readonly ScriptableAbility asset;

        public AbilitySpec(ScriptableAbility asset)
        {
            this.asset = asset;
        }

        public virtual void PreAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
        {
            result = SpecResult.CreateSpecResult(string.Empty, true);

            if (CanActivateAbility(ref result))
            {
                result.message = string.Concat($"{instigator.pokemon.name.ToUpper()} used {asset.abilityName}!\n");
            }
        }

        public abstract IEnumerator ActivateAbility(Combatant instigator, Combatant target);

        public virtual void PostAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
        {
            result = SpecResult.CreateSpecResult(string.Empty, true);
        }

        protected virtual bool CanActivateAbility(ref SpecResult result)
        {
            return true;
        }
    }

    public sealed class SpecResult
    {
        public string message;
        public bool success;

        public SpecResult(string message, bool success)
        {
            this.message = message;
            this.success = success;
        }

        public static SpecResult CreateSpecResult(string message, bool success)
        {
            return new SpecResult(message, success);
        }
    }
}

/*

return CheckAbilityCost(ref result) && CheckAbilityCooldown(ref result);

protected virtual bool CheckAbilityCost(ref SpecResult result)
        {
            // Not enough Pp.
            if (cost.value <= 0)
            {
                result.message += string.Concat("Not enough Pp!");
                result.success = false;
            }

            return result.success;
        }

        protected virtual bool CheckAbilityCooldown(ref SpecResult result)
        {
            // On cooldown from use.
            if (cooldown.value > 0)
            {
                result.success = false;
            }

            return result.success;
        } 
 */ 