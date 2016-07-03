using System;
using System.Linq;

namespace DummyProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://msdn.microsoft.com/en-us/library/aa664573(VS.71).aspx

            int[] a1 = new int[2] { 1, 2 };

            int[] a2 = { 1, 2 };

            var a3 = new[] { 1, 2 };

            Console.WriteLine(a1.SequenceEqual(a2) && a2.SequenceEqual(a3)); // True

            Console.Read();
        }
    }
}
