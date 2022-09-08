using System;

using UnityEngine;
using UnityEngine.UI;

using Golem;
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

        public override void Show()
        {
            base.Show();

            AnimateEnterTransition();
        }

        protected override void AnimateEnterTransition()
        {
            rectTransform.Move(new Vector3(-128f, 0f), Vector3.zero, 0.5f, EasingType.EaseOutSine, Space.World);
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
