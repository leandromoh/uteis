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
        public static void Main(string[] args)
        {
            Console.WriteLine("Calling Click Method");
            Task x = Click();

            while (!x.IsCompleted)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Main thread Active");
            }

            Thread.Sleep(1000);
            Console.WriteLine("Main thread Finished");

            Console.Read();
        }

        public static async Task Click()
        {
            Console.WriteLine("\tClick Method Enter");
            await IncrementProgressBar();
            Console.WriteLine("\tClick Method Exits");
        }

        public static async Task IncrementProgressBar()
        {
            for (int i = 0; i < 101; i += 10)
            {
                await Task.Delay(1000);
                Console.WriteLine(string.Format("\t\t{0}% complete", i));
            }
        }
    }
}
