using UnityEngine;

using Golem;
using Voltorb;

namespace Iris
{
    internal sealed class EnemyPanel : Panel
    {
        public override void Show()
        {
            base.Show();

            AnimateEnterTransition();
        }

        protected override void AnimateEnterTransition()
        {
            rectTransform.Move(new Vector3(-256f, 0f), Vector3.zero, 1.25f, EasingType.EaseOutSine, Space.World);
        }
    }
}
