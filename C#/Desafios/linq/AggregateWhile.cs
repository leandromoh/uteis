        public static TSource AggregateWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func, Func<TSource, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            using (var e = source.GetEnumerator())
            {
                if (!e.MoveNext())
                    throw new InvalidOperationException("Sequence contains no elements.");

                if (!predicate(e.Current))
                    throw new InvalidOperationException("The first element of the sequence does not satisfies the predicate.");

                return AggregateWhileImp(e, e.Current, func, predicate);
            }
        }

        public static TAccumulate AggregateWhile<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            using (var e = source.GetEnumerator())
            {
                return AggregateWhileImp(e, seed, func, predicate);
            }
        }

        private static TResult AggregateWhileImp<TSource, TResult>(IEnumerator<TSource> e, TResult accumulator, Func<TResult, TSource, TResult> func, Func<TResult, bool> predicate)
        {
            var temp = accumulator;

            while (e.MoveNext())
            {
                accumulator = func(accumulator, e.Current);

                if (!predicate(accumulator))
                {
                    break;
                }

                temp = accumulator;
            }

            return temp;
        }
