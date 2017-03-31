public static IEnumerable<TSource> Intercalate<TSource>(this IEnumerable<TSource> source, TSource intercal)
{
    using (var e = source.GetEnumerator())
    {
        if (!e.MoveNext())
        {
            yield break;
        }
        else
        {
            TSource i = e.Current;

            while (e.MoveNext())
            {
                yield return i;
                yield return intercal;

                i = e.Current;
            }

            yield return i;
        }
    }
}
