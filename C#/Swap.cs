using System;
using MoreLinq;
using System.Linq;
using System.Collections.Generic;

namespace conso
{
    static class Program
    {
        static void Main(string[] args)
        {
            var nums = Enumerable.Range(0, 10);
            
            for (int i = 0; i < 13; i++)
                Dae(nums, 5, 3, i);

            // Enumerable.Range(1,7).InsertAt(new[]{8,9,10}, 0).Print();
            // Console.WriteLine();
            // Enumerable.Range(1,7).InsertAt(new[]{8,9,10}, +1).Print();
            // Enumerable.Range(1,7).InsertAt(new[]{8,9,10}, +2).Print();
            // Enumerable.Range(1,7).InsertAt(new[]{8,9,10}, +3).Print();
            // Console.WriteLine();
            // Enumerable.Range(1,7).InsertAt(new[]{8,9,10}, -1).Print();
            // Enumerable.Range(1,7).InsertAt(new[]{8,9,10}, -2).Print();
            // Enumerable.Range(1,7).InsertAt(new[]{8,9,10}, -3).Print();
            // Console.WriteLine();
            // new[]{1,2}.InsertAt(new[]{3,4}, -3).Print();
            // new[]{1,2}.InsertAt(new[]{3,4}, -8).Print();
            

        }

        static void Print<T>(this IEnumerable<T> source)
        {
            Console.WriteLine("[" + String.Join(", ", source.Select(x => x.ToString())) + "]");
        }

        static IEnumerable<T> InsertAt<T>(this IEnumerable<T> source,  IEnumerable<T> second, int index)
        {
            var i = 0;

            if (index < 0)
            {
                if (index == -1)
                {
                    foreach (var item in source.Concat(second))
                        yield return item;

                    yield break;
                }
                
                var count = Math.Abs(index) - 1;
                var queue = new Queue<T>(count);

                foreach (var item in source)
                {
                    if (queue.Count < count)
                    {
                        queue.Enqueue(item);
                        continue;
                    }

                    yield return queue.Dequeue();
                    queue.Enqueue(item);
                }

                foreach (var item in second.Concat(queue))
                    yield return item;

                yield break;
            }

            using (var ector = source.GetEnumerator())
            {
                while (i++ < index && ector.MoveNext())
                    yield return ector.Current;

                foreach (var item in second)
                    yield return item;

                while (ector.MoveNext())
                    yield return ector.Current;
            }
        }

        static IEnumerable<T> Bar<T>(this IEnumerable<T> source, int index, int count, int putAt)
        {
            var slc = source.Slice(index, count);
            var exc = source.Exclude(index, count);

            return exc.InsertAt(slc, putAt);
        }

        static void Dae<T>(this IEnumerable<T> source, int index, int count, int putAt)
        {
            var x = source.Bar(index, count, putAt);
            var y = source.Foo(index, count, putAt);
            var b = x.SequenceEqual(y);

            if (b)
                Console.WriteLine(b);
            else
            {
                x.Print();
                y.Print();
            }
        }

        static IEnumerable<T> Foo<T>(this IEnumerable<T> source, int index, int count, int putAt)
        {
            if (putAt == index)
                return source;

            if (putAt < index)
                return galinha(putAt, index - putAt);
            else
                return galinha(index, count);

            IEnumerable<T> galinha(int take, int skipTake)
            {
                var nums = ToEnumerable(source.GetEnumerator());
                var inter = Math.Abs(putAt - index);

                foreach (var x in nums.Take(take))
                    yield return x;

                var list = nums.Take(inter + count).ToList();

                foreach (var x in list.Skip(skipTake))
                    yield return x;

                foreach (var x in list.Take(skipTake))
                    yield return x;

                foreach (var x in nums)
                    yield return x;
            }

            IEnumerable<T> ToEnumerable<T>(IEnumerator<T> ector)
            {
                while (ector.MoveNext())
                    yield return ector.Current;
            }
        }
    }
}
