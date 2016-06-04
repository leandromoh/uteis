using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    class Program
    {
        public static IEnumerable<ulong> Fibonacci()
        {
            ulong a = 1;
            ulong b = 1;
            ulong c = a + b;

            yield return a; // The first two values are 1
            yield return b;

            // Now, each time we continue execution, generate the next entry.
            while (c > a && c > b) //when you overflow the maxValue of an numeric type (int, long, etc) strange things happens!
            {
                yield return c;
                a = b;
                b = c;
                c = a + b;
            }
        }

        public static void Main()
        {
            // Call the Fibonacci function, which 
            // immediately returns an IEnumerable<ulong>
            // (No code in Fibonnacci is run)
            var fib = Fibonacci();

            foreach (var i in fib.Take(5))
            {
                Console.WriteLine(i + " ");
            }

            Console.ReadKey();
        }
    }

    public static class Extensions
    {
        public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count) {
            if (count > 0) {
                foreach (TSource element in source) {
                    yield return element;
                    if (--count == 0) break;
                }
            }
        }
    }
}
