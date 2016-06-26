using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, bool> f1 = i => i % 2 == 0;
            Func<int, bool> f2 = i => i % 3 == 0;

            var fx = f1.And(f2).And(i => i > 10, i => i != 12);

            Console.WriteLine(fx(24));
            Console.Read();
        }
    }

    public static class Extension
    {
        public static Func<T, bool> And<T>(this Func<T, bool> fx, params Func<T, bool>[] predicates)
        {
            return t => fx(t) && predicates.All(f => f(t));
        }

        public static Func<T, bool> Or<T>(this Func<T, bool> fx, params Func<T, bool>[] predicates)
        {
            return t => fx(t) || predicates.Any(f => f(t));
        }
    }
}
