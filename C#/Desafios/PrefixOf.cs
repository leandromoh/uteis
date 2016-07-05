using System.Collections.Generic;

namespace System.Linq
{
    public static class Enumerable
    {
        public static bool PrefixOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return PrefixOf(first, second, null);
        }

        public static bool PrefixOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null)
                throw new ArgumentNullException("first");

            if (second == null)
                throw new ArgumentNullException("second");

            if (comparer == null)
                comparer = EqualityComparer<TSource>.Default;

            ICollection<TSource> firstCol = first as ICollection<TSource>;
            if (firstCol != null)
            {
                ICollection<TSource> secondCol = second as ICollection<TSource>;
                if (secondCol != null && firstCol.Count > secondCol.Count)
                {
                    return false;
                }
            }

            using (IEnumerator<TSource> e1 = first.GetEnumerator())
            using (IEnumerator<TSource> e2 = second.GetEnumerator())
            {
                while (e1.MoveNext())
                {
                    if (!(e2.MoveNext() && comparer.Equals(e1.Current, e2.Current)))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}