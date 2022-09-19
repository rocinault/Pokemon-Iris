using Umbreon;
using Voltorb;

namespace Iris
{
    internal sealed class CombatantGraphicProperties : GraphicProperties
    {
        internal readonly Combatant combatant;

        internal CombatantGraphicProperties(Combatant combatant)
        {
            this.combatant = combatant;
        }

        internal static CombatantGraphicProperties CreateProperties(Combatant combatant)
        {
            return new CombatantGraphicProperties(combatant);
        }
    }
}
