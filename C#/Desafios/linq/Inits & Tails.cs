public static IEnumerable<IEnumerable<TSource>> Inits<TSource>(this IEnumerable<TSource> source)
{
    yield return Enumerable.Empty<TSource>();

    int i = 0;

    foreach (var _ in source)
    {
        checked
        {
            yield return source.Take(++i);
        }
    }
}

private static IEnumerable<IEnumerable<TSource>> Tails<TSource>(this IEnumerable<TSource> source)
{
    int i = 0;

    foreach (var _ in source)
    {
        checked
        {
            yield return source.Skip(i++);
        }
    }

    yield return Enumerable.Empty<TSource>();
}
