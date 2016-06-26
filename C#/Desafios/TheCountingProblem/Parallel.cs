using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class SolucaoSimplistaThread
    {
        public static void Main()
        {
            var counts = new int[500][];

            var input = new string[2];
            var ranges = new int[500, 2];
            var i = -1;

            using (StreamReader sr = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "uva.txt")))
            {
                do
                {
                    i++;
                    input = sr.ReadLine().Split(' ');
                    ranges[i, 0] = Int32.Parse(input[0]);
                    ranges[i, 1] = Int32.Parse(input[1]);

                } while (ranges[i, 0] != 0 || ranges[i, 1] != 0);
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            Parallel.For(0, i, x =>
            {
                int min = Math.Min(ranges[x, 0], ranges[x, 1]);
                int max = Math.Max(ranges[x, 0], ranges[x, 1]);

                counts[x] = countDigitsLine(min, max);
            });

            for (int d = 0; d < i; d++)
                printArray(counts[d]);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            Console.ReadKey();
        }

        static int[] countDigitsNumber(int num)
        {
            var c = new int[10];

            if (num == 0) c[0]++;

            while (num != 0)
            {
                c[num % 10]++;
                num /= 10;
            }

            return c;
        }

        static int[] countDigitsLine(int a, int b)
        {
            b++;
            var r = new int[10];
            var countS = new int[b - a][];

            Parallel.For(a, b, y =>
            {
                countS[y - a] = countDigitsNumber(y);
            });

            for (int c = 0; c < 10; c++)
                for (int l = 0; l < countS.Length; l++)
                    r[c] += countS[l][c];

            return r;
        }

        static void printArray(int[] a)
        {
            for (int i = 0, len = a.Length; i < len; i++)
                Console.Write(a[i] + " ");

            Console.Write("\n");
        }
    }
}