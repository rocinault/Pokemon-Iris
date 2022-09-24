using System;

using Umbreon;

namespace Iris
{
    internal sealed class DamagedEventArgs : EventArgs
    {
        internal readonly Combatant target;

        internal DamagedEventArgs(Combatant target)
        {
            this.target = target;
        }

        internal static DamagedEventArgs CreateEventArgs(Combatant target)
        {
            return new DamagedEventArgs(target);
        }
    }
}
