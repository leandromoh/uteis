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
            Task x = Task.Factory.StartNew(Click);

            while (!x.IsCompleted)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Main thread Active");
            }

            Thread.Sleep(1000);
            Console.WriteLine("Main thread Finished");

            Console.Read();
        }

        public static void Click()
        {
            Console.WriteLine("\tClick Method Enter");
            Task.Factory.StartNew(IncrementProgressBar, TaskCreationOptions.AttachedToParent)
                .ContinueWith(task =>
                    Console.WriteLine("\tClick Method Exits")
                );
        }

        public static void IncrementProgressBar()
        {
            for (int i = 0; i < 101; i += 10)
            {
                Thread.Sleep(1000);
                Console.WriteLine(string.Format("\t\t{0}% complete", i));
            }
        }
    }
}
