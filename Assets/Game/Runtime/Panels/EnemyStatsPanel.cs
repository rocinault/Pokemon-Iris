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
    internal sealed class EnemyStatsPanel : Panel<CombatantGraphicProperties>
    {
        [Serializable]
        private sealed class StatsPanelSettings
        {
            [SerializeField]
            internal Text name;

            [SerializeField]
            internal Text level;

            [SerializeField]
            internal Slider healthBar;
        }

        [SerializeField]
        private StatsPanelSettings m_Settings = new StatsPanelSettings();

        private Combatant m_Combatant;

        public override IEnumerator Show()
        {
            gameObject.SetActive(true);

            // 0.425f
            yield return rectTransform.Translate(new Vector3(-128f, 0f), Vector3.zero, 0.425f, Space.World, EasingType.linear);
        }

        public override void SetProperties(CombatantGraphicProperties props)
        {
            m_Combatant = props.combatant;
            var pokemon = m_Combatant.pokemon;

            m_Settings.name.text = pokemon.name.ToUpper();

            float level = pokemon.level;
            m_Settings.level.text = FormatLevelText(level);

            var healthBar = m_Settings.healthBar;

            healthBar.minValue = 0f;
            healthBar.maxValue = pokemon.health.maxValue;
            healthBar.value = pokemon.health.value;
        }

        private static string FormatLevelText(float level)
        {
            return string.Concat($"Lv{level}");
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
                StartCoroutine(ShakeStatsPanelAndSetHealthBarValue());
            }
        }

        private IEnumerator ShakeStatsPanelAndSetHealthBarValue()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return rectTransform.Translate(rectTransform.anchoredPosition, Vector3.down, 0.05f, Space.Self, EasingType.PingPong);
            }

            int current = Mathf.FloorToInt(m_Settings.healthBar.value);
            int target = Mathf.FloorToInt(m_Combatant.pokemon.health.value);
            int difference = current - target;

            float duration = 1f + (difference / 32f);

            yield return m_Settings.healthBar.Interpolate(m_Combatant.pokemon.health.value, duration, EasingType.EaseOutSine);
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