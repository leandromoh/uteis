#region License and Terms
// Copyright (c) 2016 Leandro F. Vieira (leandromoh). All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    static class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> source = Enumerable.Range(0, 10000);
            IEnumerable<int> sub = Enumerable.Range(6560, 20);


            for (int i = 0; i <= 6561; i++)
            {
                if (source.IndexOf(sub, i) != 6560)
                {
                    Console.WriteLine(i);
                }
            }


            //getTime(() =>  Console.WriteLine(source.isInfixOf(sub)));
            Console.WriteLine("fim");
            Console.Read();
        }

        public static void getTime(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }





        private static T GetByIndex<T>(int index, IList<T> list, IEnumerator<T> e, out bool ended)
        {
            T val = default(T);
            ended = false;

            if (!(index < list.Count))
            {
                if (e.MoveNext())
                {
                    val = e.Current;
                    list.Add(val);
                    return val;
                }
                ended = true;
            }
            else
            {
                val = list[index];
            }

            return val;
        }

        private static int? GetCountIfCheap<T>(this IEnumerable<T> source)
        {
            var col = source as ICollection<T>;
            if (col != null)
            {
                return col.Count;
            }

            //var read = source as IReadOnlyCollection<T>;
            //if (col != null)
            //{
            //    return read.Count;
            //}

            return null;
        }

        //takes two lists and returns True iff the second list is contained, wholly and intact, anywhere within the first.
        public static int IndexOf<T>(this IEnumerable<T> source, IEnumerable<T> target, int startIndex = 0, IEqualityComparer<T> comparer = null)
        {
            comparer = comparer ?? EqualityComparer<T>.Default;

            IList<T> y = (target as IList<T>) ?? target.ToArray();
            int? len1 = source.GetCountIfCheap();
            int len2 = y.Count;
            int? maxIndex = null;
            int i = 0;
            int j = 0;
            int k = 0;

            if (len1.HasValue)
            {
                if (startIndex >= len1 || len1 < len2)
                    return -1;
                else
                    maxIndex = len1.Value - len2 - startIndex;
            }

            int toSkip = startIndex;

            using (var e1 = source.GetEnumerator())
            {
                while (toSkip-- > 0)
                {
                    if (!e1.MoveNext())
                    {
                        return -1;
                    }
                }

                var list = new List<T>();
                var ended = false;
                T value;

                for (; maxIndex.HasValue ? i <= maxIndex : true; i++)
                {
                    for (k = i, j = 0; j < len2; j++, k++)
                    {
                        value = GetByIndex(k, list, e1, out ended);

                        if (ended)
                        {
                            len1 = k;
                            maxIndex = len1.Value - len2 - startIndex;
                            break;
                        }

                        if (!comparer.Equals(value, y[j]))
                        {
                            break;
                        }
                    }

                    if (j == len2)
                    {
                        return i + startIndex;
                    }
                }

                return -1;
            }
        }



        public static bool isInfixOf<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> subpart)
        {
            return source.Tails().Any(t => t.StartsWith(subpart));
        }

        public static bool StartsWith<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer = null)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");

            comparer = comparer ?? EqualityComparer<T>.Default;

            using (var firstIter = first.GetEnumerator())
            {
                return second.All(item => firstIter.MoveNext() && comparer.Equals(firstIter.Current, item));
            }
        }

        private static IEnumerable<IEnumerable<TSource>> Tails<TSource>(this IEnumerable<TSource> source)
        {
            int i = 0;

            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    checked
                    {
                        yield return source.Skip(i++);
                    }
                }
            }

            yield return Enumerable.Empty<TSource>();
        }
    }
}
