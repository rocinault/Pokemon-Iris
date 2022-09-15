using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Slowbro;
using Voltorb;

namespace Iris
{
    internal sealed class EnemyStatsPanel : Panel<PokemonGraphicProperties>
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

        public override IEnumerator Show()
        {
            gameObject.SetActive(true);

            // 0.425f
            yield return rectTransform.Translate(new Vector3(-128f, 0f), Vector3.zero, 0.1f, Space.World, EasingType.linear);
        }

        public override void SetProperties(PokemonGraphicProperties props)
        {
            var pokemon = props.pokemon;

            m_Settings.name.text = pokemon.name.ToUpper();

            float level = pokemon.level;
            m_Settings.level.text = FormatLevelText(level);

            var healthBar = m_Settings.healthBar;

            healthBar.minValue = 0f;
            healthBar.maxValue = pokemon.health.maxValue;
            healthBar.value = pokemon.health.currentValue;
        }

        private static string FormatLevelText(float level)
        {
            return string.Concat($"Lv{level}");
        }
    }
}