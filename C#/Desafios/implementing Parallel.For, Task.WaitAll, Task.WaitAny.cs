using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public static class MyParallel
    {
        public static void For(int fromInclusive, int toExclusive, Action<int> body)
        {
            int leng = toExclusive - fromInclusive;

            Task[] tasks = new Task[leng];

            for (int i = 0; i < leng; i++)
            {
                tasks[i] = new Task(() => body(fromInclusive++));
                tasks[i].Start();
            }

            WaitAll(tasks);
        }

        public static void WaitAll(Task[] tasks)
        {
            List<Task> list = tasks.ToList();
            int i;

            while (list.Count != 0)
            {
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].IsCompleted)
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public static int WaitAny(Task[] tasks)
        {
            int i;

            while (true)
            {
                for (i = 0; i < tasks.Length; i++)
                {
                    if (tasks[i].IsCompleted)
                    {
                        return i;
                    }
                }
            }
        }

        public static void Main()
        {
            Console.WriteLine("    start");

            MyParallel.For(0, 20, i =>
            {
                Console.WriteLine(i);
            });

            Console.WriteLine("    end");

            Console.Read();
        }
    }
}