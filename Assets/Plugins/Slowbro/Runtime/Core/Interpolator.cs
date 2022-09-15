using UnityEngine;

namespace Slowbro
{
    public interface IInterpolator<T>
    {
        public T Interpolate(T a, T b, float t);
    }

    public struct Vector3Interpolator : IInterpolator<Vector3>
    {
        internal readonly EasingType easing;

        internal Vector3Interpolator(EasingType easing)
        {
            this.easing = easing;
        }

        public Vector3 Interpolate(Vector3 a, Vector3 b, float t)
        {
            return new Vector3(Mathf.LerpUnclamped(a.x, b.x, Easing.Resolve(easing, t)),
                Mathf.LerpUnclamped(a.y, b.y, Easing.Resolve(easing, t)), 1f);
        }
    }
}
