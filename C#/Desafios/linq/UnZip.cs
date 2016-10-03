using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public static class Ex
    {
        public static void Main()
        {
            var people = GetPeople();

            var g = people.UnZip(a => a.name, a => a.age);

            foreach (var i in g.Sequence1)
                Console.WriteLine(i);

            foreach (var i in g.Sequence2)
                Console.WriteLine(i);

            Console.Read();
        }

        public static SequenceGroup<T2, T3> UnZip<T1, T2, T3>(this IEnumerable<T1> source, Func<T1, T2> selector1, Func<T1, T3> selector2)
        {
            var result = new SequenceGroup<T2, T3>();
            var col = source as ICollection<T1>;

            if (col != null)
            {
                var seq1 = new T2[col.Count];
                var seq2 = new T3[col.Count];

                int i = 0;

                foreach (T1 item in source)
                {
                    seq1[i] = selector1(item);
                    seq2[i] = selector2(item);

                    i++;
                }

                result.Sequence1 = seq1;
                result.Sequence2 = seq2;
            }
            else
            {
                result.Sequence1 = source.Select(x => selector1(x));
                result.Sequence2 = source.Select(x => selector2(x));
            }

            return result;
        }

        public struct SequenceGroup<T1, T2>
        {
            public IEnumerable<T1> Sequence1;
            public IEnumerable<T2> Sequence2;
        }

        public static IEnumerable<Person> GetPeople()
        {
            var people = new[]
            {
                new Person { name = "ana", age = 1},
                new Person { name = "bob", age = 2},
                new Person { name = "carlos", age = 3},
                new Person { name = "david", age = 4},
                new Person { name = "eduardo", age = 5},
            };

            foreach (var p in people)
            {
                yield return p;
            }
        }
    }

    public class Person
    {
        public string name;
        public int age;
    }
}
