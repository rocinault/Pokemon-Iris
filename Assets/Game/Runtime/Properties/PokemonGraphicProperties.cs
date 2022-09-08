using Umbreon;
using Voltorb;

namespace Iris
{
    internal sealed class PokemonGraphicProperties : GraphicProperties
    {
        internal readonly Pokemon pokemon;

        internal PokemonGraphicProperties(Pokemon pokemon)
        {
            this.pokemon = pokemon;
        }

        internal static PokemonGraphicProperties CreateProperties(Pokemon pokemon)
        {
            return new PokemonGraphicProperties(pokemon);
        }
    }
}
