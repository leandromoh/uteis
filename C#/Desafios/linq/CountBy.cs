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
using System.Linq;
using System.Runtime.CompilerServices;

namespace ConsoleApplication1
{
    public static class Ex
    {
        public static void Main()
        {
            Console.WriteLine(Tests.Test1());
            Console.WriteLine(Tests.Test2());
            Console.WriteLine(Tests.Test3());

            Console.Read();
        }



        public static IEnumerable<KeyValuePair<TKey, int>> CountBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.CountBy(keySelector, null);
        }

        public static IEnumerable<KeyValuePair<TKey, int>> CountBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector == null) throw new ArgumentNullException("keySelector");

            return CountByImpl(source, keySelector, comparer);
        }

        private static IEnumerable<KeyValuePair<TKey, int>> CountByImpl<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            var dic = new Dictionary<TKey, int>(comparer);

            foreach (var item in source)
            {
                TKey key = keySelector(item);

                if (dic.ContainsKey(key))
                {
                    dic[key]++;
                }
                else
                {
                    dic[key] = 1;
                }
            }

            return dic;
        }
    }
    
    
    class Tests
    {
        public static bool Test1()
        {
            var result = new[] { 1, 2, 3, 4, 5, 6, 1, 2, 3, 1, 1, 2 }.CountBy(c => c);

            var expecteds = new Dictionary<int, int>() { { 1, 4 }, { 2, 3 }, { 3, 2 }, { 4, 1 }, { 5, 1 }, { 6, 1 } };

            return result.SequenceEqual(expecteds);
        }

        public static bool Test2()
        {
            var result = Enumerable.Range(1, 100).CountBy(c => c % 2);

            var expecteds = new Dictionary<int, int>() { { 1, 50 }, { 0, 50 } };

            return result.SequenceEqual(expecteds);
        }

        public static bool Test3()
        {
            var result = new[] { 'a', 'b', 'c', 'A', 'b', 'a' }.CountBy(c => Char.ToUpper(c));

            var expecteds = new Dictionary<char, int>() { { 'A', 3 }, { 'B', 2 }, { 'C', 1 } };

            return result.SequenceEqual(expecteds);
        }
    }
}
