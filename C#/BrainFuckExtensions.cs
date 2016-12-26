using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public static class BrainFuckExtensions
    {
        private static Mult[] multiplicacoes = Enumerable.Range(1, 20).Select(tabuada).SelectMany(x => x).ToArray();

        //compile brain fuck online https://copy.sh/brainfuck/
        public static string ToBrainFuck(this string str)
        {
            StringBuilder sb = new StringBuilder();

            int[] ascii = str.Replace(@"\n", "\n").Select(c => (int)c).ToArray();

            foreach (var s in ascii)
            {
                var mult = multiplicacoes.MinBy(x => Math.Abs(x.res - s));
                var min = Math.Min(mult.esq, mult.dir);
                var max = Math.Max(mult.esq, mult.dir);
                var res = Math.Abs(mult.res - s);
                var cha = mult.res > s ? '-' : '+';

                sb.Append(string.Format(">{0}[<{1}>-]<{2}.", new string('+', max), new string('+', min), new string(cha, res)));
                sb.Append("[-]");
            }

            return sb.ToString();
        }

        public static T MinBy<T, M>(this IEnumerable<T> source, Func<T, M> selector)
            where M : IComparable
        {
            var t = source.First();
            var min = selector(t);
            foreach (var item in source.Skip(1))
            {
                var m = selector(item);
                if (m.CompareTo(min) < 0)
                {
                    min = m;
                    t = item;
                }

            }
            return t;
        }

        static IEnumerable<Mult> tabuada(int x)
        {
            for (int soma = 0, i = 1; i < 11; i++)
            {
                soma += x;
                yield return new Mult(x, i, soma);
            }
        }

        class Mult
        {
            public int esq;
            public int dir;
            public int res;

            public Mult(int a, int b, int c)
            {
                esq = a;
                dir = b;
                res = c;
            }
        }
    }
}
