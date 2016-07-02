using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        public static void Main(string[] args)
        {
            IEnumerable<int> r1, r2;
            int min = 0, max = 10 * 1000;

            for (; max <= 110020000; max += 10000)
            {
                Console.WriteLine(max);

                r1 = countDigitsLineRapido(min, max);

                r2 = countDigitsMathing(min, max);

                if (!r1.SequenceEqual(r2))
                {
                    printArray(r1);
                    printArray(r2);
                    break;
                }
            }

            Console.Read();
        }

        private static int getZeros(int num)
        {
            int x = 0;
            num /= 10;
            while (num != 0)
            {
                if (num % 10 == 0)
                {
                    x++;
                }
                num /= 10;

            }

            return x;
        }

        static int[] countDigitsMathing(int min, int max)
        {
            var dezmil = 1000;
            var r = countDigitsLineRapido(0, dezmil);

            var x = max - min;

            int a = 1;
            int num;
            int b = max / dezmil;

            if (x >= dezmil)
            {
                x /= dezmil;

                for (int f = 0; f < 10; f++)
                    r[f] *= x;

                for (; a < b; a++)
                {
                    num = a;

                    while (num != 0)
                    {
                        r[num % 10] += dezmil;
                        num /= 10;
                    }
                }
            }

            num = max / dezmil;
            while (num != 0)
            {
                r[num % 10] += 1;
                num /= 10;
            }
            r[1] -= x;

            if (x < 11)
            {
                r[0] = (x - 1) * 300 + 192;
                r[0]++;
            }
            else
            {
                r[0] = (10 - 1) * 300 + 192;
                a = 10;
                while (a < b)
                {
                    a += 10;
                    ;
                    r[0] += 4000;
                    r[0] += getZeros(a - 10) * 10000;
                }

                r[0] += getZeros(b) + 1;
            }

            return r;
        }

        static int[] countDigitsLineRapido(int a, int b)
        {
            var c = new int[10];
            int num;
            b++;

            for (; a < b; a++)
            {
                num = a;

                while (num != 0)
                {
                    c[num % 10]++;
                    num /= 10;
                }
            }

            return c;
        }

        static void printArray(IEnumerable<int> c)
        {
            for (int i = 0, len = c.Count(); i < len; i++)
                Console.Write(c.ElementAt(i) + " ");

            Console.WriteLine();
        }

        static void printTimeSpent(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            GC.Collect();
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}
