using System;

using UnityEngine;
using UnityEngine.UI;

using Golem;
using Voltorb;

namespace Iris
{
    internal sealed class PlayerStatsPanel : Panel<PokemonGraphicProperties>
    {
        [Serializable]
        private sealed class StatsPanelSettings
        {
            [SerializeField]
            internal Text name;

            [SerializeField]
            internal Text level;

            [SerializeField]
            internal Text health;

            [SerializeField]
            internal Slider healthBar;

            [SerializeField]
            internal Slider experienceBar;
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
            rectTransform.Move(new Vector3(128f, 0f), Vector3.zero, 0.5f, EasingType.EaseOutSine, Space.World);
        }

        public override void SetProperties(PokemonGraphicProperties props)
        {
            var pokemon = props.pokemon;

            m_Settings.name.text = pokemon.name.ToUpper();

            float level = pokemon.level;
            m_Settings.level.text = FormatLevelText(level);

            float current = pokemon.health.currentValue;
            float max = pokemon.health.maxValue;
            m_Settings.health.text = FormatHealthText(current, max);

            var healthBar = m_Settings.healthBar;

            healthBar.minValue = 0f;
            healthBar.maxValue = pokemon.health.maxValue;
            healthBar.value = pokemon.health.currentValue;

            var experienceBar = m_Settings.experienceBar;

            experienceBar.minValue = 0f;
            experienceBar.maxValue = 1f;
            experienceBar.value = 0.5f;
        }

        private static string FormatLevelText(float level)
        {
            return string.Concat($"Lv{level}");
        }

        private static string FormatHealthText(float current, float max)
        {
            return string.Concat($"{current}/{max}");
        }
    }
}
