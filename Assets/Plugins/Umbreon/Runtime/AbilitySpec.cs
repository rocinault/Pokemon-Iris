using System;
using System.Collections;

using UnityEngine;

namespace Umbreon
{
    public abstract class AbilitySpec : CustomYieldInstruction
    {
        public readonly ScriptableAbility asset;

        public readonly Attribute cost;
        public readonly Attribute cooldown;

        protected bool m_IsComplete;

        public AbilitySpec(ScriptableAbility asset)
        {
            this.asset = asset;

            cost = new Attribute(asset.cost);
            cooldown = new Attribute(asset.cooldown);

            m_IsComplete = false;
        }

        public void TryActivateAbility(Pokemon instigator, Pokemon target)
        {
            if (CanActivateAbility())
            {
                ActivateAbility(instigator, target);
            }
        }

        protected abstract void ActivateAbility(Pokemon instigator, Pokemon target);

        protected virtual bool IsComplete()
        {
            return m_IsComplete;
        }

        protected virtual bool CanActivateAbility()
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
}