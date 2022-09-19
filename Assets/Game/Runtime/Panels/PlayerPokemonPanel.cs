using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Golem;
using Slowbro;
using Umbreon;
using Voltorb;

namespace Iris
{
    [RequireComponent(typeof(Image))]
    internal sealed class PlayerPokemonPanel : Panel<CombatantGraphicProperties>
    {
        [Serializable]
        private sealed class PlayerPokemonPanelSettings
        {
            [SerializeField]
            internal Image image;
        }

        [SerializeField]
        private PlayerPokemonPanelSettings m_Settings = new PlayerPokemonPanelSettings();

        private Combatant m_Combatant;

        public override IEnumerator Show()
        {
            gameObject.SetActive(true);

            // 0.215f
            yield return rectTransform.Scale(Vector3.zero, Vector3.one, 0.215f, Space.Self, EasingType.EaseOutSine);
        }

        public override void SetProperties(CombatantGraphicProperties props)
        {
            m_Combatant = props.combatant;

            m_Settings.image.sprite = m_Combatant.pokemon.asset.spriteBack;
        }

        private void OnEnable()
        {
            if (m_Combatant != null)
            {
                EventSystem.instance.AddListener<DamageEventArgs>(OnDamage);
            }
        }

        private void OnDamage(DamageEventArgs args)
        {
            if (args.combatant == m_Combatant)
            {
                StartCoroutine(FlashPokemonImageOnDamage());
            }
        }

        private IEnumerator FlashPokemonImageOnDamage()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return m_Settings.image.material.Alpha(0f, 0.1f, EasingType.PingPong);
            }
        }

        private void OnDisable()
        {
            if (m_Combatant != null)
            {
                EventSystem.instance.RemoveListener<DamageEventArgs>(OnDamage);
            }
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