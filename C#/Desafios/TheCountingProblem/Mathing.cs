using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int min = 0, max = 16500000;

            printTimeSpent(() => printArray(countDigitsLineRapido(min, max)));

            Console.WriteLine();
            
            printTimeSpent(() =>
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
                r[0] = (x - 1) * 300 + 192;
                printArray(r);
            });
            Console.Read();

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

        static void printArray(int[] c)
        {
            for (int i = 0, len = c.Length; i < len; i++)
                Console.Write(c[i] + " ");

            Console.Write("\n");
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
