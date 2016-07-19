using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] even = { 0, 2, 4, 6, 8, 11 };
            int[] odd = { 1, 3, 5, 7, 9 };
            int[] primes = { 2, 3, 5, 7, 11 };
            int[] natural = { 1, 2, 3, 4, 5 };

            foreach (var i in odd.ZipWith4(even, primes, natural, (a, b, c, d) => a + b + c + d))
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }
    }

    public static class Ex
    {
        public static IEnumerable<T> ZipWith3<T>(this IEnumerable<T> i1, IEnumerable<T> i2, IEnumerable<T> i3, Func<T, T, T, T> func)
        {
            foreach (var item in sele(func, i1, i2, i3))
            {
                yield return item;
            }
        }

        public static IEnumerable<T> ZipWith4<T>(this IEnumerable<T> i1, IEnumerable<T> i2, IEnumerable<T> i3, IEnumerable<T> i4, Func<T, T, T, T, T> func)
        {
            foreach (var item in sele(func, i1, i2, i3, i4))
            {
                yield return item;
            }
        }

        public static IEnumerable<T> ZipWith5<T>(this IEnumerable<T> i1, IEnumerable<T> i2, IEnumerable<T> i3, IEnumerable<T> i4, IEnumerable<T> i5, Func<T, T, T, T, T, T> func)
        {
            foreach (var item in sele(func, i1, i2, i3, i4, i5))
            {
                yield return item;
            }
        }

        public static IEnumerable<T> ZipWith6<T>(this IEnumerable<T> i1, IEnumerable<T> i2, IEnumerable<T> i3, IEnumerable<T> i4, IEnumerable<T> i5, IEnumerable<T> i6, Func<T, T, T, T, T, T, T> func)
        {
            foreach (var item in sele(func, i1, i2, i3, i4, i5, i6))
            {
                yield return item;
            }
        }

        private static IEnumerable<T> sele<T>(Delegate func, params IEnumerable<T>[] list)
        {
            var e = list.Select(p => p.GetEnumerator()).ToArray();

            while (e.All(p => p.MoveNext()))
            {
                var args = e.Select(p => p.Current).Cast<object>().ToArray();
                yield return (T)func.DynamicInvoke(args);
            }

            Array.ForEach(e, p => ((IDisposable)p).Dispose());
        }
    }
}
