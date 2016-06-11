using System;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Func<Task<int>> task = async () =>
            {
                await Task.Delay(3000);

                return 24;
            };

            Task<int> t = task();

            //Your another logic code...

            t.Wait();

            Console.WriteLine(t.Result);
        }
    }
}
