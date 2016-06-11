using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Concurrent;

// http://stackoverflow.com/questions/17441420/how-to-set-value-for-property-of-an-anonymous-object

//  optimized version

public static class AnonymousObjectMutator
{
    private const BindingFlags FieldFlags = BindingFlags.NonPublic | BindingFlags.Instance;
    private const BindingFlags PropFlags = BindingFlags.Public | BindingFlags.Instance;
    private static readonly string[] BackingFieldFormats = { "<{0}>i__Field", "<{0}>" };
    private static ConcurrentDictionary<Type, IDictionary<string, Action<object, object>>> _map =
        new ConcurrentDictionary<Type, IDictionary<string, Action<object, object>>>();

    public static T Set<T, TProperty>(
        this T instance, 
        Expression<Func<T, TProperty>> propExpression,
        TProperty newValue) where T : class
    {
        GetSetterFor(propExpression)(instance, newValue);
        return instance;
    }

    private static Action<object, object> GetSetterFor<T, TProperty>(Expression<Func<T, TProperty>> propExpression)
    {
        var memberExpression = propExpression.Body as MemberExpression;
        if (memberExpression == null || memberExpression.Member.MemberType != MemberTypes.Property)
            throw new InvalidOperationException("Only property expressions are supported");
        Action<object, object> setter = null;
        GetPropMap<T>().TryGetValue(memberExpression.Member.Name, out setter);
        if (setter == null)
            throw new InvalidOperationException("No setter found");
        return setter;
    }

    private static IDictionary<string, Action<object, object>> GetPropMap<T>()
    {
        return _map.GetOrAdd(typeof(T), x => BuildPropMap<T>());
    }

    private static IDictionary<string, Action<object, object>> BuildPropMap<T>()
    {
        var typeMap = new Dictionary<string, Action<object, object>>();
        var fields = typeof(T).GetFields(FieldFlags);
        foreach (var pi in typeof(T).GetProperties(PropFlags)) 
        {
            var backingFieldNames = BackingFieldFormats.Select(x => string.Format(x, pi.Name)).ToList();
            var fi = fields.FirstOrDefault(f => backingFieldNames.Contains(f.Name) && f.FieldType == pi.PropertyType);
            if (fi == null)
                throw new NotSupportedException(string.Format("No backing field found for property {0}.", pi.Name));
            typeMap.Add(pi.Name, (inst, val) => fi.SetValue(inst, val));
        }
        return typeMap;
    }
}

/*

//  not optimized version

public static class AnonymousObjectMutator
{
    private const BindingFlags FieldFlags = BindingFlags.NonPublic | BindingFlags.Instance;
    private static readonly string[] BackingFieldFormats = { "<{0}>i__Field", "<{0}>" };

    public static T Set<T, TProperty>(
        this T instance,
        Expression<Func<T, TProperty>> propExpression,
        TProperty newValue) where T : class
    {
        var pi = (propExpression.Body as MemberExpression).Member;
        var backingFieldNames = BackingFieldFormats.Select(x => string.Format(x, pi.Name)).ToList();
        var fi = typeof(T)
            .GetFields(FieldFlags)
            .FirstOrDefault(f => backingFieldNames.Contains(f.Name));
        if (fi == null)
            throw new NotSupportedException(string.Format("Cannot find backing field for {0}", pi.Name));
        fi.SetValue(instance, newValue);
        return instance;
    }
}

*/

public class Program
{
	public static void Main()
	{
        var myAnonInstance = new { 
            FirstField = "Hello", 
            AnotherField = 30, 
        };
		
        Console.WriteLine(myAnonInstance);

        myAnonInstance
            .Set(x => x.FirstField, "Hello SO")
            .Set(x => x.AnotherField, 42);
			
        Console.WriteLine(myAnonInstance);
	}
}
