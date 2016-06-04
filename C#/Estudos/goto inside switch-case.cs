using System;

namespace ConsoleApplication1
{
    class Program
    {
        public static void Main()
        {
            int i = 1;

            switch (i)
            {
                case 0:
                case 1:
                    Console.Write("B ");
                    goto case 3;
                case 2:
                    Console.Write("2 ");
                    goto default;
                case 3:
                    Console.Write("3 ");
                    goto case 2;
                default:
                    Console.Write("D ");
                    break;
            }

            //Output: B 3 2 D
            Console.Read();
        }
    }
}
