using System;
using System.Collections.Generic;

public static class ListExtensions
{
    private static Random _randomRange = new();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = _randomRange.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}