using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Enumerable.Range(1, 2).atMost(3)); // true
            Console.WriteLine(Enumerable.Range(1, 3).atMost(3)); // true
            Console.WriteLine(Enumerable.Range(1, 4).atMost(3)); // false

            Console.WriteLine();

            Console.WriteLine(Enumerable.Range(1, 2).atLeast(3)); // false
            Console.WriteLine(Enumerable.Range(1, 3).atLeast(3)); // true
            Console.WriteLine(Enumerable.Range(1, 4).atLeast(3)); // true

            Console.Read();
        }
    }

    public static class xx
    {
        public static bool atLeast<T>(this IEnumerable<T> s, int min)
        {
            var count = 0;

            using (var e = s.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (++count == min)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool atMost<T>(this IEnumerable<T> s, int max)
        {
            var count = 0;

            using (var e = s.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (++count > max)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
