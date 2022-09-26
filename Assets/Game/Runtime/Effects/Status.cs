using System;
using System.Collections;

using UnityEngine;

using Slowbro;
using Umbreon;

namespace Iris
{
    [CreateAssetMenu]
    internal sealed class Status : ScriptableEffect
    {
        [SerializeField]
        private Texture2D m_Attack;

        [SerializeField]
        private Texture2D m_Defence;

        [SerializeField]
        private Texture2D m_Speed;

        [SerializeField]
        private Texture2D m_Accuracy;

        [SerializeField]
        private Texture2D m_Evasion;

        public override EffectSpec CreateEffectSpec(ScriptableAbility asset)
        {
            return new StatusEffectSpec(asset, this);
        }

        private Texture2D GetTextureForStatistic(StatisticType statType)
        {
            switch (statType)
            {
                case StatisticType.Attack:
                    return m_Attack;
                case StatisticType.Defence:
                    return m_Defence;
                case StatisticType.Speed:
                    return m_Speed;
                default:
                    break;
            }

#if UNITY_EDITOR
            throw new Exception($"Texture for stat type {statType} was not found");
#endif
        }

        private sealed class StatusEffectSpec : EffectSpec
        {
            private readonly Status m_Status;

            public StatusEffectSpec(ScriptableAbility asset, Status status) : base(asset)
            {
                m_Status = status;   
            }

            protected override bool CanApplyEffectSpec(Combatant instigator, Combatant target, ref SpecResult result)
            {
                base.CanApplyEffectSpec(instigator, target, ref result);
                
                if (result.success)
                {
                    CheckStatisticStageLevel(instigator.pokemon, target.pokemon, ref result);
                }

                return result.success;
            }

