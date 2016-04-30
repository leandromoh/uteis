using System;
using System.Text;

namespace ConsoleApplication1
{
    interface IPeople
    {
        Person this[int index] { get; set; }
    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class People : IPeople
    {
        private Person[] _people = new Person[10];

        public Person this[int index]
        {
            get
            {
                return _people[index];
            }
            set
            {
                _people[index] = value;
            }
        }

        public Person this[string name]
        {
            get
            {
                for (int i = 0; i < _people.Length; i++)
                {
                    if (_people[i].Name == name)
                    {
                        return _people[i];
                    }
                }
                return new Person();
            }
        }
    }


    class Program
    {
        public static void Main(string[] args)
        {
            People people = new People();
            people[0] = new Person() { Name = "Bob", Age = 16 };
            people[1] = new Person() { Name = "Jack", Age = 15 };

            Console.WriteLine(people[0].Name); //Bob
            Console.WriteLine(people["Jack"].Age); //15

            Console.Read();
        }
    }
}
