using System;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        public static void greetings(string name = "Bob", string day = "Sunday")
        {
            Console.WriteLine("Hello " + name + "! Today is " + day);
        }

        public static void Main()
        {
            greetings(); //Hello Bob! Today is Sunday
            greetings("Leandro", "Friday"); //Hello Leandro! Today is Friday
            greetings(day: "Friday"); //Hello Bob! Today is Friday

            Console.Read();
        }
    }
}
