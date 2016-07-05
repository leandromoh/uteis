using System.Collections.Generic;

namespace System.Linq
{
    public static class Enumerdable
    {
        public static bool SuffixOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return SuffixOf(first, second, null);
        }

        public static bool SuffixOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null)
                throw new ArgumentNullException("first");

            if (second == null)
                throw new ArgumentNullException("second");

            if (comparer == null)
                comparer = EqualityComparer<TSource>.Default;

            int firstCount = -1;
            int secondCount = -1;

            ICollection<TSource> firstCol = first as ICollection<TSource>;
            if (firstCol != null)
            {
                ICollection<TSource> secondCol = second as ICollection<TSource>;
                if (secondCol != null && firstCol.Count > secondCol.Count)
                {
                    return false;
                }
                else
                {
                    firstCount = firstCol.Count;
                    secondCount = secondCol.Count;
                }
            }

            if (firstCount == -1)
            {
                firstCount = first.Count();
                secondCount = second.Count();
            }

            using (IEnumerator<TSource> e1 = first.GetEnumerator())
            using (IEnumerator<TSource> e2 = second.Skip(secondCount - firstCount).GetEnumerator())
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