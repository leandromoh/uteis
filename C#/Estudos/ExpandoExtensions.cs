using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace ConsoleApplication1
{
    //font: http://stackoverflow.com/questions/18465630/understanding-conditionalweaktable

    public static class Ex
    {
        public static void Main()
        {
            var p = new Pessoa();
            p.Props().Y = "hello";

            Console.WriteLine(p.Props().Y);

            Console.Read();
        }
    }

    public class Pessoa
    {
        public string Nome;
    }

    public static class ExpandoExtensions
    {
        private static IDictionary<Pessoa, ExpandoObject> props = new Dictionary<Pessoa, ExpandoObject>();

        public static dynamic Props(this Pessoa key)
        {
            ExpandoObject o;
            if (!props.TryGetValue(key, out o))
            {
                o = new ExpandoObject();
                props[key] = o;
            }
            return o;
        }



        private static readonly ConditionalWeakTable<Pessoa, ExpandoObject> props2 =
            new ConditionalWeakTable<Pessoa, ExpandoObject>();

        public static dynamic Props2(this Pessoa key)
        {
            return props2.GetOrCreateValue(key);
        }
    }
}
