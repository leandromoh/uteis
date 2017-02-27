public static bool EqualLength<T>(this IEnumerable<T> current, params IEnumerable<T>[] list)
{
    if (list.Length == 0)
        return true;

    if (!Enumerable.Repeat(current, 1)
        .Concat(list)
        .Select(x => x as ICollection<T>)
        .Where(x => x != null)
        .Select(x => x.Count)
        .AllEquals())
        return false;

    var enumerators = list.Select(x => x.GetEnumerator()).ToArray();

    foreach (var c in current)
        foreach (var e in enumerators)
            if (!e.MoveNext())
                return Dispose(enumerators, false);

    foreach (var e in enumerators)
        if (e.MoveNext())
            return Dispose(enumerators, false);

    return Dispose(enumerators, true);
}

private static bool Dispose<T>(IEnumerator<T>[] enumerators, bool result)
{
    Array.ForEach(enumerators, d => d.Dispose());
    return result;
}
