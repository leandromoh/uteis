using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    static class Program
    {
        public static void Main()
        {
            //return null if T is a reference type, 
            //0 if T is an int, false if T is a boolean, etc.

            Console.WriteLine(default(int));
            Console.WriteLine(default(bool));
            Console.WriteLine(default(string) ?? "null");

            Console.Read();
        }
    }
}