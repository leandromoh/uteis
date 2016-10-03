using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    public static class Ex
    {
        public static void Main()
        {

            //4
            Console.WriteLine(new int[] { 8, 12, 24, 4 }.AggregateRight((a, b) => a / b));


            Console.WriteLine(new int[] { 8, 12, 24, 4 }.AggregateRight("2", (a, b) => string.Format("({0} / {1})", a, b)));
            //8
            Console.WriteLine(new int[] { 8, 12, 24, 4 }.AggregateRight(2, (a, b) => a / b));
            //2
            Console.WriteLine(new int[] { }.AggregateRight(2, (a, b) => a / b));


            //7
            Console.WriteLine(new int[] { 8, 12, 24, 4 }.AggregateRight("2", (a, b) => string.Format("{1}{0}", a, b), str => str.Length));

            Console.Read();
        }

        public static TSource AggregateRight<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (func == null) throw new ArgumentNullException("func");
            if (!source.Any()) throw new InvalidOperationException("Sequence contains no elements");

            IList<TSource> e = (source as IList<TSource>) ?? source.ToArray();

            return AggregateRightImp(e, e.Last(), func, e.Count - 1);
        }

        public static TAccumulate AggregateRight<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TSource, TAccumulate, TAccumulate> func)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (func == null) throw new ArgumentNullException("func");

            IList<TSource> e = (source as IList<TSource>) ?? source.ToArray();

            return AggregateRightImp(e, seed, func, e.Count);
        }

        public static TResult AggregateRight<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TSource, TAccumulate, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (func == null) throw new ArgumentNullException("func");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");

            return resultSelector(source.AggregateRight(seed, func));
        }

        public static TResult AggregateRightImp<TSource, TResult>(IList<TSource> e, TResult current, Func<TSource, TResult, TResult> func, int i)
        {
            while (i-- > 0)
            {
                current = func(e[i], current);
            }

            return current;
        }

    }
}
