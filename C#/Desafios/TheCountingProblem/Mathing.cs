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
            int boolx = 0;
            bool asd = true;
            int min = 0, max = 10000;
            //Console.WriteLine(getZeros(1000000));
            for (; max <= 10020000; max += 10000)
            {
                if (!asd)
                {
                    boolx++;
                }

                if (boolx > 3)
                {
                    max -= 30000;
                }

                #region [a]
                Console.Write(max + "  ");
                var temp1 = countDigitsLineRapido(min, max).Take(1);
                printArray(temp1);
                Console.Write("   ");
                #endregion [a]
                    #region [bla]
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
                    #endregion [bla]
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

                    var temp2 = r.Take(1);
                    printArray(temp2);
                    int gg = temp1.First();

                    Console.WriteLine("   " + (asd = temp2.First() == gg));


            }
  

        end:
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

        static bool So1numero1(int num)
        {
            while (num != 0)
            {
                if (num % 10 != 0)
                {
                    return num == 1;
                }

                num /= 10;
            }

            return false;
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

    public static class MyExtensions
    {
        public static bool isBetween(this int i, int fromInclusive, int toInclusive)
        {
            return fromInclusive <= i && i <= toInclusive;
        }
    }
}
