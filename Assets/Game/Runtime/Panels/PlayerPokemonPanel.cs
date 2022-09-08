using System;

using UnityEngine;
using UnityEngine.UI;

using Golem;
using Voltorb;

namespace Iris
{
    [RequireComponent(typeof(Image))]
    internal sealed class PlayerPokemonPanel : Panel<PokemonGraphicProperties>
    {
        [Serializable]
        private sealed class PlayerPokemonPanelSettings
        {
            [SerializeField]
            internal Image image;
        }

        [SerializeField]
        private PlayerPokemonPanelSettings m_Settings = new PlayerPokemonPanelSettings();

        public override void Show()
        {
            base.Show();

            AnimateEnterTransition();
        }

        protected override void AnimateEnterTransition()
        {
            rectTransform.LocalScale(Vector3.zero, Vector3.one, 0.4f, EasingType.EaseOutSine, Space.Self);
        }

        public override void SetProperties(PokemonGraphicProperties props)
        {
            m_Settings.image.sprite = props.pokemon.asset.spriteBack;
        }
    }
}
