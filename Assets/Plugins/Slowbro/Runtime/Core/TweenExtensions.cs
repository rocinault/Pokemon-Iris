using UnityEngine;

namespace Slowbro
{
    public static class TweenExtensions
    {
        public static AnchoredPosition Translate(this RectTransform transform, Vector3 end, float duration, Space relativeTo, EasingType easing)
        {
            return (AnchoredPosition)new AnchoredPosition((o) => o.anchoredPosition, (o, v) => o.anchoredPosition = v)
                .Initialise(transform).SetStart(transform.position).SetEnd(end).SetDuration(duration).SetSpace(relativeTo).SetInterpolation(new Vector3Interpolator(easing)).Run();
        }

        public static AnchoredPosition Translate(this RectTransform transform, Vector3 start, Vector3 end, float duration, Space relativeTo, EasingType easing)
        {
            return (AnchoredPosition)new AnchoredPosition((o) => o.anchoredPosition, (o, v) => o.anchoredPosition = v)
                .Initialise(transform).SetStart(start).SetEnd(end).SetDuration(duration).SetSpace(relativeTo).SetInterpolation(new Vector3Interpolator(easing)).Run();
        }

        public static AnchoredPosition Scale(this RectTransform transform, Vector3 start, Vector3 end, float duration, Space relativeTo, EasingType easing)
        {
            return (AnchoredPosition)new AnchoredPosition((o) => o.localScale, (o, v) => o.localScale = v)
                .Initialise(transform).SetStart(start).SetEnd(end).SetDuration(duration).SetSpace(relativeTo).SetInterpolation(new Vector3Interpolator(easing)).Run();
        }
    }
}