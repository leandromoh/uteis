public static void ForEachLast<T>(this IEnumerable<T> source, Action<T> defaultAction, params Action<T>[] actions)
{
    if (source == null) throw new ArgumentNullException(nameof(source));
    if (defaultAction == null) throw new ArgumentNullException(nameof(defaultAction));

    actions.ForEach((x, i) => {
        if (x == null)
            throw new ArgumentNullException($"{i+1}th last function is null");
    });

    var funcs = actions.Select(x => new Action<T, int>((y, i) => x(y))).ToArray();
    source.ForEachLast((x, i) => defaultAction(x), funcs);
}

public static void ForEachLast<T>(this IEnumerable<T> source, Action<T, int> defaultAction, params Action<T, int>[] actions)
{
    if (source == null) throw new ArgumentNullException(nameof(source));
    if (defaultAction == null) throw new ArgumentNullException(nameof(defaultAction));

    actions.ForEach((x, i) => {
        if (x == null)
            throw new ArgumentNullException($"{i+1}th last function is null");
    });
    
    var col = source as ICollection<T>;
    if (col != null)
    {
        ForEachLastImpl(col, defaultAction, actions);
    }
    else
    {
        ForEachLastImpl(source, defaultAction, actions);
    }
}

private static void ForEachLastImpl<T>(ICollection<T> source, Action<T, int> defaultAction, params Action<T, int>[] actions)
{
    var count = actions.Length;
    var len = source.Count - actions.Length;
    var index = 0;

    using (var e = source.GetEnumerator())
    {
        e.MoveNext();

        for (int i = 0; i < len; i++, e.MoveNext())
        {
            defaultAction(e.Current, index++);
        }
        
        for (int i = Math.Abs(count - (source.Count - index)); i < count; i++, e.MoveNext())
        {
            actions[i](e.Current, index++);
        }
    }
}

private static void ForEachLastImpl<T>(IEnumerable<T> source, Action<T, int> defaultAction, params Action<T, int>[] actions)
{
    var count = actions.Length;
    var queue = new Queue<T>(count);
    var index = 0;

    foreach (var item in source)
    {
        if (queue.Count < count)
        {
            queue.Enqueue(item);
            continue;
        }

        defaultAction(queue.Dequeue(), index++);
        queue.Enqueue(item);
    }

    for (int i = Math.Abs(count - queue.Count); i < count; i++)
    {
        actions[i](queue.Dequeue(), index++);
    }
} 
