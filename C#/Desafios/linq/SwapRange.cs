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
            Enumerable.Range(0, 7).InsertAt(new[] { 97, 98, 99 }, 8).Print();

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
                Console.WriteLine(putAt + " " + b);
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

            if (putAt == -1)
                return galinha(index, count, x => x);

            if (putAt < 0)
                return SkipLast();

            if (putAt < index)
                return galinha(putAt, index - putAt, x => x.Take(count));
            else
                return galinha(index, count, x => x.Take(putAt - index));

            IEnumerable<T> galinha(int x, int y, Func<IEnumerable<T>, IEnumerable<T>> func)
            {
                var nums = ToEnumerable(source.GetEnumerator());

                foreach (var item in nums.Take(x)) yield return item;

                var list = nums.Take(y).ToList();

                foreach (var item in func(nums)) yield return item;

                foreach (var item in list) yield return item;

                foreach (var item in nums) yield return item;
            }

            IEnumerable<T> SkipLast()
            {
                var skip = Math.Abs(putAt) - 1;
                var queue = new Queue<T>(skip);
                var i = -1;
                var endIndex = index + count - 1;
                var list = new List<T>();

                foreach (var item in source)
                {
                    i++;

                    if (index <= i && i <= endIndex)
                    {
                        list.Add(item);
                        continue;
                    }

                    if (queue.Count < skip)
                    {
                        queue.Enqueue(item);
                        continue;
                    }

                    yield return queue.Dequeue();
                    queue.Enqueue(item);
                }

                foreach (var item in list) yield return item;

                foreach (var item in queue) yield return item;
            }

            IEnumerable<T> ToEnumerable<T>(IEnumerator<T> ector)
            {
                while (ector.MoveNext())
                    yield return ector.Current;
            }
        }
    }
}
