using System;

using Golem;
using Umbreon;

namespace Iris
{
    internal abstract class Move : YieldCoroutine, IComparable
    {
        public abstract int CompareTo(object obj);
    }

    internal class Fight : Move
    {
        private readonly BattleCoordinator m_Coordinator;

        private readonly Pokemon m_Instigator;
        private readonly Pokemon m_Target;

        private readonly AbilitySpec m_AbilitySpec;

        public override bool keepWaiting => throw new NotImplementedException();

        internal Fight(BattleCoordinator coordinator, Pokemon instigator, Pokemon target, AbilitySpec abilitySpec)
        {
            m_Coordinator = coordinator;

            m_Instigator = instigator;
            m_Target = target;

            m_AbilitySpec = abilitySpec;
        }

        protected override bool Update()
        {
            m_AbilitySpec.TryActivateAbility(m_Instigator, m_Target);

            return true;
        }

        public override int CompareTo(object obj)
        {
            return -1;
        }
    }
}
