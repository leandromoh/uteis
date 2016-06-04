using System;

namespace ConsoleApplication1
{
    //You cannot overload operators via Extension methods.
    //Best you can do is create an extension method that convert the type. For example:
    //public static bool ToBoolean(this int i) { return i > 0; }
    
    public class Person
    {
        public string Name { get; set; }

        //can be cast implicitly or explicitly
        public static implicit operator string(Person p)
        {
            return p.Name;
        }

        //can be cast only explicitly
        public static explicit operator bool(Person p)
        {
            return !string.IsNullOrWhiteSpace(p);
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            var p = new Person() { Name = "Leandro" };

            string s = p; //ok, Person can be convert to string implicitly
            //bool b = p; //error: Cannot implicitly convert type 'Person' to 'bool'. An explicit conversion exists (are you missing a cast?)

            Console.WriteLine((string)p); //ok, implicit conversion can also be made explicitly
            Console.WriteLine((bool)p); //ok, explict conversion being made explicitly

            Console.Read();
        }
    }
}
