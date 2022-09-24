using UnityEngine;

namespace Umbreon
{
    public abstract class Summoner : MonoBehaviour
    {
        [SerializeField]
        protected ScriptablePokemon m_Asset;

        [SerializeField]
        protected Combatant m_Combatant;

        private void Awake()
        {
            CreateStartupPokemon();
        }

        protected virtual void CreateStartupPokemon()
        {
            var pokemon = new Pokemon(m_Asset);

            m_Combatant.affinity = Affinity.hostile;
            m_Combatant.pokemon = pokemon;
        }
    }
}
