using System;

namespace ConsoleApplication1
{
    public class Outer
    {
        private int Value { get; set; }

        public class Inner
        {
            protected void ModifyOuterMember(Outer outer, int value)
            {
                outer.Value = value;
            }
        }
    }

    public class Cheater : Outer.Inner
    {
        public void MakeValue5(Outer outer)
        {
            ModifyOuterMember(outer, 5);
        }
    }

    static class Program
    {
        public static void Main()
        {
            var o = new Outer();
            var c = new Cheater();

            c.MakeValue5(o);

            Console.Read();
        }
    }
}