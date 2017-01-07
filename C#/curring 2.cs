using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    static class X
    {
        static void Main(string[] args)
        {
            //for (int i = 2; i < 6; i++)
            //    Console.WriteLine(CreateFlipCurry(i) + "\n");

            //for (int i = 2; i < 17; i++)
            //    Console.WriteLine(CreateFlip(i) + "\n");

            //for (int i = 2; i < 17; i++)
            //    Console.WriteLine(CreatePartial(i) + "\n");

            //for (int i = 2; i < 17; i++)
            //    Console.WriteLine(CreateCurry(i) + "\n");

            //for (int i = 0; i < 3; i++)
            //    Console.WriteLine(CreateUnCurry(i) + "\n");


            Console.Read();
        }

        // obs: the arg = 2 version works for any quantity curried function!!!
        private static string CreateFlipCurry(int args)
        {
            if (args < 2) throw new ArgumentException("args", "args must not be lesser than 2");

            args++;
            var n = Enumerable.Range(1, args).ToArray();
            var nFlip = n.Take(2).Reverse().Concat(n.Skip(2)).ToArray();

            var letters = Enumerable.Range(1, args - 1).Select(x => "arg" + x); // args - 1 because last arg is the TResult
            var lettersFlip = letters.Take(2).Reverse().Concat(letters.Skip(2));

            var ts = n.Select(x => "T" + x);
            var tsFlip = nFlip.Select(x => "T" + x);

            string TsComma = string.Join(", ", ts);
            string TsCommaFlip = string.Join(", ", tsFlip);

            var templete = new StringBuilder();
            templete.Append("public static ");
            templete.Append(tsFlip.AggregateRight((t1, t2) => "Func<" + t1 + ", " + t2 + ">") + " ");
            templete.Append("Flip<" + TsComma + ">");
            templete.Append("(this " + ts.AggregateRight((t1, t2) => "Func<" + t1 + ", " + t2 + ">") + " function)");
            templete.Append("\n{");
            templete.Append("\n\treturn arg2 => arg1 => function(arg1)(arg2);");
            templete.Append("\n}");

            return templete.Replace("T" + args, "TResult").Replace("return  =>", "return ").ToString();
        }

        private static string CreateFlip(int args)
        {
            if (args < 2) throw new ArgumentException("args", "args must not be lesser than 2");

            args++;
            var n = Enumerable.Range(1, args).ToArray();
            var nFlip = n.Take(2).Reverse().Concat(n.Skip(2)).ToArray();

            var letters = Enumerable.Range(1, args - 1).Select(x => "arg" + x); // args - 1 because last arg is the TResult
            var lettersFlip = letters.Take(2).Reverse().Concat(letters.Skip(2));

            var ts = n.Select(x => "T" + x);
            var tsFlip = nFlip.Select(x => "T" + x);

            string TsComma = string.Join(", ", ts);
            string TsCommaFlip = string.Join(", ", tsFlip);

            var templete = new StringBuilder();
            templete.Append("public static ");
            templete.Append("Func<" + TsCommaFlip + "> ");
            templete.Append("Flip<" + TsComma + ">");
            templete.Append("(this Func<" + TsComma + "> function)");
            templete.Append("\n{");
            templete.Append("\n\treturn (" + string.Join(", ", lettersFlip) + ") => function(" + string.Join(", ", letters) + ");");
            templete.Append("\n}");

            return templete.Replace("T" + args, "TResult").Replace("return  =>", "return ").ToString();
        }

        private static string CreatePartial(int args)
        {
            if (args < 1) throw new ArgumentException("args", "args must be greater than 0");

            List<string> definitions = new List<string>();

            args++;

            for (int i = 1; i < args; i++)
            {
                var n = Enumerable.Range(1, args).ToArray();
                var ts = n.Select(x => "T" + x);
                string TsComma = string.Join(", ", ts);
                var letters = Enumerable.Range(1, args - 1).Select(x => "arg" + x);
                var partial = Enumerable.Range(1, args).Select(x => "T" + x + " arg" + x).Take(i);

                var templete = new StringBuilder();
                templete.Append("public static ");
                templete.Append(ts.Skip(i).AggregateRight((t1, t2) => "Func<" + t1 + ", " + t2 + ">") + " ");
                templete.Append("Partial<" + TsComma + ">");
                templete.Append("(this Func<" + TsComma + "> function," + string.Join(", ", partial) + ")");
                templete.Append("\n{");
                templete.Append("\n\treturn " + string.Join(" => ", letters.Skip(i)) + " => function(" + string.Join(", ", letters) + ");");
                templete.Append("\n}");

                definitions.Add(templete.Replace("T" + args, "TResult").Replace("return  =>", "return ").ToString());
            }

            return string.Join("\n\n", definitions);
        }

        private static string CreateCurry(int args)
        {
            if (args < 0) throw new ArgumentException("args", "args must not be negative");

            args++;
            var n = Enumerable.Range(1, args).ToArray();
            var ts = n.Select(x => "T" + x);
            string TsComma = string.Join(", ", ts);
            var letters = Enumerable.Range(1, args - 1).Select(x => "arg" + x);

            var templete = new StringBuilder();
            templete.Append("public static ");
            templete.Append(ts.AggregateRight((t1, t2) => "Func<" + t1 + ", " + t2 + ">") + " ");
            templete.Append("Curry<" + TsComma + ">");
            templete.Append("(this Func<" + TsComma + "> function)");
            templete.Append("\n{");
            templete.Append("\n\treturn " + string.Join(" => ", letters) + " => function(" + string.Join(", ", letters) + ");");
            templete.Append("\n}");

            return templete.Replace("T" + args, "TResult").Replace("return  =>", "return ").ToString();
        }

        private static string CreateUnCurry(int args)
        {
            if (args < 0) throw new ArgumentException("args", "args must not be negative");

            args++;
            var n = Enumerable.Range(1, args).ToArray();
            var ts = n.Select(x => "T" + x);
            string TsComma = string.Join(", ", ts);
            var letters = Enumerable.Range(1, args - 1).Select(x => "arg" + x);

            var templete = new StringBuilder();
            templete.Append("public static ");
            templete.Append("Func<" + TsComma + "> ");
            templete.Append("UnCurry<" + TsComma + ">");
            templete.Append("(this " + ts.AggregateRight((t1, t2) => "Func<" + t1 + ", " + t2 + ">") + " function)");
            templete.Append("\n{");
            templete.Append("\n\treturn (" + string.Join(", ", letters) + ") => function(" + string.Join(")(", letters) + ");");
            templete.Append("\n}");

            return templete.Replace("T" + args, "TResult").Replace("return () =>", "return ").ToString();
        }




        public static TSource AggregateRight<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (func == null) throw new ArgumentNullException("func");
            if (!source.Any()) throw new InvalidOperationException("Sequence contains no elements");

            IList<TSource> e = (source as IList<TSource>) ?? source.ToArray();

            return AggregateRightImp(e, e.Last(), func, e.Count - 1);
        }

        private static TResult AggregateRightImp<TSource, TResult>(IList<TSource> e, TResult current, Func<TSource, TResult, TResult> func, int i)
        {
            while (i-- > 0)
            {
                current = func(e[i], current);
            }

            return current;
        }
    }
}
