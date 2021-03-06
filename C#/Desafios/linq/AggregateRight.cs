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
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    public static class Program
    {
        public static void Main()
        {
            var result = new string[] { "1", "2", "3", "4" }.AggregateRight((a, b) => string.Format("({0}/{1})", a, b));
            Console.WriteLine(result == "(1/(2/(3/4)))");

            result = new int[] { 1, 2, 3 }.AggregateRight("4", (a, b) => string.Format("({0}/{1})", a, b));
            Console.WriteLine(result == "(1/(2/(3/4)))");

            result = new int[] { }.AggregateRight("4", (a, b) => string.Format("({0}/{1})", a, b));
            Console.WriteLine(result == "4");

            int length = new int[] { 1, 2, 3 }.AggregateRight("4", (a, b) => string.Format("({0}/{1})", a, b), str => str.Length);
            Console.WriteLine(length == "(1/(2/(3/4)))".Length);

            Console.Read();
        }

        public static TSource AggregateRight<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (func == null) throw new ArgumentNullException("func");
            if (!source.Any()) throw new InvalidOperationException("Sequence contains no elements");

            IList<TSource> e = (source as IList<TSource>) ?? source.ToArray();

            return AggregateRightImp(e, e.Last(), func, e.Count - 1);
        }

        public static TAccumulate AggregateRight<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TSource, TAccumulate, TAccumulate> func)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (func == null) throw new ArgumentNullException("func");

            IList<TSource> e = (source as IList<TSource>) ?? source.ToArray();

            return AggregateRightImp(e, seed, func, e.Count);
        }

        public static TResult AggregateRight<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TSource, TAccumulate, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (func == null) throw new ArgumentNullException("func");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");

            return resultSelector(source.AggregateRight(seed, func));
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
