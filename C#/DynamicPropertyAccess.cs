using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace teste
{
    public static class DynamicPropertyAccess
    {
        public static U SetValue<T, U>(this T obj, string prop, U value)
        {
            Dictionary<string, Func<object, object, object>> dic = SetProperty(typeof(T));

            if (!dic.ContainsKey(prop))
            {
                dic = SetField(typeof(T));
            }

            return (U)dic[prop](obj, value);
        }

        public static object GetValue<T>(this T obj, string prop)
        {
            Dictionary<string, Func<object, object>> dic = GetProperty(typeof(T));

            if (!dic.ContainsKey(prop))
            {
                dic = GetField(typeof(T));
            }

            return dic[prop](obj);
        }

        private static Dictionary<string, Func<object, object>> Get<T>(Type type, Func<Type, IEnumerable<T>> getList, Func<T, string> getName)
        {
            var props = getList(type);

            var dic = props.ToDictionary(getName, p =>
            {
                var param = Expression.Parameter(typeof(object), "x");
                var e = Expression.Convert(param, type);

                var mem = Expression.PropertyOrField(e, getName(p));
                var conv = Expression.Convert(mem, typeof(object));
                var exp = Expression.Lambda<Func<object, object>>(conv, param);

                return exp.Compile();
            });

            return dic;
        }

        private static Dictionary<string, Func<object, object, object>> Set<T>(Type type, Func<Type, IEnumerable<T>> getList, Func<T, string> getName, Func<T, Type> getType)
        {
            var props = getList(type);

            var dic = props.ToDictionary(getName, p =>
            {
                var param1 = Expression.Parameter(typeof(object), "x");
                var e1 = Expression.Convert(param1, type);

                var param2 = Expression.Parameter(typeof(object), "y");
                var e2 = Expression.Convert(param2, getType(p));

                var mem = Expression.PropertyOrField(e1, getName(p));

                var ass = Expression.Assign(mem, e2);

                var conv = Expression.Convert(ass, typeof(object));
                var exp = Expression.Lambda<Func<object, object, object>>(conv, param1, param2);

                return exp.Compile();
            });

            return dic;
        }

        private static BindingFlags flag = BindingFlags.Public | BindingFlags.Instance;

        private static readonly Func<Type, Dictionary<string, Func<object, object, object>>> SetProperty = 
            Cache((Type t) => Set(t, type => type.GetProperties(flag), p => p.Name, p => p.PropertyType));

        private static readonly Func<Type, Dictionary<string, Func<object, object, object>>> SetField = 
            Cache((Type t) => Set(t, type => type.GetFields(flag), p => p.Name, p => p.FieldType));

        private static readonly Func<Type, Dictionary<string, Func<object, object>>> GetProperty = 
            Cache((Type t) => Get(t, type => type.GetProperties(flag), p => p.Name));

        private static readonly Func<Type, Dictionary<string, Func<object, object>>> GetField = 
            Cache((Type t) => Get(t, type => type.GetFields(flag), p => p.Name));

        private static Func<TSource, TResult> Cache<TSource, TResult>(Func<TSource, TResult> func)
        {
            var cache = new Dictionary<TSource, TResult>();

            return (TSource arg) =>
            {
                TResult value;

                if (!cache.TryGetValue(arg, out value))
                {
                    cache[arg] = value = func(arg);
                }

                return value;
            };
        }

        private static Dictionary<string, Func<T, object>> GetT<T>()
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
