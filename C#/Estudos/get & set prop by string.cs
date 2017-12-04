using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace teste
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
    }

    static partial class Program
    {
        static void Main(string[] args)
        {
            var p = new Pessoa { Nome = "leandro", Idade = 22 };

            Console.WriteLine(p.Prop("Nome"));
            Console.WriteLine(p.Prop("Idade"));

            p.Prop("Idade", 34);

            Console.WriteLine(p.Idade);

            Console.Read();
        }

        public static U Prop<T, U>(this T obj, string prop, U value)
        {
            Dictionary<string, Func<object, object, object>> func;

            if (!setDicionario.TryGetValue(typeof(T), out func))
            {
                setDicionario[typeof(T)] = func = Set(obj);
            }

            return (U)func[prop](obj, value);
        }

        public static object Prop<T>(this T obj, string prop)
        {
            Dictionary<string, Func<object, object>> func;

            if (!getDicionario.TryGetValue(typeof(T), out func))
            {
                getDicionario[typeof(T)] = func = Get(obj);
            }

            return func[prop](obj);
        }

        private static Dictionary<Type, Dictionary<string, Func<object, object>>> getDicionario = new Dictionary<Type, Dictionary<string, Func<object, object>>>();
        private static Dictionary<Type, Dictionary<string, Func<object, object, object>>> setDicionario = new Dictionary<Type, Dictionary<string, Func<object, object, object>>>();

        private static Dictionary<string, Func<object, object, object>> Set<T>(T obj)
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var dic = props.ToDictionary(p => p.Name, p =>
            {
                var param1 = Expression.Parameter(typeof(object), "x");
                var e1 = Expression.Convert(param1, typeof(T));

                var param2 = Expression.Parameter(typeof(object), "y");
                var e2 = Expression.Convert(param2, p.PropertyType);

                var mem = Expression.PropertyOrField(e1, p.Name);

                var ass = Expression.Assign(mem, e2);

                var conv = Expression.Convert(ass, typeof(object));
                var exp = Expression.Lambda<Func<object, object, object>>(conv, param1, param2);

                return exp.Compile();
            });

            return dic;
        }

        private static Dictionary<string, Func<object, object>> Get<T>(T obj)
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var dic = props.ToDictionary(p => p.Name, p =>
            {
                var param = Expression.Parameter(typeof(object), "x");
                var e = Expression.Convert(param, typeof(T));

                var mem = Expression.PropertyOrField(e, p.Name);
                var conv = Expression.Convert(mem, typeof(object));
                var exp = Expression.Lambda<Func<object, object>>(conv, param);

                return exp.Compile();
            });

            return dic;
        }

        private static Dictionary<string, Func<T, object>> GetT<T>(T obj)
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var dic = props.ToDictionary(p => p.Name, p =>
            {
                var param = Expression.Parameter(typeof(T), "x");
                var mem = Expression.PropertyOrField(param, p.Name);
                var conv = Expression.Convert(mem, typeof(object));
                var exp = Expression.Lambda<Func<T, object>>(conv, param);

                return exp.Compile();
            });

            return dic;

        }
    }
}
