using UnityEngine;
using UnityEngine.UI;

namespace Golem
{
    public static class TweenExtensions
    {
        public static void Move(this Transform transform, Vector3 target, float duration, EasingType easingType, Space relativeTo)
        {
            CoroutineUtility.StartCoroutine(new Position(transform).SetTarget(target).SetDuration(duration).SetEasing(easingType).SetSpace(relativeTo).Run());
        }

        public static void Move(this RectTransform rectTransform, Vector3 start, Vector3 target, float duration, EasingType easingType, Space relativeTo)
        {
            CoroutineUtility.StartCoroutine(new AnchoredPosition(rectTransform).SetStart(start).SetTarget(target).SetDuration(duration).SetEasing(easingType).SetSpace(relativeTo).Run());
        }

        public static void LocalScale(this Transform transform, Vector3 start, Vector3 target, float duration, EasingType easingType, Space relativeTo)
        {
            CoroutineUtility.StartCoroutine(new Scale(transform).SetStart(start).SetTarget(target).SetDuration(duration).SetEasing(easingType).SetSpace(relativeTo).Run());
        }

        public static void LocalScale(this RectTransform rectTransform, Vector3 start, Vector3 target, float duration, EasingType easingType, Space relativeTo)
        {
            CoroutineUtility.StartCoroutine(new AnchoredScale(rectTransform).SetStart(start).SetTarget(target).SetDuration(duration).SetEasing(easingType).SetSpace(relativeTo).Run());
        }

        public static void Animate(this Image image, AnimationClip clip)
        {
            CoroutineUtility.StartCoroutine(new Animation(image, clip).Run());
        }
    }

}
