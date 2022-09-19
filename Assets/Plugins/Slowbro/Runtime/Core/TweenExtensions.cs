using UnityEngine;
using UnityEngine.UI;

namespace Slowbro
{
    public static class TweenExtensions
    {
        public static Tween<Material, float> Alpha(this Material material, float end, float duration, EasingType easing)
        {
            return (Tween<Material, float>)new Tween<Material, float>((o) => o.GetFloat("_Alpha"), (o, v) => o.SetFloat("_Alpha", v))
                .Initialise(material).SetStart(material.GetFloat("_Alpha")).SetEnd(end).SetDuration(duration).SetInterpolation(new FloatInterpolator(easing)).Run();
        }

        public static Tween<Slider, float> Interpolate(this Slider slider, float end, float duration, EasingType easing)
        {
            return (Tween<Slider, float>)new Tween<Slider, float>((o) => o.value, (o, v) => o.value = v)
                .Initialise(slider).SetStart(slider.value).SetEnd(end).SetDuration(duration).SetInterpolation(new FloatInterpolator(easing)).Run();
        }

        public static AnchoredPosition Translate(this RectTransform transform, Vector3 end, float duration, Space relativeTo, EasingType easing)
        {
            return (AnchoredPosition)new AnchoredPosition((o) => o.anchoredPosition, (o, v) => o.anchoredPosition = v)
                .Initialise(transform).SetStart(transform.position).SetEnd(end).SetDuration(duration).SetSpace(relativeTo).SetInterpolation(new Vector3InterpolatorClamped(easing)).Run();
        }

        public static AnchoredPosition Translate(this RectTransform transform, Vector3 start, Vector3 end, float duration, Space relativeTo, EasingType easing)
        {
            return (AnchoredPosition)new AnchoredPosition((o) => o.anchoredPosition, (o, v) => o.anchoredPosition = v)
                .Initialise(transform).SetStart(start).SetEnd(end).SetDuration(duration).SetSpace(relativeTo).SetInterpolation(new Vector3InterpolatorClamped(easing)).Run();
        }

        public static AnchoredPosition Scale(this RectTransform transform, Vector3 start, Vector3 end, float duration, Space relativeTo, EasingType easing)
        {
            return (AnchoredPosition)new AnchoredPosition((o) => o.localScale, (o, v) => o.localScale = v)
                .Initialise(transform).SetStart(start).SetEnd(end).SetDuration(duration).SetSpace(relativeTo).SetInterpolation(new Vector3Interpolator(easing)).Run();
        }
    }
}