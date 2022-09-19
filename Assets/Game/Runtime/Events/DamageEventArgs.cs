using System;

using UnityEngine;

using Umbreon;

namespace Iris
{
    internal sealed class DamageEventArgs : EventArgs
    {
        internal readonly Combatant combatant;

        internal DamageEventArgs(Combatant combatant)
        {
            this.combatant = combatant;
        }

        internal static DamageEventArgs CreateEventArgs(Combatant combatant)
        {
            return new DamageEventArgs(combatant);
        }
    }
}
