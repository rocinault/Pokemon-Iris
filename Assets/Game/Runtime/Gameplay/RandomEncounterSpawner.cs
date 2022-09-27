using System;

using UnityEngine;

using Umbreon;

namespace Iris
{
    internal sealed class RandomEncounterSpawner : Summoner
    {
        [SerializeField]
        private RandomEncounterRuntimeSet m_EncounterSet;

        private Pokemon m_Pokemon;

        protected override void CreateStartupPokemonParty()
        {
            m_Pokemon = RandomEncounterRuntimeSet.CreatePokemonFromSet(m_EncounterSet.GetRandomEncounterFromSet());
        }

        public override Pokemon GetActivePokemonPartyMember()
        {
            return m_Pokemon;
        }

        public override Pokemon GetFirstPokemonThatIsNotFainted()
        {
            if (!m_Pokemon.isFainted)
            {
                return m_Pokemon;
            }

            #region Debug
#if UNITY_EDITOR
            throw new ArgumentOutOfRangeException("No non-fainted pokemon found!");
#endif
            #endregion
        }
    }
}
