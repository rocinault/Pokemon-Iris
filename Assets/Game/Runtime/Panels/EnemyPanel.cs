using UnityEngine;

using Slowbro;
using Voltorb;

namespace Iris
{
    internal sealed class EnemyPanel : Panel
    {
        public override System.Collections.IEnumerator Show()
        {
            gameObject.SetActive(true);

            yield return rectTransform.Translate(new Vector3(-256f, 0f), Vector3.zero, 0.1f, Space.World, EasingType.EaseOutSine);
        }
    }
}