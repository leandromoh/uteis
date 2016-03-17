using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        public static List<string> names;
        public static bool stop;

        public static void ProcessingMessage()
        {
            string processing;
            while (!stop)
            {
                processing = "Processing";
                Console.WriteLine(processing);
                Thread.Sleep(500);

                for (int i = 0; i < 3; i++)
                {
                    Console.Clear();
                    Console.WriteLine(processing += ".");
                    Thread.Sleep(500);
                }

                Console.Clear();
            }
        }

        public static void Main(string[] args)
        {
            names = new List<string>();

            for(int i = 0; i < 500000; i++)
            {
                names.Add("nome.mp3");
            }

            double[][] resultados = new double[4][];

            for (int i = 0; i < 4; i++)
            {
                resultados[i] = new double[10];
            }

            stop = false;

            Thread t = new Thread(ProcessingMessage);
            t.Start();

            for (int i = 0; i < 10; i++)
            {
                resultados[0][i] = getTime(Rename1).TotalMilliseconds;
                resultados[1][i] = getTime(Rename2).TotalMilliseconds;
                resultados[2][i] = getTime(Rename3).TotalMilliseconds;
                resultados[3][i] = getTime(Rename4).TotalMilliseconds;
            }

            stop = true;

            t.Join();

            Console.WriteLine("\nCalculating Averages...\n");

            Thread.Sleep(1000);

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(string.Format("Performance Method {0}: {1} Milliseconds", i+1, resultados[i].Average()));
            }

            Console.Read();
        }

        public static void Rename1()
        {
            Func<string, string> Concat1 = x => x + "_1";
            Func<string, string> Concat2 = x => x + "_2";
            Func<string, string> Concat3 = x => x + "_3";

            Func<string, string> funcs = x => x;

            funcs += Concat1;
            funcs += Concat2;
            funcs += Concat3;

            Delegate[] funcsToRun = funcs.GetInvocationList();

            Parallel.For(0, names.Count, i =>
            {
                foreach (Func<string, string> f in funcsToRun)
                {
                    names[i] = f(names[i]);
                }
            });
        }

        public static void Rename2()
        {
            Func<string, string> Concat1 = x => x + "_1";
            Func<string, string> Concat2 = x => x + "_2";
            Func<string, string> Concat3 = x => x + "_3";

            Func<string, string> funcs = x => x;

            funcs += Concat1;
            funcs += Concat2;
            funcs += Concat3;

            Parallel.For(0, names.Count, i =>
            {
                foreach (Func<string, string> f in funcs.GetInvocationList())
                {
                    names[i] = f(names[i]);
                }
            });
        }

        public static void Rename3()
        {
            Func<string, string> Concat1 = x => x + "_1";
            Func<string, string> Concat2 = x => x + "_2";
            Func<string, string> Concat3 = x => x + "_3";

            Func<string, string> funcs = x => x;

            funcs += Concat1;
            funcs += Concat2;
            funcs += Concat3;

            Delegate[] funcsToRun = funcs.GetInvocationList();

            Parallel.For(0, names.Count, i =>
            {
                for (int j = 0, len = funcsToRun.Length; j < len; j++)
                {
                    names[i] = ((Func<string, string>)funcsToRun[j])(names[i]);
                }
            });
        }

        public static void Rename4()
        {
            Func<string, string> Concat1 = x => x + "_1";
            Func<string, string> Concat2 = x => x + "_2";
            Func<string, string> Concat3 = x => x + "_3";

            Func<string, string> funcs = x => x;

            funcs += Concat1;
            funcs += Concat2;
            funcs += Concat3;

            Delegate[] funcsToRun = funcs.GetInvocationList();

            for (int i = 0, len1 = names.Count(); i < len1; i++)
            {
                for (int j = 0, len2 = funcsToRun.Length; j < len2; j++)
                {
                    names[i] = ((Func<string, string>)funcsToRun[j])(names[i]);
                }
            };
        }

        public static TimeSpan getTime(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}
