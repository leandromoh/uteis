using System;
using System.Linq.Expressions;

namespace ConsoleApplication1
{
    class Bla
    {
         class Foo
        {
            public Bar myBar { get; set; }
            public int age { get; set; }
        }
        class Bar
        {
            public string name { get; set; }
        }

        static void Main()
        {
            var expression1 = CreateExpression<Foo>("age");
            var expression2 = CreateExpression<Foo>("myBar.name");

            // x => x.myBar.name

            var f = new Foo() 
            { 
                age = 13,
                myBar = new Bar() 
                { 
                    name = "leandro"
                }
            };

            var f1 = expression1.Compile();
            var f2 = expression2.Compile();

            var r1 = f1.DynamicInvoke(f);
            var r2 = f2.DynamicInvoke(f);

            Console.WriteLine(r1);
            Console.WriteLine(r2);

            Console.Read();
        }

        static LambdaExpression CreateExpression<T>(string propertyName)
        {
            var param = Expression.Parameter(typeof(T), "x");

            var exp = propertyName.Split('.').Aggregate((Expression)param, (body, member) => Expression.PropertyOrField(body, member));

            return Expression.Lambda(exp, param);
        }
    }
}
