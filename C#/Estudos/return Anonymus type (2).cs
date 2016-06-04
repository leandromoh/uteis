using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    static class Program
    {
        public static void Main()
        {
            var CastToAnonymus = CastToType(new { Name = default(string), Age = default(int) });

            var user = CastToAnonymus(GetAnonymus());

            Console.WriteLine("Name: {0}, Age: {1}", user.Name, user.Age);

            Console.Read();
        }

        static object GetAnonymus()
        {
            return new { Name = "Frank", Age = 15 };
        }

        static Func<object,T> CastToType<T>(T t)
        {
            return o => (T)o;
        }
    }
}