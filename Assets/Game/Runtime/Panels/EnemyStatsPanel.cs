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

        public override void SetProperties(PokemonGraphicProperties props)
        {
            var pokemon = props.pokemon;

            m_Settings.name.text = pokemon.name.ToUpper();

            float level = pokemon.level;
            m_Settings.level.text = string.Concat($"Lv{level}");

            var healthBar = m_Settings.healthBar;

            healthBar.minValue = 0f;
            healthBar.maxValue = pokemon.health.maxValue;
            healthBar.value = pokemon.health.value;
        }

        public override IEnumerator Show()
        {
            gameObject.SetActive(true);

            yield return rectTransform.Translate(new Vector3(-128f, 0f), Vector3.zero, 0.425f, Space.World, EasingType.linear);
        }

        protected override void AddListeners()
        {
            EventSystem.instance.AddListener<DamagedEventArgs>(OnDamaged);
        }

        private void OnDamaged(DamagedEventArgs args)
        {
            var target = args.target;

            if (target.affinity == Affinity.hostile)
            {
                StartCoroutine(ShakeStatsPanelAndSetHealthBarValue(target.pokemon));
            }
        }

        private IEnumerator ShakeStatsPanelAndSetHealthBarValue(Pokemon pokemon)
        {
            for (int i = 0; i < 10; i++)
            {
                yield return rectTransform.Translate(rectTransform.anchoredPosition, Vector3.down, 0.05f, Space.Self, EasingType.PingPong);
            }

            int current = Mathf.FloorToInt(m_Settings.healthBar.value);
            int target = Mathf.FloorToInt(pokemon.health.value);
            int difference = current - target;

            float duration = 0.5f + (difference / 32f);

            yield return m_Settings.healthBar.Interpolate(pokemon.health.value, duration, EasingType.EaseOutSine);
        }

        protected override void RemoveListeners()
        {
            EventSystem.instance.RemoveListener<DamagedEventArgs>(OnDamaged);
        }
    }
}