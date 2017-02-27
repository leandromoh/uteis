public static bool AllEquals<TSource>(this IEnumerable<TSource> source)
{
    return source.AllEquals(null);
}

public static bool AllEquals<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
{
	comparer = comparer ?? EqualityComparer<TSource>.Default;

	using (var e = source.GetEnumerator())
	{
		if (!e.MoveNext())
		{
			return true;
		}
		else
		{
			var first = e.Current;
			var firstHash = comparer.GetHashCode(first);

			while (e.MoveNext())
			{
				if (comparer.GetHashCode(e.Current) != firstHash || !comparer.Equals(e.Current, first))
				{
					return false;
				}
			}

			return true;
		}
	}
}
