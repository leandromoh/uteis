using System.Collections.Generic;
using System;

namespace EffectiveLINQ
{
    static partial class LinqExtensions
    {
        public static T NthOrDefault<T>(this IEnumerable<T> source, int nth)
        {
            return NthIterator(source, nth, null, true, false);
        }

        public static T Nth<T>(this IEnumerable<T> source, int nth)
        {
            return NthIterator(source, nth, null, false, false);
        }

        public static T NthOrDefault<T>(this IEnumerable<T> source, int nth, Func<T, bool> predicate)
        {
            return NthIterator(source, nth, predicate, true, true);
        }

        public static T Nth<T>(this IEnumerable<T> source, int nth, Func<T, bool> predicate)
        {
            return NthIterator(source, nth, predicate, false, true);
        }

        private static T NthIterator<T>(IEnumerable<T> source, int nth, Func<T, bool> predicate, bool useDefault, bool usePredicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (nth <= 0) throw new ArgumentOutOfRangeException("nth", "nth must be greater than zero.");
            if (usePredicate && predicate == null) throw new ArgumentNullException("predicate");

            int found = 0;

            if (usePredicate)
            {
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        if (++found == nth)
                        {
                            return item;
                        }
                    }
                }
            }
            else
            {
                foreach (var item in source)
                {
                    if (++found == nth)
                    {
                        return item;
                    }
                }
            }

            if (!useDefault)
            {
                if (usePredicate)
                    throw new InvalidOperationException("Sequence does not contains " + nth + " elements that matches the predicate");
                else
                    throw new InvalidOperationException("Sequence does not contains " + nth + " elements");
            }

            return default(T);
        }
    }
}
