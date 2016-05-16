using System;
using System.Text;

namespace ConsoleApplication1.df
{
    class Program
    {
        public static void Main()
        {
            int number = 6;
            Console.WriteLine(number.isBetween(0, 10)); // True
        }
    }

    // Extension methods are just syntactic sugar and get transformed into static method calls passing in the instance.
    public static class MyExtensions
    {
        public static bool isBetween(this int i, int fromInclusive, int toInclusive)
        {
            return fromInclusive <= i && i <= toInclusive;
        }
    }
}
