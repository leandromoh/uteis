using System;
using System.Text;

namespace ConsoleApplication1
{
    class Person
    {
        public int Age { get; set; }

        public Person() : this(18) { }

        public Person(int age)
        {
            this.Age = age;
        }

        public static void Main()
        {
            var p1 = new Person();
            var p2 = new Person(3);

            Console.WriteLine(p1.Age); //18
            Console.WriteLine(p2.Age); //3

            Console.Read();
        }
    }
}