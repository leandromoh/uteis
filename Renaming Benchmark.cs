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
        public delegate void FunRef(ref string x);

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
            const int numberFuncs = 8;
            const int numberTestsPerFuncs = 10;

            double[][] resultados = new double[numberFuncs][];

            for (int i = 0; i < numberFuncs; i++)
            {
                resultados[i] = new double[numberTestsPerFuncs];
            }

            stop = false;

            Thread t = new Thread(ProcessingMessage);
            //t.Start();

            for (int i = 0; i < numberTestsPerFuncs; i++)
            {
                resultados[0][i] = getTime(Rename1).TotalMilliseconds;
                resultados[1][i] = getTime(Rename2).TotalMilliseconds;
                resultados[2][i] = getTime(Rename3).TotalMilliseconds;
                resultados[3][i] = getTime(Rename4).TotalMilliseconds;
                resultados[4][i] = getTime(Rename5).TotalMilliseconds;
                resultados[5][i] = getTime(Rename6).TotalMilliseconds;
                resultados[6][i] = getTime(Rename7).TotalMilliseconds;
                resultados[7][i] = getTime(Rename8).TotalMilliseconds;

                //  Thread.Sleep(50000);
            }

            stop = true;

            //t.Join();

            Console.WriteLine("\nCalculating Averages...\n");

            Thread.Sleep(1000);

            for (int i = 0; i < numberFuncs; i++)
            {
                Console.WriteLine(string.Format("Performance Method {0}: {1} Milliseconds", i + 1, resultados[i].Average()));
            }

            Console.Read();
        }

        public static void Rename1()
        {
            ResetList();

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

            //        Console.WriteLine("1 " + names[0]);
        }

        public static void Rename2()
        {
            ResetList();

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

            //     Console.WriteLine("2 " + names[0]);
        }

        public static void Rename3()
        {
            ResetList();

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

            //      Console.WriteLine("3 " + names[0]);
        }

        public static void Rename4()
        {
            ResetList();

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

            //      Console.WriteLine("4 " + names[0]);
        }

        public static void Rename5()
        {
            ResetList();

            List<FunRef> funcs = new List<FunRef>();

            funcs.Add((ref string x) => { x = x + "_1"; });
            funcs.Add((ref string x) => { x = x + "_2"; });
            funcs.Add((ref string x) => { x = x + "_3"; });

            Parallel.For(0, names.Count, i =>
            {
                string x = names[i];
                for (int j = 0, len = funcs.Count; j < len; j++)
                {
                    funcs[j](ref x);
                }
                names[i] = x;
            });
            //      Console.WriteLine("5 " + names[0]);
        }

        public static void Rename6()
        {
            ResetList();

            FunRef Concat1 = (ref string x) => { x = x + "_1"; };
            FunRef Concat2 = (ref string x) => { x = x + "_2"; };
            FunRef Concat3 = (ref string x) => { x = x + "_3"; };

            FunRef funcs = (ref string x) => { };

            funcs += Concat1;
            funcs += Concat2;
            funcs += Concat3;

            Delegate[] funcsToRun = funcs.GetInvocationList();

            Parallel.For(0, names.Count, i =>
            {
                string x = names[i];
                for (int j = 0, len = funcsToRun.Length; j < len; j++)
                {
                    ((FunRef)funcsToRun[j])(ref x);
                }
                names[i] = x;
            });

            //       Console.WriteLine("6 " + names[0]);
        }

        public static void Rename7()
        {
            ResetList();

            List<Func<string, string>> funcs = new List<Func<string, string>>();

            funcs.Add((string x) => x + "_1");
            funcs.Add((string x) => x + "_2");
            funcs.Add((string x) => x + "_3");

            Parallel.For(0, names.Count, i =>
            {
                for (int j = 0, len = funcs.Count; j < len; j++)
                {
                    names[i] = funcs[j](names[i]);
                }
            });
            //      Console.WriteLine("7 " + names[0]);
        }

        public static void Rename8()
        {
            ResetList();

            List<Func<string, string>> funcs = new List<Func<string, string>>();

            funcs.Add((string x) => x + "_1");
            funcs.Add((string x) => x + "_2");
            funcs.Add((string x) => x + "_3");

            for (int i = 0, len1 = names.Count; i < len1; i++)
            {
                for (int j = 0, len2 = funcs.Count; j < len2; j++)
                {
                    names[i] = funcs[j](names[i]);
                }
            };

            //      Console.WriteLine("8 " + names[0]);
        }

        public static void ResetList()
        {
            names = new List<string>();

            for (int i = 0; i < 500000; i++)
            {
                names.Add("nome.mp3");
            }
        }

        public static TimeSpan getTime(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            GC.Collect();
            return stopwatch.Elapsed;
        }
    }
}
