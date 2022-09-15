using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Slowbro;
using Voltorb;

namespace Iris
{
    [RequireComponent(typeof(Image))]
    internal class PlayerTrainerPanel : Panel
    {
        [Serializable]
        private sealed class PlayerTrainerPanelSettings
        {
            [SerializeField]
            internal Image image;

            [SerializeField]
            internal AnimationClip clip;
        }

        [SerializeField]
        private PlayerTrainerPanelSettings m_Settings = new PlayerTrainerPanelSettings();

        public override IEnumerator Hide()
        {
            // 1.25f
            yield return rectTransform.Translate(rectTransform.anchoredPosition, Vector3.right * -128f, 0.1f, Space.Self, EasingType.linear);

            gameObject.SetActive(false);
        }
    }
}