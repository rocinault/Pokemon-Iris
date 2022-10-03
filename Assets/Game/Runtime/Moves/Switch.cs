using System;
using System.Collections;

using Umbreon;

namespace Iris
{
    internal sealed class Switch : Move
    {
        public Switch(BattleGraphicsInterface graphicsInterface, Combatant instigator, Combatant target)
            : base(graphicsInterface, instigator, target)
        {

        }

        public override IEnumerator Run()
        {
            throw new NotImplementedException();
        }

        public override int CompareTo(Move other)
        {
            return kInstigatorHasPriority;
        }
    }
}