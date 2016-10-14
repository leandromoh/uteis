using System.Collections.Generic;
using System;

namespace EffectiveLINQ
{
    static partial class LinqExtensions
    {
        public static T NthOrDefault<T>(this IEnumerable<T> source, int nth)
        {
            return NthIterator(source, nth, true);
        }

        public static T NthOrDefault<T>(this IEnumerable<T> source, int nth, Func<T, bool> predicate)
        {
            return NthIterator(source, nth, true, predicate);
        }

        public static T Nth<T>(this IEnumerable<T> source, int nth)
        {
            return NthIterator(source, nth, false);
        }

        public static T Nth<T>(this IEnumerable<T> source, int nth, Func<T, bool> predicate)
        {
            return NthIterator(source, nth, false, predicate);
        }

        private static T NthIterator<T>(IEnumerable<T> source, int nth, bool useDefault)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (nth <= 0) throw new ArgumentOutOfRangeException("nth", "nth must be greater than zero.");

            int found = 0;

            foreach (var item in source)
            {
                checked
                {
                    if (++found == nth)
                    {
                        return item;
                    }
                }
            }

            if (!useDefault)
                throw new InvalidOperationException("Sequence does not contains " + nth + " elements");

            return default(T);
        }

        private static T NthIterator<T>(IEnumerable<T> source, int nth, bool useDefault, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");
            if (nth <= 0) throw new ArgumentOutOfRangeException("nth", "nth must be greater than zero.");

            int found = 0;

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    checked
                    {
                        if (++found == nth)
                        {
                            return item;
                        }
                    }
                }
            }

            if (!useDefault)
                throw new InvalidOperationException("Sequence does not contains " + nth + " elements that matches the predicate");

            return default(T);
        }
    }
}
