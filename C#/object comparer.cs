using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class Program
{
	public static void Main()
	{
		var bond = new BondOrder
		{
			asd = new Y
			{
				MyProperty = 3,
			},
			Institution = "aava"
		};

		var cash = new Car
		{
			asd = new X
			{
				MyProperty = 3,
			},
			Institution = "aaa",
			Bla = "adddddddd"
		};

		Compare(cash, bond);
	}

	public  static void Compare(object source, object destination)
	{
		var flags = BindingFlags.Public | BindingFlags.Instance;
		var props = source.GetType().GetProperties(flags);
		var destinationType = destination.GetType(); 

		foreach (PropertyInfo prop1 in props.Where(p => p.CanRead))
		{
			var prop2 = destinationType.GetProperty(prop1.Name);

			if (prop2 == null)
				continue;
			
			object valueA = prop1.GetValue(source, null);
			object valueB = prop2.GetValue(destination, null);

			if (valueA is IComparable comp)
			{
				if (comp.CompareTo(valueB) != 0)
					throw new Exception();
			}
			else
				Compare(valueA, valueB);
		}
	}
}

public class BondOrder
{
    public string Institution {get; set;}
	public Y asd {get; set;}
}

public class Car 
{
    public string Institution {get; set;}
	public X asd {get; set;}
	public string Bla {get; set;}
}

public class X
{
	public int MyProperty { get; set; }
}

public class Y
{
	public int MyProperty { get; set; }
}
