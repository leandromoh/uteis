using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var n in quicksort(new int[] { 0, 9, 2, 7, 4, 5, 6, 3, 8, 1 }))
            {
                Console.Write(n + " ");
            }

            Console.Read();
        }

        static IEnumerable<TSource> quicksort1<TSource>(TSource pivot, IEnumerable<TSource> source)
            where TSource : IComparable
        {
            IEnumerable<TSource> lowers = null, greaters = null;

            var t1 = Task.Run(() => lowers = quicksort(source.Where(x => x.CompareTo(pivot) <= 0)));
            var t2 = Task.Run(() => greaters = quicksort(source.Where(x => x.CompareTo(pivot) > 0)));

            Task.WaitAll(t1, t2);

            return lowers.Concat(new[] { pivot }).Concat(greaters);
        }

        static IEnumerable<TSource> quicksort2<TSource>(TSource pivot, IEnumerable<TSource> source)
            where TSource : IComparable
        {
            var lowers = Task.Run(() => quicksort(source.Where(x => x.CompareTo(pivot) <= 0)));
            var greaters = Task.Run(() => quicksort(source.Where(x => x.CompareTo(pivot) > 0)));

            return lowers.Result.Concat(new[] { pivot }).Concat(greaters.Result);
        }

        static IEnumerable<TSource> quicksort<TSource>(IEnumerable<TSource> source)
            where TSource : IComparable
        {
            if (source.Any())
                return quicksort2(source.First(), source.Skip(1));

            return source;
        }
    }
}