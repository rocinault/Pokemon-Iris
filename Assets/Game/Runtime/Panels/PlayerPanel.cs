using UnityEngine;

using Slowbro;
using Voltorb;

namespace Iris
{
    [RequireComponent(typeof(RectTransform))]
    internal sealed class PlayerPanel : Panel
    {
        public override System.Collections.IEnumerator Show()
        {
            gameObject.SetActive(true);

            // 2f
            yield return rectTransform.Translate(new Vector3(256f, 0f), Vector3.zero, 0.1f, Space.World, EasingType.EaseOutSine);
        }

    }
}