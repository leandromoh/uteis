public static bool AtLeast<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second)
{
	return first.CompareCount(second) >= 0;
}

public static bool AtMost<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second)
{
	return first.CompareCount(second) <= 0;
}

public static bool Exactly<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second)
{
	return first.CompareCount(second) == 0;
}

public static int CompareCount<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second)
{
	if (first == null) throw new ArgumentNullException(nameof(first));
	if (second == null) throw new ArgumentNullException(nameof(second));

	var firstCol = first as ICollection<T1>;
	var secondCol = second as ICollection<T2>;

	if (firstCol != null)
	{
		if (secondCol != null)
		{
			return firstCol.Count.CompareTo(secondCol.Count);
		}

		return firstCol.Count.CompareTo(QuantityIterator(second, firstCol.Count + 1));
	}

	if (secondCol != null)
	{
		return QuantityIterator(first, secondCol.Count + 1).CompareTo(secondCol.Count);
	}

	bool firstHasNext, secondHasNext;

	using (var e1 = first.GetEnumerator())
	using (var e2 = second.GetEnumerator())
	{
		do
		{
			firstHasNext = e1.MoveNext();
			secondHasNext = e2.MoveNext();
		}
		while (firstHasNext && secondHasNext);
	}

	return Convert.ToInt32(firstHasNext).CompareTo(Convert.ToInt32(secondHasNext));
}

private static int QuantityIterator<T>(IEnumerable<T> source, int limit)
{
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

	return count;
}
