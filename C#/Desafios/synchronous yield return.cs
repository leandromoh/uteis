using System;
using System.Collections;
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

            foreach (var i in g.Sequence2)
            {
                Console.WriteLine(i);
                if (i > 2) break;
            }

            foreach (var i in g.Sequence1)
            {
                Console.WriteLine(i);

                if (i == "carlos")
                {
                    ;
                }
            }


            Console.Read();
        }

        public static SequenceGroup<T2, T3> UnZip<T1, T2, T3>(this IEnumerable<T1> source, Func<T1, T2> selector1, Func<T1, T3> selector2)
        {
            var result = new SequenceGroup<T2, T3>();
            var col = source as ICollection<T1>;

            if (col != null)
            {
                var l2 = new List<T2>();
                var l3 = new List<T3>();

                var b = new Bla<T1, T2, T3>(col.ToList(), l2, l3, selector1, selector2);
                var c = new Bla<T1, T3, T2>(col.ToList(), l3, l2, selector2, selector1);
                result.Sequence1 = b;
                result.Sequence2 = c;
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

        public class Bla<T0, T1, T2> : IEnumerable<T1>
        {
            private List<T0> List0;
            public List<T1> List1;
            public List<T2> List2;

            Func<T0, T1> selector1;
            Func<T0, T2> selector2;

            public Bla(List<T0> l0, List<T1> l1, List<T2> l2, Func<T0, T1> f1, Func<T0, T2> f2)
            {
                List0 = l0;
                List1 = l1;
                List2 = l2;
                selector1 = f1;
                selector2 = f2;
            }

            public IEnumerator<T1> GetEnumerator()
            {
                int counter = 0;

                foreach (var item in List0)
                {
                    if (counter < List1.Count)
                    {
                        yield return List1[counter];
                        counter++;
                        continue;
                    }

                    List1.Add(selector1(item));
                    List2.Add(selector2(item));

                    counter++;
                    yield return selector1(item);
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
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

            return people;
        }
    }

    public class Person
    {
        public string name;
        public int age;
    }
}
