using System.Collections.Generic;
using System;
using System.Linq;

namespace EffectiveLINQ
{
    static partial class LinqExtensions
    {
        private static T GetByIndex<T>(int index, Dictionary<int, T> dic, IEnumerator<T> e, out bool ended)
        {
            T val;
            ended = false;

            if (!dic.TryGetValue(index, out val))
            {
                if (e.MoveNext())
                {
                    val = dic[index] = e.Current;
                    return val;
                }
                ended = true;
            }

            return val;
        }

        public static int IndexOf<T>(this IEnumerable<T> source, IEnumerable<T> target, int startIndex = 0, IEqualityComparer<T> comparer = null)
        {
            comparer = comparer ?? EqualityComparer<T>.Default;

            T[] y = target.ToArray();
            int? len1 = source.TryGetLenght();
            int len2 = y.Length;
            int? maxIndex = null;
            int j = 0;
            int k = 0;

            if (len1.HasValue)
            {
                if (startIndex >= len1 || len1 < len2)
                    return -1;
                else
                    maxIndex = len1.Value - len2;
            }

            int toSkip = startIndex;

            using (var e1 = source.GetEnumerator())
            {
                while (toSkip-- > 0)
                {
                    if (!e1.MoveNext())
                    {
                        return -1;
                    }
                }

                var dic = new Dictionary<int, T>();
                var ended = false;
                T value;

                for (; maxIndex.HasValue ? startIndex <= maxIndex : true; startIndex++)
                {
                    for (k = startIndex, j = 0; j < len2; j++, k++)
                    {
                        value = GetByIndex(k, dic, e1, out ended);

                        if (ended)
                        {
                            len1 = k;
                            maxIndex = len1.Value - len2;
                            break;
                        }

                        if (!comparer.Equals(value, y[j]))
                        {
                            break;
                        }
                    }

                    if (j == len2)
                    {
                        return startIndex;
                    }
                }

                return -1;
            }
        }
    }
}
