using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstResponseMP.Shared.Extensions;

public static class ListExtensions
{
    private static readonly Random Random = new Random();

    public static T SelectRandom<T>(this List<T> list)
    {
        if (!list.Any())
            throw new ArgumentException("List must contain at least one element");
        return list[Random.Next(Math.Max(0, list.Count - 1))];
    }
}