using UnityEngine;

namespace Eevee
{
    [CreateAssetMenu(fileName = "new-pokemon", menuName = "ScriptableObjects/Eevee/Pokemon", order = 150)]
    public sealed class ScriptablePokemon : ScriptableObject
    {
        [SerializeField]
        private string m_PokemonName;

        [SerializeField]
        private Sprite m_SpriteFront;

        [SerializeField]
        private Sprite m_SpriteBack;

        [SerializeField]
        private int m_Health;

        [SerializeField]
        private int m_Attack;

        [SerializeField]
        private int m_Defence;

        [SerializeField]
        private int m_Speed;

        [SerializeField]
        private int m_level;

        [SerializeField]
        private LevelRequiredAbility[] m_Abilities;

        public string pokemonName
        {
            get => m_PokemonName;
        }

        public Sprite spriteFront
        {
            get => m_SpriteFront;
        }

        public Sprite spriteBack
        {
            get => m_SpriteBack;
        }

        internal int health
        {
            get => m_Health;
        }

        internal int attack
        {
            get => m_Attack;
        }

        internal int defence
        {
            get => m_Defence;
        }

        internal int speed
        {
            get => m_Speed;
        }

        internal int level
        {
            get => m_level;
        }

        internal LevelRequiredAbility[] abilities
        {
            get => m_Abilities;
        }
    }
}
