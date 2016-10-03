using System.Collections.Generic;
using System.Linq;
using System;

namespace EffectiveLINQ
{
    static partial class LinqExtensions
    {
        public static bool AtLeast<T>(this IEnumerable<T> source, int min)
        {
            if (min < 0) throw new ArgumentOutOfRangeException("min", "min must not be negative.");

            return QuantityIterator(source, min, count => count >= min);
        }

        public static bool AtMost<T>(this IEnumerable<T> source, int max)
        {
            if (max < 0) throw new ArgumentOutOfRangeException("max", "max must not be negative.");

            return QuantityIterator(source, max + 1, count => count <= max);
        }

        public static bool Exactly<T>(this IEnumerable<T> source, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "length must not be negative.");

            return QuantityIterator(source, length + 1, count => count == length);
        }

        public static bool CountBetween<T>(this IEnumerable<T> source, int min, int max)
        {
            if (min < 0) throw new ArgumentOutOfRangeException("min", "min must not be negative.");
            if (max < min) throw new ArgumentOutOfRangeException("max", "max must not be lesser than min.");

            return QuantityIterator(source, max + 1, count => min <= count && count <= max);
        }

        private static bool QuantityIterator<T>(IEnumerable<T> source, int limit, Func<int, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");

            var col = source as ICollection<T>;

            if (col != null)
            {
                return predicate(col.Count);
            }

            var count = 0;

            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (++count == limit)
                    {
                        break;
                    }
                }
            }

            return predicate(count);
        }
    }
    


    class CountTest
    {
        public static void Main()
        {
            Console.WriteLine(Enumerable.Range(1, 2).AtLeast(3) == false);
            Console.WriteLine(Enumerable.Range(1, 3).AtLeast(3) == true);
            Console.WriteLine(Enumerable.Range(1, 4).AtLeast(3) == true);

            Console.WriteLine();

            Console.WriteLine(Enumerable.Range(1, 2).AtMost(3) == true);
            Console.WriteLine(Enumerable.Range(1, 3).AtMost(3) == true);
            Console.WriteLine(Enumerable.Range(1, 4).AtMost(3) == false);

            Console.WriteLine();

            Console.WriteLine(Enumerable.Range(1, 2).Exactly(3) == false);
            Console.WriteLine(Enumerable.Range(1, 3).Exactly(3) == true);
            Console.WriteLine(Enumerable.Range(1, 4).Exactly(3) == false);

            Console.WriteLine();

            Console.WriteLine(Enumerable.Range(1, 1).CountBetween(2, 4) == false);
            Console.WriteLine(Enumerable.Range(1, 2).CountBetween(2, 4) == true);
            Console.WriteLine(Enumerable.Range(1, 3).CountBetween(2, 4) == true);
            Console.WriteLine(Enumerable.Range(1, 4).CountBetween(2, 4) == true);
            Console.WriteLine(Enumerable.Range(1, 5).CountBetween(2, 4) == false);

            Console.Read();
        }
    }
}
