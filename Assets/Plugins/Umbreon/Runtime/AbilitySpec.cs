using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Umbreon
{
    public abstract class AbilitySpec
    {
        public readonly ScriptableAbility asset;

        public readonly Attribute cost;
        public readonly Attribute cooldown;

        public AbilitySpec(ScriptableAbility asset)
        {
            this.asset = asset;

            cost = new Attribute(asset.cost);
            cooldown = new Attribute(asset.cooldown);
        }

        public abstract IEnumerator ActivateAbility(Pokemon instigator, Pokemon target);

        public virtual bool CanActivateAbility()
        {
            return CheckAbilityCost() && CheckAbilityCooldown();
        }

        protected virtual bool CheckAbilityCost()
        {
            return cost.currentValue > 0;
        }

        protected virtual bool CheckAbilityCooldown()
        {
            return cooldown.currentValue <= 0;
        }
    }

    public class AbilitySpecResult
    {
        public string message;
        public bool success;
    }
}

/*
         public IEnumerator TryActivateAbility(Pokemon instigator, Pokemon target)
        {
            if (CanActivateAbility())
            {                
                yield return ActivateAbility(instigator, target);
            }
        } 
 */ 