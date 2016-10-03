using System.Collections.Generic;
using System;
using System.Linq;

namespace EffectiveLINQ
{
    static partial class LinqExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> source, T item)
        {
            return source.IndexOf(item, 0);
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T item, int startIndex)
        {
            return source.IndexOf(item, startIndex, null);
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T item, int startIndex, int count)
        {
            return source.IndexOf(item, startIndex, count, null);
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T item, IEqualityComparer<T> comparer)
        {
            return source.IndexOf(item, 0, null, comparer);
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T item, int startIndex, int? count, IEqualityComparer<T> comparer = null)
        {
            comparer = comparer ?? EqualityComparer<T>.Default;

            int i = 0;
            int endIndex = count.HasValue ? startIndex + count.Value : int.MaxValue;

            foreach (var current in source)
            {
                if (i < startIndex)
                {
                    i++;
                    continue;
                }

                if (i == endIndex)
                    break;

                if (comparer.Equals(current, item))
                {
                    return i;
                }

                i++;
            }

            return -1;
        }

    }
}
