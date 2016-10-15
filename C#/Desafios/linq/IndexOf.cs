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
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "0123456789";
            var e = s.AsEnumerable();

            Console.WriteLine(s.IndexOf('0') == e.IndexOf('0'));
            Console.WriteLine(s.IndexOf('5') == e.IndexOf('5'));
            Console.WriteLine(s.IndexOf('-') == e.IndexOf('-'));

            Console.WriteLine(s.IndexOf('0', 0) == e.IndexOf('0', 0));
            Console.WriteLine(s.IndexOf('0', 1) == e.IndexOf('0', 1));
            Console.WriteLine(s.IndexOf('5', 5) == e.IndexOf('5', 5));
            Console.WriteLine(s.IndexOf('5', 6) == e.IndexOf('5', 6));

            Console.WriteLine(s.IndexOf('2', 0, 2) == e.IndexOf('2', 0, 2));
            Console.WriteLine(s.IndexOf('2', 0, 3) == e.IndexOf('2', 0, 3));
            Console.WriteLine(s.IndexOf('8', 0, 8) == e.IndexOf('8', 0, 8));
            Console.WriteLine(s.IndexOf('8', 1, 8) == e.IndexOf('8', 1, 8));
            Console.WriteLine(s.IndexOf('8', 0, 9) == e.IndexOf('8', 0, 9));

            Console.Read();
        }
    }

    static partial class LinqExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> source, T item)
        {
            return source.IndexOf(item, 0, null, null);
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T item, int startIndex)
        {
            return source.IndexOf(item, startIndex, null, null);
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T item, int startIndex, int count)
        {
            return source.IndexOf(item, startIndex, count, null);
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T item, IEqualityComparer<T> comparer)
        {
            return source.IndexOf(item, 0, null, comparer);
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T item, int startIndex, int? count, IEqualityComparer<T> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (startIndex < 0) throw new ArgumentOutOfRangeException("startIndex", "startIndex must not be negative");
            if (count.HasValue && count.Value < 0) throw new ArgumentOutOfRangeException("count", "count must not be negative");

            comparer = comparer ?? EqualityComparer<T>.Default;

            int _count = count.HasValue ? count.Value : int.MaxValue;

            using (var e = source.Skip(startIndex).GetEnumerator())
            {
                for (int i = 0; e.MoveNext() && i < _count; i = checked(i + 1))
                {
                    if (comparer.Equals(e.Current, item))
                    {
                        return i + startIndex;
                    }
                }
            }

            return -1;
        }

    }
}
