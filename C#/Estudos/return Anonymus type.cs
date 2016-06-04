using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    static class Program
    {
        public static void Main()
        {
            var a = new { Name = default(string), Age = default(int) };

            var user = Cast(GetAnonymus(), a);

            Console.WriteLine("Name: {0}, Age: {1}", user.Name, user.Age);

            Console.Read();
        }

        static object GetAnonymus()
        {
            return new { Name = "Frank", Age = 15 };
        } 

        static T Cast<T>(object obj, T t)
        {
            return (T)obj;
        }
    }
}