using System;
using System.Diagnostics;
using System.Threading;
namespace PerformanceCounters
{
    //https://msdn.microsoft.com/pt-br/library/2e08f6yc(v=vs.110).aspx

    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int, string> c = conta;

            IAsyncResult r = c.BeginInvoke(1, 10, null, null);

            while (!r.IsCompleted)
            {
                Console.WriteLine("main");
                Thread.Sleep(1000);
            }

            // obs: EndInvoke blocks the current Thread until the async method finish
            
            Console.WriteLine("result: " + c.EndInvoke(r));
            Console.Read();
        }

        static string conta(int a, int b)
        {
            for (; a < b; a++)
            {    
                Console.WriteLine(a);
                Thread.Sleep(500);
            }

            return "async method finish";
        }
    }
}