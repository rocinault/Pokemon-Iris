﻿using System;
using System.Collections;

using UnityEngine;

using Slowbro;
using Umbreon;

namespace Iris
{
    internal sealed class Fight : Move
    {
        private readonly AbilitySpec m_AbilitySpec;

        private readonly WaitForSeconds m_DelayForHalfSecond = new WaitForSeconds(kDelayForHalfSecond);
        private readonly WaitForSeconds m_DelayForOneSecond = new WaitForSeconds(kDelayForOneSecond);

        private const float kDelayForHalfSecond = 0.5f;
        private const float kDelayForOneSecond = 1f;

        private const int kInstigatorHasPriority = -1;
        private const int kInstigatorDoesNotHavePriority = 1;

        public Fight(BattleGraphicsInterface graphicsInterface, Combatant instigator, Combatant target, AbilitySpec abilitySpec) : base(graphicsInterface, instigator, target)
        {
            m_AbilitySpec = abilitySpec;
        }

        // Find a better way to check the type of effect that is being run, this shit sucks ass.
        public override IEnumerator Run()
        {
            m_AbilitySpec.PreAbilityActivate(instigator, target, out SpecResult result);

            yield return TypeSpecResultMessagesCharByCharWithHalfSecondDelay(result);

            if (result.success)
            {
                yield return m_AbilitySpec.ActivateAbility(instigator, target);

                // Get a list of the effects, then sort it, run the correct routine based on the type.

                yield return m_DelayForHalfSecond;

                yield return m_AbilitySpec.effectSpec.ApplyEffectSpec(instigator, target, result);

                // Consider moving this to the actual classes then all of this checking bs can be avoided.
                //if (m_AbilitySpec.asset.effect.GetType() == typeof(Damage))
                //{
                //    result.message = string.Empty;

                //    m_AbilitySpec.effectSpec.ApplyEffectSpec(instigator.pokemon, target.pokemon, ref result);

                //    yield return OnDamageEffectSpec(result);
                //}
                //else if (m_AbilitySpec.asset.effect.GetType() == typeof(Status))
                //{
                //    result.message = string.Empty;

                //    m_AbilitySpec.effectSpec.ApplyEffectSpec(instigator.pokemon, target.pokemon, ref result);

                //    yield return OnStatusEffectSpec(result);
                //}
            }

            graphicsInterface.CleanupTextProcessorAndClearText();
        }

        private IEnumerator TypeSpecResultMessagesCharByCharWithHalfSecondDelay(SpecResult result)
        {
            var messages = result.message.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < messages.Length; i++)
            {
                yield return graphicsInterface.TypeTextCharByChar(messages[i]);

                yield return m_DelayForHalfSecond;
            }
        }

        private IEnumerator TypeSpecResultMessagesCharByCharWithOneSecondDelay(SpecResult result)
        {
            var messages = result.message.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < messages.Length; i++)
            {
                yield return graphicsInterface.TypeTextCharByChar(messages[i]);

                yield return m_DelayForOneSecond;
            }
        }

        public override int CompareTo(Move other)
        {
            if (instigator.pokemon.speed.value > other.instigator.pokemon.speed.value)
            {
                return kInstigatorHasPriority;
            }
            else if (instigator.pokemon.speed.value == other.instigator.pokemon.speed.value)
            {
                if (UnityEngine.Random.value > 0.5f)
                {
                    return kInstigatorHasPriority;
                }
                else
                {
                    return kInstigatorDoesNotHavePriority;
                }
            }
            else
            {
                return kInstigatorDoesNotHavePriority;
            }
        }
    }
}