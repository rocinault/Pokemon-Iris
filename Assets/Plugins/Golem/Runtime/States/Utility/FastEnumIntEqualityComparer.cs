using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Golem
{
    internal struct FastEnumIntEqualityComparer<T> : IEqualityComparer<T>
    {
        private static class BoxAvoidance
        {
            private static readonly Func<T, int> s_Wrapper;

            static BoxAvoidance()
            {
                var paramter = Expression.Parameter(typeof(T), null);
                var convert = Expression.ConvertChecked(paramter, typeof(int));

                s_Wrapper = Expression.Lambda<Func<T, int>>(convert, paramter).Compile();
            }

            internal static int ToInt(T value)
            {
                return s_Wrapper(value);
            }
        }

        public bool Equals(T x, T y)
        {
            return BoxAvoidance.ToInt(x) == BoxAvoidance.ToInt(y);
        }

        public int GetHashCode(T value)
        {
            return BoxAvoidance.ToInt(value);
        }
    }
}
