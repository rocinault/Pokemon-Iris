using System;
using System.Collections;

using UnityEngine;

using Umbreon;

namespace Iris
{
    internal sealed class Fight : Move
    {
        private readonly AbilitySpec m_AbilitySpec;

        private readonly WaitForSeconds m_DelayForHalfSecond = new WaitForSeconds(kDelayForHalfSecond);
        private readonly WaitForSeconds m_DelayForOneSecond = new WaitForSeconds(kDelayForOneSecond);
        private readonly WaitForSeconds m_DelayForTwoSeconds = new WaitForSeconds(kDelayForTwoSeconds);

        private const float kDelayForHalfSecond = 0.5f;
        private const float kDelayForOneSecond = 1f;
        private const float kDelayForTwoSeconds = 2f;

        private const int kInstigatorHasPriority = -1;
        private const int kInstigatorDoesNotHavePriority = 1;

        public Fight(BattleGraphicsInterface graphicsInterface, Combatant instigator, Combatant target, AbilitySpec abilitySpec) : base(graphicsInterface, instigator, target)
        {
            m_AbilitySpec = abilitySpec;
        }

        public override IEnumerator Run()
        {
            m_AbilitySpec.PreAbilityActivate(instigator, target, out SpecResult result);

            yield return TypeSpecResultMessagesCharByCharWithHalfSecondDelay(result);

            if (result.success)
            {
                yield return m_AbilitySpec.ActivateAbility(instigator, target);

                yield return m_DelayForHalfSecond;

                m_AbilitySpec.PostAbilityActivate(instigator, target, out result);

                yield return m_DelayForTwoSeconds;

                yield return TypeSpecResultMessagesCharByCharWithOneSecondDelay(result);
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
            if (other.instigator.pokemon.speed.value < instigator.pokemon.speed.value)
            {
                return kInstigatorHasPriority;
            }
            else if (UnityEngine.Random.value > 0.5f)
            {
                return kInstigatorHasPriority;
            }
            else
            {
                return kInstigatorDoesNotHavePriority;
            }
        }
    }
}