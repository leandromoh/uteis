using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] even = { 0, 2, 4, 6, 8 };
            int[] odd = { 1, 3, 5, 7, 9 };
            int[] primes = { 2, 3, 5, 7, 11 };
            int[] natural = { 1, 2, 3, 4, 5 };

            foreach (var i in odd.ZipWithMany(even, primes, natural, (a, b, c, d) => a + b + c + d))
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }
    }

    public static class Ex
    {
        public static IEnumerable<T> ZipWithMany<T>(this IEnumerable<T> i1, IEnumerable<T> i2, IEnumerable<T> i3, IEnumerable<T> i4, Func<T, T, T, T, T> func)
        {
            IEnumerator<T>[] e = new[] { i1, i2, i3, i4 }.Select(p => p.GetEnumerator()).ToArray();

            while (e.All(p => p.MoveNext()))
            {
                yield return func(e[0].Current, e[1].Current, e[2].Current, e[3].Current);
            }
        }
    }
}
