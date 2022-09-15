using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Slowbro;
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

        public override IEnumerator Show()
        {
            gameObject.SetActive(true);

            // 0.215f
            yield return rectTransform.Scale(Vector3.zero, Vector3.one, 0.1f, Space.Self, EasingType.linear);
        }

        public override void SetProperties(PokemonGraphicProperties props)
        {
            m_Settings.image.sprite = props.pokemon.asset.spriteBack;
        }
    }
}

/*
         public override void Show()
        {
            base.Show();

            AnimateEnterTransition();
        }

        protected override void AnimateEnterTransition()
        {
            rectTransform.Scale(Vector3.zero, Vector3.one, 0.4f, Space.Self, EasingType.EaseOutSine);

            //rectTransform.LocalScale(Vector3.zero, Vector3.one, 0.4f, EasingType.EaseOutSine, Space.Self);
        } 
 */ 