            private bool CheckStatisticStageLevel(Pokemon instigator, Pokemon target, ref SpecResult result)
            {
                foreach (var modifier in asset.container.modifiers)
                {
                    switch (modifier.target)
                    {
                        case EffectModifierType.Self:
                            CheckStatisticIsWithinMinAndMaxRange(instigator, modifier.stat, ref result);
                            break;
                        case EffectModifierType.Target:
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

            private bool CheckStatisticIsWithinMinAndMaxRange(Pokemon combatant, StatisticType statType, ref SpecResult result)
            {
                if (combatant.TryGetStatistic(statType, out Statistic stat))
                {
                    if (stat.stage >= 6)
                    {
                        result.message += string.Concat($"{combatant.name.ToUpper()} {statType} won't go higher!\n");
                        result.success = false;
                    }
                    else if (stat.stage <= -6)
                    {
                        result.message += string.Concat($"{combatant.name.ToUpper()} {statType} won't go lower!\n");
                        result.success = false;
                    }
                }

                return result.success;
            }

            public override IEnumerator ApplyEffectSpec(Combatant instigator, Combatant target, SpecResult result)
            {
                result.message = string.Empty;

                // consider using a list to sort them by priority; target debuff, target buff, self debuff, self buff.

                foreach (var modifier in GetAllTargetDebuffModifiers())
                {
                    ApplyStageModifiersToCombatant(target, modifier.stat, modifier.multiplier, ref result);

                    yield return target.image.material.Overlay(m_Status.GetTextureForStatistic(modifier.stat), 0.75f, -1f, 1f, EasingType.PingPong);
                }

                yield return TypeStatusCharByCharWithOneSecondDelay(target, result);

                result.message = string.Empty;

                foreach (var modifier in GetAllTargetBuffModifiers())
                {
                    ApplyStageModifiersToCombatant(target, modifier.stat, modifier.multiplier, ref result);

                    yield return target.image.material.Overlay(m_Status.GetTextureForStatistic(modifier.stat), 0.75f, 1f, 1f, EasingType.PingPong);
                }

                yield return TypeStatusCharByCharWithOneSecondDelay(target, result);

                result.message = string.Empty;

                foreach (var modifier in GetAllSelfTargetDebuffModifiers())
                {
                    ApplyStageModifiersToCombatant(instigator, modifier.stat, modifier.multiplier, ref result);

                    yield return instigator.image.material.Overlay(m_Status.GetTextureForStatistic(modifier.stat), 0.75f, -1f, 1f, EasingType.PingPong);
                }

                yield return TypeStatusCharByCharWithOneSecondDelay(instigator, result);

                result.message = string.Empty;

                foreach (var modifier in GetAllSelfTargetBuffModifiers())
                {
                    ApplyStageModifiersToCombatant(instigator, modifier.stat, modifier.multiplier, ref result);

                    yield return instigator.image.material.Overlay(m_Status.GetTextureForStatistic(modifier.stat), 0.75f, 1f, 1f, EasingType.PingPong);
                }

                yield return TypeStatusCharByCharWithOneSecondDelay(instigator, result);

                result.message = string.Empty;
            }

            private void ApplyStageModifiersToCombatant(Combatant combatant, StatisticType statType, int multiplier, ref SpecResult result)
            {
                var pokemon = combatant.pokemon;

                if (pokemon.TryGetStatistic(statType, out Statistic stat))
                {
                    stat.stage += multiplier;

                    if (multiplier < 0)
                    {
                        result.message += string.Concat($"{pokemon.name.ToUpper()}'s {statType} fell!\n");
                    }
                    else if (multiplier > 0)
                    {
                        result.message += string.Concat($"{pokemon.name.ToUpper()}'s {statType} rose!\n");
                    }
                }
            }

            private IEnumerator TypeStatusCharByCharWithOneSecondDelay(Combatant combatant, SpecResult result)
            {
                var graphicsInterface = FindObjectOfType<BattleGraphicsInterface>();

                var messages = result.message.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < messages.Length; i++)
                {
                    yield return graphicsInterface.TypeTextCharByChar(messages[i]);
                    yield return m_DelayForOneSecond;
                }
            }

            private EffectModifiers[] GetAllTargetDebuffModifiers()
            {
                return Array.FindAll(asset.container.modifiers, (x) => x.target == EffectModifierType.Target && x.multiplier < 0);
            }

            private EffectModifiers[] GetAllTargetBuffModifiers()
            {
                return Array.FindAll(asset.container.modifiers, (x) => x.target == EffectModifierType.Target && x.multiplier > 0);
            }

            private EffectModifiers[] GetAllSelfTargetDebuffModifiers()
            {
                return Array.FindAll(asset.container.modifiers, (x) => x.target == EffectModifierType.Self && x.multiplier < 0);
            }

            private EffectModifiers[] GetAllSelfTargetBuffModifiers()
            {
                return Array.FindAll(asset.container.modifiers, (x) => x.target == EffectModifierType.Self && x.multiplier > 0);
            }
        }
    }
}


/*

                switch (target.affinity)
                {
                    case Affinity.Friendly:
                        yield return new Parallel(graphicsInterface, graphicsInterface.SetPlayerStatsPanelHealthSlider(),
                            graphicsInterface.ShakePlayerStatsPanel());
                        break;
                    case Affinity.Hostile:
                        yield return new Parallel(graphicsInterface, graphicsInterface.SetEnemyStatsPanelHealthSlider(),
                            graphicsInterface.ShakeEnemyStatsPanel());
                        break;
                }

private IEnumerator OnStatusEffectSpec(SpecResult result)
        {
            var modifiers = m_AbilitySpec.asset.container.modifiers;

            for (int i = 0; i < modifiers.Length; i++)
            {
                switch (modifiers[i].target)
                {
                    case EffectModifierType.Target:
                        {
                            switch (target.affinity)
                            {
                                case Affinity.Friendly:
                                    {
                                        yield return new Parallel(graphicsInterface, graphicsInterface.Get<PlayerPokemonPanel>().FlashPokemonImageOnDamage(),
                                            graphicsInterface.Get<PlayerStatsPanel>().ShakeStatsPanelAndSetHealthBarValue(instigator.pokemon));

                                        string message = result.message.Split('\n', StringSplitOptions.RemoveEmptyEntries)[i];
                                        yield return graphicsInterface.TypeTextCharByChar(message);
                                        yield return m_DelayForOneSecond;
                                        break;
                                    }
                                case Affinity.Hostile:
                                    {
                                        yield return new Parallel(graphicsInterface, graphicsInterface.Get<EnemyPokemonPanel>().FlashPokemonImageOnDamage(),
                                            graphicsInterface.Get<EnemyStatsPanel>().ShakeStatsPanelAndSetHealthBarValue(target.pokemon));

                                        string message = result.message.Split('\n', StringSplitOptions.RemoveEmptyEntries)[i];
                                        yield return graphicsInterface.TypeTextCharByChar(message);
                                        yield return m_DelayForOneSecond;
                                        break;
                                    }
                            }
                            break;
                        }
                    case EffectModifierType.Self:
                        {
                            switch (instigator.affinity)
                            {
                                case Affinity.Friendly:
                                    {
                                        yield return new Parallel(graphicsInterface, graphicsInterface.Get<PlayerPokemonPanel>().FlashPokemonImageOnDamage(),
                                            graphicsInterface.Get<PlayerStatsPanel>().ShakeStatsPanelAndSetHealthBarValue(instigator.pokemon));

                                        string message = result.message.Split('\n', StringSplitOptions.RemoveEmptyEntries)[i];
                                        yield return graphicsInterface.TypeTextCharByChar(message);
                                        yield return m_DelayForOneSecond;
                                        break;
                                    }
                                case Affinity.Hostile:
                                    {
                                        yield return new Parallel(graphicsInterface, graphicsInterface.Get<EnemyPokemonPanel>().FlashPokemonImageOnDamage(),
                                            graphicsInterface.Get<EnemyStatsPanel>().ShakeStatsPanelAndSetHealthBarValue(target.pokemon));

                                        string message = result.message.Split('\n', StringSplitOptions.RemoveEmptyEntries)[i];
                                        yield return graphicsInterface.TypeTextCharByChar(message);
                                        yield return m_DelayForOneSecond;
                                        break;
                                    }
                            }
                            break;
                        }
                }
            }
        }


                foreach (var modifier in asset.container.modifiers)
                {
                    switch (modifier.target)
                    {
                        case EffectModifierType.Self:
                            ApplyStageModifiersToCombatant(instigator, modifier.stat, modifier.multiplier, ref result);
                            break;
                        case EffectModifierType.Target:
                            ApplyStageModifiersToCombatant(target, modifier.stat, modifier.multiplier, ref result);
                            break;
                    }
                }




 */ 