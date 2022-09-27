using UnityEngine;

namespace Umbreon
{
    public abstract class Summoner : MonoBehaviour
    {
        protected virtual void Awake()
        {
            CreateStartupPokemonParty();
        }

        protected abstract void CreateStartupPokemonParty();

        public abstract Pokemon GetActivePokemonPartyMember();

        public abstract Pokemon GetFirstPokemonThatIsNotFainted();
    }
}
