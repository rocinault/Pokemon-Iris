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

        public override void SetProperties(PokemonGraphicProperties props)
        {
            var pokemon = props.pokemon;

            m_Settings.name.text = pokemon.name.ToUpper();

            float level = pokemon.level;
            m_Settings.level.text = string.Concat($"Lv{level}");

            float current = pokemon.health.value;
            float max = pokemon.health.maxValue;
            m_Settings.health.text = string.Concat($"{current}/{max}");

            var healthBar = m_Settings.healthBar;

            healthBar.minValue = 0f;
            healthBar.maxValue = pokemon.health.maxValue;
            healthBar.value = pokemon.health.value;

            var experienceBar = m_Settings.experienceBar;

            experienceBar.minValue = Mathf.Pow(pokemon.level, 3f);
            experienceBar.maxValue = Mathf.Pow(pokemon.level + 1, 3f);
            experienceBar.value = pokemon.experience;
        }

        public override IEnumerator Show()
        {
            gameObject.SetActive(true);

            yield return rectTransform.Translate(new Vector3(128f, 0f), Vector3.zero, 0.425f, Space.World, EasingType.linear);
        }

        protected override void AddListeners()
        {
            EventSystem.instance.AddListener<DamagedEventArgs>(OnDamaged);
        }

        private void OnDamaged(DamagedEventArgs args)
        {
            var target = args.target;

            if (target.affinity == Affinity.friendly)
            {
                StartCoroutine(ShakeStatsPanelAndSetHealthBarValue(target.pokemon));
            }
        }

        internal IEnumerator SetExperienceBarValue(Pokemon pokemon)
        {
            m_Settings.experienceBar.minValue = Mathf.Pow(pokemon.level, 3f);
            m_Settings.experienceBar.maxValue = Mathf.Pow(Mathf.Min(pokemon.level + 1, 100f), 3f);

            float difference = pokemon.experience - m_Settings.experienceBar.value;
            float duration = 0.5f + (difference / 64f);

            yield return m_Settings.experienceBar.Interpolate(pokemon.experience, duration, EasingType.EaseOutSine);
        }

        private IEnumerator ShakeStatsPanelAndSetHealthBarValue(Pokemon pokemon)
        {
            for (int i = 0; i < 10; i++)
            {
                yield return rectTransform.Translate(rectTransform.anchoredPosition, Vector3.down, 0.05f, Space.Self, EasingType.PingPong);
            }

            int current = Mathf.FloorToInt(m_Settings.healthBar.value);
            int target = Mathf.FloorToInt(pokemon.health.value);
            int max = Mathf.FloorToInt(m_Settings.healthBar.maxValue);

            int difference = current - target;
            float duration = 0.5f + (difference / 64f);

            yield return new Parallel(this, TypeHealthTextCharByChar(target, current, max),
                    m_Settings.healthBar.Interpolate(pokemon.health.value, duration, EasingType.EaseOutSine));
        }

        private IEnumerator TypeHealthTextCharByChar(int target, int current, int max)
        {
            while (current != target)
            {
                int value = Mathf.FloorToInt(m_Settings.healthBar.value);

                if (current != value)
                {
                    current = value;
                    m_Settings.health.text = string.Concat($"{current}/{max}");
                }

                yield return null;
            }
        }

        protected override void RemoveListeners()
        {
            EventSystem.instance.RemoveListener<DamagedEventArgs>(OnDamaged);
        }
    }
}