using System;

using UnityEngine;
using UnityEngine.UI;

using Voltorb;

namespace Iris
{
    [RequireComponent(typeof(Image))]
    internal sealed class EnemyPokemonPanel : Panel<PokemonGraphicProperties>
    {
        [Serializable]
        private sealed class EnemyPokemonPanelSettings
        {
            [SerializeField]
            internal Image image;
        }

        [SerializeField]
        private EnemyPokemonPanelSettings m_Settings = new EnemyPokemonPanelSettings();

        public override void SetProperties(PokemonGraphicProperties props)
        {
            m_Settings.image.sprite = props.pokemon.asset.spriteFront;
        }
    }
}
