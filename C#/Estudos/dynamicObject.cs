using System;
using System.Collections.Generic;
using System.Text;
using System.Dynamic;

namespace ConsoleApplication1
{
    class MyDynamic : DynamicObject
    {
        Dictionary<string, object> properties = new Dictionary<string, object>();

        public int Count
        {
            get { return properties.Count; }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            properties.TryGetValue(binder.Name, out result);
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            properties[binder.Name] = value;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (properties.ContainsKey(binder.Name) && properties[binder.Name] is Delegate)
            {
                result = ((Delegate)properties[binder.Name]).DynamicInvoke(args);
                return true;
            }

            string property = (string)args[0];
            object value = args.Length == 2 ? args[1] : null;
            result = value;

            if (binder.Name.ToLower() == "addproperty")
            {
                if (args[0].GetType() == typeof(string))
                {
                    properties.Add(property, value);
                    return true;
                }
            }
            if (binder.Name.ToLower() == "removeproperty")
            {
                if (args[0].GetType() == typeof(string))
                {
                    properties.TryGetValue(property, out result);
                    properties.Remove(property);
                    return true;
                }
            }

            return false;
        }
    }

    static class Program
    {
        public static void Main()
        {
            dynamic myObject = new MyDynamic();

            myObject.FirstName = "John";
            Console.WriteLine(myObject.FirstName);

            myObject.AddProperty("Salary");
            Console.WriteLine(myObject.Salary ?? "undefined");
            myObject.Salary = 35000m;
            Console.WriteLine(myObject.Salary);

            myObject.AddProperty("DateOfBirth", new DateTime(2015, 12, 31));
            Console.WriteLine(myObject.DateOfBirth);

            Console.WriteLine("number of properties: " + myObject.Count);

            myObject.RemoveProperty("FirstName");
            Console.WriteLine(myObject.FirstName ?? "undefined");

            Console.WriteLine("number of properties: " + myObject.Count);

            myObject.sum = new Func<int, int, int>((a, b) => a + b);
            Console.WriteLine("Sum = " + myObject.sum(3, 5));

            Console.Read();
        }
    }
}
