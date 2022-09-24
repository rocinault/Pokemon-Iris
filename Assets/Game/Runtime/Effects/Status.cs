using UnityEngine;

using Umbreon;

namespace Iris
{
    [CreateAssetMenu]
    internal sealed class Status : ScriptableEffect
    {
        public override EffectSpec CreateEffectSpec(ScriptableAbility asset)
        {
            return new StatusEffectSpec(asset);
        }

        private sealed class StatusEffectSpec : EffectSpec
        {
            public StatusEffectSpec(ScriptableAbility asset) : base(asset)
            {

            }

            protected override bool CanApplyEffectSpec(Pokemon instigator, Pokemon target, ref SpecResult result)
            {
                base.CanApplyEffectSpec(instigator, target, ref result);

                if (result.success)
                {
                    return CheckAttributeStageLevel(instigator, target, ref result);
                }

                return result.success;
            }

            private bool CheckAttributeStageLevel(Pokemon instigator, Pokemon target, ref SpecResult result)
            {
                result.success = true;

                foreach (var modifier in asset.container.modifiers)
                {
                    switch (modifier.target)
                    {
                        case StatisticModifierType.self:
                            CheckStatisticIsWithinMinAndMaxRange(instigator, modifier.stat, ref result);
                            break;
                        case StatisticModifierType.target:
                            CheckStatisticIsWithinMinAndMaxRange(target, modifier.stat, ref result);
                            break;
                    }

                    if (!result.success)
                    {
                        break;
                    }
                }

                return result.success;
            }

            private bool CheckStatisticIsWithinMinAndMaxRange(Pokemon combatant, StatisticType type, ref SpecResult result)
            {
                if (combatant.TryGetStatistic(type, out Statistic stat))
                {
                    if (stat.stage >= 6)
                    {
                        result.message += string.Concat($"{combatant.name.ToUpper()} {type} won't go higher!\n");
                        result.success = false;
                    }
                    else if (stat.stage <= -6)
                    {
                        result.message += string.Concat($"{combatant.name.ToUpper()} {type} won't go lower!\n");
                        result.success = false;
                    }
                }

                return result.success;
            }

            public override void PostApplyEffectSpec(Pokemon instigator, Pokemon target, ref SpecResult result)
            {
                foreach (var modifier in asset.container.modifiers)
                {
                    switch (modifier.target)
                    {
                        case StatisticModifierType.self:
                            ApplyStageModifiersToCombatant(instigator, modifier.stat, modifier.multiplier, ref result);
                            break;
                        case StatisticModifierType.target:
                            ApplyStageModifiersToCombatant(target, modifier.stat, modifier.multiplier, ref result);
                            break;
                    }
                }
            }

            private void ApplyStageModifiersToCombatant(Pokemon combatant, StatisticType type, int multiplier, ref SpecResult result)
            {
                if (combatant.TryGetStatistic(type, out Statistic stat))
                {
                    int oldStageValue = stat.stage;

                    stat.stage += multiplier;

                    int currentStageValue = stat.stage;

                    if (oldStageValue > currentStageValue)
                    {
                        result.message += string.Concat($"{combatant.name.ToUpper()}'s {type} fell!\n");
                    }
                    else if (oldStageValue < currentStageValue)
                    {
                        result.message += string.Concat($"{combatant.name.ToUpper()}'s {type} rose!\n");
                    }
                }
            }

        }
    }
}
