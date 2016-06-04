using System;

namespace ConsoleApplication1
{
    public class Tuple<T1, T2>
    {
        public Tuple(T1 v1, T2 v2) { Item1 = v1; Item2 = v2; }
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
    }

    public static class Tuple
    {
        public static Tuple<T1, T2> Create<T1, T2>(T1 v1, T2 v2)
        {
            return new Tuple<T1, T2>(v1, v2);
        }
    }

    class Program
    {
        static void Main()
        {
            Tuple<int, string> t;
            
            t = new Tuple<int, string>(1, "um");
            Console.WriteLine(t.Item1);

            t = Tuple.Create(2, "dois");
            Console.WriteLine(t.Item2);

            Console.Read();
        }
    }
}
