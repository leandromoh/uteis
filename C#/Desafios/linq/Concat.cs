public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> source, params IEnumerable<TSource>[] sequences)
{
    return source.Concat(sequences.AsEnumerable());
}

public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> source, IEnumerable<IEnumerable<TSource>> sequences)
{
    foreach (var item in source)
    {
        yield return item;
    }

    foreach (var innerList in sequences)
    {
        foreach (var item in innerList)
        {
            yield return item;
        }
    }
}
