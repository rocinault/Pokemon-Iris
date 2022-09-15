using System;
using System.Collections;

using UnityEngine;

using Umbreon;

namespace Iris
{
    internal abstract class Move : IComparable
    {
        public abstract IEnumerator Run();

        public abstract int CompareTo(object obj);
    }

    internal class Fight : Move
    {
        private readonly BattleGraphicsInterface m_GraphicsInterface;

        private readonly Pokemon m_Instigator;
        private readonly Pokemon m_Target;

        private readonly AbilitySpec m_AbilitySpec;

        internal Fight(BattleGraphicsInterface graphicsInterface, Pokemon instigator, Pokemon target, AbilitySpec abilitySpec)
        {
            m_GraphicsInterface = graphicsInterface;

            m_Instigator = instigator;
            m_Target = target;

            m_AbilitySpec = abilitySpec;
        }

        public override IEnumerator Run()
        {

            yield return m_AbilitySpec.TryActivateAbility(m_Instigator, m_Target);
        }

        public override int CompareTo(object obj)
        {
            return -1;
        }
    }
}
