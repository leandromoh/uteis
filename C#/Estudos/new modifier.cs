using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    /*
        You cannot override a non-virtual or static method. 

        The overridden base method must be virtual, abstract, or override.

        So the 'new' keyword is needed to allow you to 'override' non-virtual and static methods (e.g., third-party code).

        The new modifier instructs the compiler to use your implementation instead of the base class implementation. Any code that is not referencing your class but the base class will use the base class implementation.

        The override, virtual, and new keywords can also be applied to properties, indexers, and events.
    */

    public class Person
    {
        public virtual void Speak()
        {
            Console.WriteLine("Living");
        }
    }

    public class Student : Person
    {
        public new void Speak()
        {
            Console.WriteLine("Studying");
        }
    }

    public class Worker : Person
    {
        public override void Speak()
        {
            Console.WriteLine("Working");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person student1 = new Student();
            Person worker = new Worker();

            Student student2 = new Student();

            student1.Speak(); //Prints "Living" when you might expect "Studying"
            worker.Speak(); //Prints "Working"

            student2.Speak(); //Prints "Studying"

            Console.Read();
        }
    }
}