using System;

namespace ConsoleApplication1
{
    public class MyClass : InterfaceOne, InterfaceTwo, InterfaceThree
    {
        //you CANT add Public access modifier to explicitly implemented interface
        void InterfaceOne.InterfaceMethod() //explicit implementation
        {
            Console.WriteLine("Only accessible if cast obj to InterfaceOne");
        }

        void InterfaceTwo.InterfaceMethod() //explicit implementation
        {
            Console.WriteLine("Only accessible if cast obj to InterfaceTwo");
        }

        public void InterfaceMethod() //implicit implementation
        {
            Console.WriteLine("Default implementation! Accessible as a normal class member.");
        }

        public static void Main()
        {
            MyClass obj = new MyClass();

            obj.InterfaceMethod();
            ((InterfaceOne)obj).InterfaceMethod();
            ((InterfaceTwo)obj).InterfaceMethod();
            
            Console.Read();
        }
    }

    interface InterfaceOne
    {
        void InterfaceMethod();
    }

    interface InterfaceTwo
    {
        void InterfaceMethod();
    }

    interface InterfaceThree
    {
        void InterfaceMethod();
    }
}
