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
            source = source.Select(Memoize).Memoize();

            var e = source.Select(p => p.GetEnumerator()).Where(HasNext);

            while (e.Any())
            {
                yield return e.Select(p => p.Current);

                e = e.Where(HasNext);
            }
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
            source = source.Select(Memoize).Memoize();

            var padList = new[] { new { value = pad, native = false } }.Repeat();

            var e = source.Select(p => p.Select(y => new { value = y, native = true }).Concat(padList).GetEnumerator());

            while (true)
            {
                e = e.Where(x => x.MoveNext());

                if (e.All(x => x.Current.native == false))
                    yield break;

                yield return e.Select(p => p.Current.value);
            }
        }

        private static bool HasNext<T>(IEnumerator<T> e)
        {
            var hasNext = e.MoveNext();

            if (!hasNext) e.Dispose();

            return hasNext;
        }


        private static IEnumerable<IEnumerable<T>> Transpose<T>(ICollection<ICollection<T>> source)
        {
            return TransposeImpl(source, x => x.Where(y => y.MoveNext()).Select(y => y.Current));
        }

        private static IEnumerable<IEnumerable<T>> Transpose<T>(ICollection<ICollection<T>> source, T pad)
        {
            return TransposeImpl(source, x => x.Select(y => y.MoveNext() ? y.Current : pad));
        }

        private static IEnumerable<IEnumerable<T>> TransposeImpl<T>(ICollection<ICollection<T>> source, Func<IEnumerable<IEnumerator<T>>, IEnumerable<T>> func)
        {
            var maxCount = source.MaxBy(x => x.Count).Count;

            var enumerators = source.Select(p => p.GetEnumerator()).ToList();

            for (int i = 0; i < maxCount; i++)
            {
                yield return func(enumerators).ToList();
            }
        }
    }
}
