using System;
using System.Collections;

using Umbreon;

namespace Iris
{
    internal abstract class Move : IComparable<Move>
    {
        public readonly BattleGraphicsInterface graphicsInterface;

        public readonly Combatant instigator;
        public readonly Combatant target;

        protected const int kInstigatorHasPriority = -1;
        protected const int kInstigatorDoesNotHavePriority = 1;

        public Move(BattleGraphicsInterface graphicsInterface, Combatant instigator, Combatant target)
        {
            this.graphicsInterface = graphicsInterface;

            this.instigator = instigator;
            this.target = target;
        }

        public abstract IEnumerator Run();

        public abstract int CompareTo(Move other);
    }
}