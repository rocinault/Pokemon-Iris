using System;

using UnityEngine;
using UnityEngine.Events;

using Eevee;
using Voltorb;

namespace Iris
{
    internal sealed class PokemonButton : SelectableButton<PokemonSpec>
    {
        [SerializeField]
        private PokemonButtonClickedEvent m_OnClick;

        private PokemonSpec m_Pokemon;

        public override void BindProperties(PokemonSpec pokemon)
        {
            m_Pokemon = pokemon;
        }

        public override void BindPropertiesToNull()
        {
            
        }

        public override void Select()
        {
            m_OnClick?.Invoke(m_Pokemon);
        }

        [Serializable]
        private sealed class PokemonButtonClickedEvent : UnityEvent<PokemonSpec>
        {

        }
    }
}
