using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class ExpressionAggregate
    {
        private static MethodCallExpression f(Delegate func, Expression c1, Expression c2)
        {
            return Expression.Call(Expression.Constant(func.Target), func.Method, c1, c2);
        }

        public static TSource AggregateRightExpression<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (func == null) throw new ArgumentNullException("func");

            var stack = new Stack<Expression>();

            foreach (var i in source)
            {
                stack.Push(Expression.Constant(i));
            }

            Expression m = stack.Pop();

            while (stack.Count > 0)
            {
                m = f(func, stack.Pop(), m);
            }

            return Expression.Lambda<Func<TSource>>(m).Compile()();
        }
    }
}
