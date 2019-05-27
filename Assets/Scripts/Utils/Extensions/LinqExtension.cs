using System;
using System.Collections.Generic;
using System.Linq;

public static class LinqExtension
{
    public static Queue<TSource> OrderQueue<TSource, TKey>(this Queue<TSource> oldQueue, Func<TSource, TKey> func)
    {
        return new Queue<TSource>(oldQueue.OrderBy(func));
    }
}
