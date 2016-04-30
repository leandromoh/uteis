using System;
using System.Reflection;

namespace ConsoleApplication1
{
    public class Program
    {
        public static int a = 1;
        public static int b = 2;
        public static int c = 3;

        public static void Main(string[] args)
        {
            FieldInfo x = typeof(Program).GetField(Console.ReadLine(), BindingFlags.Public | BindingFlags.Static);

            if (x == null)
                Console.WriteLine("atributo não existe");
            else
                x.SetValue(null, Int32.Parse(Console.ReadLine()));

            Console.WriteLine("total = " + (a + b + c));

            Console.Read();
        }
    }
}
