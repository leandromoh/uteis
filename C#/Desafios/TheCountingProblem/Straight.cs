using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Test3
    {
        public static void Main()
        {
            var input = new string[2];
            int a;
            int b;
            Stopwatch stopwatch = Stopwatch.StartNew();
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "uva.txt")))
            {
                do
                {
                    input = sr.ReadLine().Split(' ');
                    a = Int32.Parse(input[0]);
                    b = Int32.Parse(input[1]);

                    countDigitsLineRapido(Math.Min(a, b), Math.Max(a, b));

                } while (a != 0 || b != 0);
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            Console.ReadKey();
        }

        static void countDigitsLineRapido(int a, int b)
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

            for (int i = 0, len = c.Length; i < len; i++)
                Console.Write(c[i] + " ");
            Console.Write("\n");
        }
    }
}