using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Optional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public static class Program
    {
        static void Main(string[] args)
        {
            object list = new List<int>() { 1, 2, 3 };

            if (list.IsUnboundTypeOf(typeof(List<>)))
            {
                var l2 = list.ParseAs<List<object>>();

                Console.WriteLine(l2.Count);
            }

            Console.Read();
        }

        public static T ParseAs<T>(this object obj)
        {
            var fromType = obj.GetType();
            var toType = typeof(T);

            if (!(fromType.IsGenericType && toType.IsGenericType &&
                  fromType.GetGenericTypeDefinition() == toType.GetGenericTypeDefinition()))
                throw new Exception();

            var fromTypeArguments = fromType.GetGenericArguments();
            var toTypeArguments = toType.GetGenericArguments();

            if (fromTypeArguments.Length != toTypeArguments.Length)
                throw new Exception();

            for(var i = 0; i < fromTypeArguments.Length; i++)
            {
                if (!fromTypeArguments[i].IsSubclassOf(toTypeArguments[i]))
                    throw new Exception();
            }

           var x = Convert.ChangeType(obj, typeof(T));

            return (T)x;
        }

        public static bool IsUnboundTypeOf(this object obj, Type unboundType)
        {
            return IsUnboundTypeOf(obj.GetType(), unboundType);
        }

        public static bool IsUnboundTypeOf(this Type toCheck, Type unboundType)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType 
                          ? toCheck.GetGenericTypeDefinition() 
                          : toCheck;

                if (unboundType == cur)
                {
                    return true;
                }

                toCheck = toCheck.BaseType;
            }

            return false;
        }
    }
}
