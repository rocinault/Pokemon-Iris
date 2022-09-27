using System;

using UnityEngine;

using Umbreon;

namespace Iris
{
    internal class Trainer : Summoner
    {
        [SerializeField]
        private TrainerRuntimeSet m_TrainerSet;

        [SerializeField]
        private PokemonRuntimeSet m_PokemonRuntimeSet;

        protected override void CreateStartupPokemonParty()
        {
            int count = m_TrainerSet.Count();

            for (int i = 0; i < count; i++)
            {
                m_PokemonRuntimeSet.Add(TrainerRuntimeSet.CreatePokemonFromSet(m_TrainerSet[i]));
            }
        }

        public override Pokemon GetActivePokemonPartyMember()
        {
            throw new NotImplementedException();
        }

        public override Pokemon GetFirstPokemonThatIsNotFainted()
        {
            for (int i = 0; i < m_PokemonRuntimeSet.Count(); i++)
            {
                var pokemon = m_PokemonRuntimeSet[i];

                if (pokemon != null && !pokemon.isFainted)
                {
                    return pokemon;
                }
            }

            #region Debug
#if UNITY_EDITOR
            throw new ArgumentOutOfRangeException("No non-fainted pokemon found!");
#endif
            #endregion
        }
    }
}
