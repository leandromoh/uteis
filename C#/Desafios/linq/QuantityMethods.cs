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
using System.Linq;
using System;

namespace EffectiveLINQ
{
    static partial class LinqExtensions
    {
        /// <summary>
        /// Returns true when the number of elements in the given sequence is greater than
        /// or equal to the given integer.
        /// This method throws an exception if the given integer is negative.
        /// </summary>
        /// <typeparam name="T">Element type of sequence</typeparam>
        /// <param name="source">The source sequence</param>
        /// <param name="min">The minimum number of items a sequence must have for this
        /// function to return true</param>
        /// <exception cref="ArgumentNullException">source is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">min is negative</exception>
        /// <returns><c>true</c> if the number of elements in the sequence is greater than
        /// or equal to the given integer or <c>false</c> otherwise.</returns>
        /// <example>
        /// <code>
        /// var numbers = { 123, 456, 789 };
        /// var result = numbers.AtLeast(2);
        /// </code>
        /// The <c>result</c> variable will contain <c>true</c>.
        /// </example>
        public static bool AtLeast<T>(this IEnumerable<T> source, int min)
        {
            if (min < 0) throw new ArgumentOutOfRangeException("min", "min must not be negative.");

            return QuantityIterator(source, min, count => count >= min);
        }

        /// <summary>
        /// Returns true when the number of elements in the given sequence is lesser than
        /// or equal to the given integer.
        /// This method throws an exception if the given integer is negative.
        /// </summary>
        /// <typeparam name="T">Element type of sequence</typeparam>
        /// <param name="source">The source sequence</param>
        /// <param name="max">The maximun number of items a sequence must have for this
        /// function to return true</param>
        /// <exception cref="ArgumentNullException">source is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">max is negative</exception>
        /// <returns><c>true</c> if the number of elements in the sequence is lesser than
        /// or equal to the given integer or <c>false</c> otherwise.</returns>
        /// <example>
        /// <code>
        /// var numbers = { 123, 456, 789 };
        /// var result = numbers.AtMost(2);
        /// </code>
        /// The <c>result</c> variable will contain <c>false</c>.
        /// </example>
        public static bool AtMost<T>(this IEnumerable<T> source, int max)
        {
            if (max < 0) throw new ArgumentOutOfRangeException("max", "max must not be negative.");

            return QuantityIterator(source, max + 1, count => count <= max);
        }

        /// <summary>
        /// Returns true when the number of elements in the given sequence is greater than
        /// or equal to the given integer.
        /// This method throws an exception if the given integer is negative.
        /// </summary>
        /// <typeparam name="T">Element type of sequence</typeparam>
        /// <param name="source">The source sequence</param>
        /// <param name="length">The exactly number of items a sequence must have for this
        /// function to return true</param>
        /// <exception cref="ArgumentNullException">source is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">length is negative</exception>
        /// <returns><c>true</c> if the number of elements in the sequence is equals
        /// to the given integer or <c>false</c> otherwise.</returns>
        /// <example>
        /// <code>
        /// var numbers = { 123, 456, 789 };
        /// var result = numbers.Exactly(3);
        /// </code>
        /// The <c>result</c> variable will contain <c>true</c>.
        /// </example>
        public static bool Exactly<T>(this IEnumerable<T> source, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "length must not be negative.");

            return QuantityIterator(source, length + 1, count => count == length);
        }

        /// <summary>
        /// Returns true when the number of elements in the given sequence is between (inclusive)
        /// to the minimum and maximum given integer.
        /// This method throws an exception if the minimum given integer is negative
        /// or if the maximun given integer is lesser than the minimum integer.
        /// </summary>
        /// <remarks>
        /// The number of items streamed will be greater than or equal to the given integer.
        /// </remarks>
        /// <typeparam name="T">Element type of sequence</typeparam>
        /// <param name="source">The source sequence</param>
        /// <param name="min">The minimum number of items a sequence must have for this
        /// function to return true</param>
        /// <param name="max">The maximun number of items a sequence must have for this
        /// function to return true</param>
        /// <exception cref="ArgumentNullException">source is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">min is negative</exception>
        /// <exception cref="ArgumentOutOfRangeException">max is lesser than min</exception>
        /// <returns><c>true</c> if the number of elements in the sequence is between (inclusive)
        /// the min and max given integers or <c>false</c> otherwise.</returns>
        /// <example>
        /// <code>
        /// var numbers = { 123, 456, 789 };
        /// var result = numbers.CountBetween(1, 2);
        /// </code>
        /// The <c>result</c> variable will contain <c>false</c>.
        /// </example>
        public static bool CountBetween<T>(this IEnumerable<T> source, int min, int max)
        {
            if (min < 0) throw new ArgumentOutOfRangeException("min", "min must not be negative.");
            if (max < min) throw new ArgumentOutOfRangeException("max", "max must be greater than or equals to min.");

            return QuantityIterator(source, max + 1, count => min <= count && count <= max);
        }


        private static bool QuantityIterator<T>(IEnumerable<T> source, int limit, Func<int, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");

            var col = source as ICollection<T>;
            if (col != null)
            {
                return predicate(col.Count);
            }

            var count = 0;

            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (++count == limit)
                    {
                        break;
                    }
                }
            }

            return predicate(count);
        }
    }

    class CountTest
    {
        public static void Main()
        {
            Console.WriteLine(Enumerable.Range(1, 2).AtLeast(3) == false);
            Console.WriteLine(Enumerable.Range(1, 3).AtLeast(3) == true);
            Console.WriteLine(Enumerable.Range(1, 4).AtLeast(3) == true);

            Console.WriteLine();

            Console.WriteLine(Enumerable.Range(1, 2).AtMost(3) == true);
            Console.WriteLine(Enumerable.Range(1, 3).AtMost(3) == true);
            Console.WriteLine(Enumerable.Range(1, 4).AtMost(3) == false);

            Console.WriteLine();

            Console.WriteLine(Enumerable.Range(1, 2).Exactly(3) == false);
            Console.WriteLine(Enumerable.Range(1, 3).Exactly(3) == true);
            Console.WriteLine(Enumerable.Range(1, 4).Exactly(3) == false);

            Console.WriteLine();

            Console.WriteLine(Enumerable.Range(1, 1).CountBetween(2, 4) == false);
            Console.WriteLine(Enumerable.Range(1, 2).CountBetween(2, 4) == true);
            Console.WriteLine(Enumerable.Range(1, 3).CountBetween(2, 4) == true);
            Console.WriteLine(Enumerable.Range(1, 4).CountBetween(2, 4) == true);
            Console.WriteLine(Enumerable.Range(1, 5).CountBetween(2, 4) == false);

            Console.Read();
        }
    }
}
