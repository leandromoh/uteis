using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "um", "dois", "tres", "quatro", "cinco" };
            int[] natural = { 1, 2, 3, 4, 5 };
            bool[] flags = { true, false, true, false, true };

            foreach (var i in names.Zip(natural, flags, (a, b, c) => c ? Tuple.Create(a, b) : null))
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }
    }

    public static class Ex
    {
        public static IEnumerable<TResult> Zip<T1, T2, T3, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, Func<T1, T2, T3, TResult> func)
        {
            return sele<TResult>(func, i1, i2, i3);
        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, Func<T1, T2, T3, T4, TResult> func)
        {
            return sele<TResult>(func, i1, i2, i3, i4);

        }

        public static IEnumerable<TResult> Zip<T1, T2, T3, T4, T5, TResult>(this IEnumerable<T1> i1, IEnumerable<T2> i2, IEnumerable<T3> i3, IEnumerable<T4> i4, IEnumerable<T5> i5, Func<T1, T2, T3, T4, T5, TResult> func)
        {
            return sele<TResult>(func, i1, i2, i3, i4, i5);
        }

        private static IEnumerable<TResult> sele<TResult>(Delegate func, params IEnumerable[] list)
        {
            var e = list.Select(p => p.GetEnumerator()).ToArray();

            while (e.All(p => p.MoveNext()))
            {
                yield return (TResult)func.DynamicInvoke(e.Select(p => p.Current).ToArray());
            }

            Array.ForEach(e, p =>
            {
                var d = p as IDisposable;

                if (d != null)
                    d.Dispose();
            });
        }
    }
}
