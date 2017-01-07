using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public static class Bla
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(Bla.getDestructor(i) + "\n\n");
            }

            Console.Read();
        }
        public static string getDestructor(int n)
        {
            var numbers = Enumerable.Range(1, n);

            var sb = new StringBuilder(
@"static bool Deconstruct<T>(this IEnumerable<T> source, {0} out IEnumerable<T> tail)
{
{1}
    tail = null;

    var e = source.GetEnumerator();

    {2}

	e.Dispose();
    tail = source.Skip({3});
    return true;
}");
            return sb
                .Replace("{0}", string.Join("", numbers.Select(i => string.Format("out T x{0},", i))))
                .Replace("{1}", string.Join("\n", numbers.Select(i => string.Format("x{0} = default(T);", i))))
                .Replace("{2}", string.Join("\n", numbers.Select(i => string.Format("if (!e.MoveNext()) return false;\n\tx{0} = e.Current;\n", i))))
                .Replace("{3}", n.ToString()).ToString();
        }
    }
}
