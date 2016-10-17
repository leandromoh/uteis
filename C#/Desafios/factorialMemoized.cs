using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    static class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> factorial = null; // Just so we can refer to it
            Func<int, int> factorialMemoized = null;

            factorial = x => x <= 1 ? 1 : x * factorialMemoized(x - 1);
            factorialMemoized = factorial.Memoize();

            var res = Enumerable.Range(1, 10).Select(x => factorialMemoized(x));

            foreach (var outt in res)
                Console.WriteLine(outt.ToString());

            Console.Read();
        }

        public static Func<A, R> Memoize<A, R>(this Func<A, R> f)
        {
            var d = new Dictionary<A, R>();
            return a =>
            {
                R r;
                if (!d.TryGetValue(a, out r))
                {
                    r = f(a);
                    d.Add(a, r);
                }
                return r;
            };
        }
    }
}
