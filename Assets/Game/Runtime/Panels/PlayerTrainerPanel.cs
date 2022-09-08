using System;

using UnityEngine;
using UnityEngine.UI;

using Golem;
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

        public override void Hide()
        {
            AnimateExitTransition();
        }

        protected override void AnimateExitTransition()
        {
            rectTransform.Move(rectTransform.anchoredPosition, Vector3.right * -128f, 1.5f, EasingType.linear, Space.Self);

            var image = m_Settings.image;
            var clip = m_Settings.clip;

            image.Animate(clip);
        }
    }
}
