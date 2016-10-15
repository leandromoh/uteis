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

using System.Collections.Generic;
using System;

namespace EffectiveLINQ
{
    static partial class LinqExtensions
    {
        public static T NthOrDefault<T>(this IEnumerable<T> source, int nth)
        {
            return NthIterator(source, nth, true);
        }

        public static T NthOrDefault<T>(this IEnumerable<T> source, int nth, Func<T, bool> predicate)
        {
            return NthIterator(source, nth, true, predicate);
        }

        public static T Nth<T>(this IEnumerable<T> source, int nth)
        {
            return NthIterator(source, nth, false);
        }

        public static T Nth<T>(this IEnumerable<T> source, int nth, Func<T, bool> predicate)
        {
            return NthIterator(source, nth, false, predicate);
        }

        private static T NthIterator<T>(IEnumerable<T> source, int nth, bool useDefault)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (nth <= 0) throw new ArgumentOutOfRangeException("nth", "nth must be greater than zero.");

            int found = 0;

            foreach (var item in source)
            {
                if (++found == nth)
                {
                    return item;
                }
            }

            if (!useDefault)
                throw new InvalidOperationException("Sequence does not contains " + nth + " elements");

            return default(T);
        }

        private static T NthIterator<T>(IEnumerable<T> source, int nth, bool useDefault, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");
            if (nth <= 0) throw new ArgumentOutOfRangeException("nth", "nth must be greater than zero.");

            int found = 0;

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    if (++found == nth)
                    {
                        return item;
                    }
                }
            }

            if (!useDefault)
                throw new InvalidOperationException("Sequence does not contains " + nth + " elements that matches the predicate");

            return default(T);
        }
    }
}
