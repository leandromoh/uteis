        private static IEnumerable<TResult> UnFold<TSource, TResult>(TSource seed, Func<TSource, bool> predicate, Func<TSource, TResult> generateResult, Func<TSource, TSource> generateSeed)
        {
            while (true)
            {
                if (!predicate(seed))
                    yield break;

                yield return generateResult(seed);
                seed = generateSeed(seed);
            }
        }

        private static IEnumerable<TResult> UnFold<TSource, TResult>(TSource seed, Func<TSource, Tuple<TResult, TSource, bool>> generator)
        {
            while (true)
            {
                var ret = generator(seed);

                if (!ret.Item3)
                    yield break;

                yield return ret.Item1;
                seed = ret.Item2;
            }
        }
