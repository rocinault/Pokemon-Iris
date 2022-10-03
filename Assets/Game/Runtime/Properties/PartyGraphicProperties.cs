using Umbreon;
using Voltorb;

namespace Iris
{
    internal sealed class PartyGraphicProperties : GraphicProperties
    {
        internal readonly Pokemon[] pokemons;

        internal PartyGraphicProperties(Pokemon[] pokemons)
        {
            this.pokemons = pokemons;
        }

        internal static PartyGraphicProperties CreateProperties(Pokemon[] pokemons)
        {
            return new PartyGraphicProperties(pokemons);
        }
    }
}
