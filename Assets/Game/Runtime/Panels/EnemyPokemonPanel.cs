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
    internal sealed class EnemyPokemonPanel : Panel<CombatantGraphicProperties>
    {
        [Serializable]
        private sealed class EnemyPokemonPanelSettings
        {
            [SerializeField]
            internal Image image;
        }

        [SerializeField]
        private EnemyPokemonPanelSettings m_Settings = new EnemyPokemonPanelSettings();

        private Combatant m_Combatant;

        public override void SetProperties(CombatantGraphicProperties props)
        {
            m_Combatant = props.combatant;

            m_Settings.image.sprite = m_Combatant.pokemon.asset.spriteFront;
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
