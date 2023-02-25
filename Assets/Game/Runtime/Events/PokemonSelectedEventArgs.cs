using Eevee;

namespace Iris
{
    internal readonly struct PokemonSelectedEventArgs
    {
        internal readonly PokemonSpec pokemon;

        private PokemonSelectedEventArgs(PokemonSpec pokemon)
        {
            this.pokemon = pokemon;
        }

        internal static PokemonSelectedEventArgs Create(PokemonSpec pokemon)
        {
            return new PokemonSelectedEventArgs(pokemon);
        }
    }
}
