#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
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

namespace MoreLinq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    static partial class MoreEnumerable
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> source)
        {
            return TransposeImpl(source, x => !x.Any());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pad"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> source, T pad)
        {
            //shall pass pad to TransposeBla
            return TransposeImpl(source, x => x.All(y => y.Equals(pad)));
        }

        private static IEnumerable<IEnumerable<T>> TransposeImpl<T>(this IEnumerable<IEnumerable<T>> source, Func<IEnumerable<T>, bool> predicate)
        {
            foreach (var i in TransposeBla(source))
            {
                if (predicate(i))
                    break;

                yield return i;
            }
        }

        private static IEnumerable<IEnumerable<T>> TransposeBla<T>(IEnumerable<IEnumerable<T>> source)
        {
            var list = new List<IEnumerator<T>>();
            var values = new List<List<T>>();
            var empty = new T[] { }; // should have a list with pad if pad overload
            var count = 0;
            var e = source.Select((p, i) =>
            {
                if (i < list.Count)
                    return list[i];

                list.Add(p.GetEnumerator());

                return list[i];
            });


            while (true)
            {
                var inner_count = count;

                yield return e.SelectMany((p, j) =>
                {
                    if (j >= values.Count)
                    {
                        values.Add(new List<T>());
                    }

                    var row = values[j];

                    if (inner_count < row.Count)
                    {
                        return new[] { row[inner_count] };
                    }

                    while (inner_count >= row.Count && p.MoveNext())
                    {
                        row.Add(p.Current);
                    }

                    return inner_count < row.Count ? new[] { row[inner_count] }
                                                   : empty;
                });

                count++;
            }
        }
    }
}
