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

    /*
        Extension methods are just syntactic sugar and are compiled into a call on the static method passing the instance as argument.
        Therefore, the principle of encapsulation is not really being violated.
        In fact, extension methods cannot access private variables in the type they are extending.
        
        Conclusion: number.isBetween(0, 10) becomes MyExtensions.isBetween(number, 0, 10)
    */
    
    public static class MyExtensions
    {
        public static bool isBetween(this int i, int fromInclusive, int toInclusive)
        {
            return fromInclusive <= i && i <= toInclusive;
        }
    }
}
