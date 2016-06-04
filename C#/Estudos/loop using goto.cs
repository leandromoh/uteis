using System;

namespace ConsoleApplication1
{
    class Program
    {
        public static void Main()
        {
            int i = 0, max = 10;

        loop:
            if (i > max) goto endLoop;
            Console.Write(i + " ");
            i++;
            goto loop;
        endLoop:

            Console.Read();
        }
    }
}
