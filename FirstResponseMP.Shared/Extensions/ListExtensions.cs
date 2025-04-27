using System;
using System.Collections.Generic;

namespace FirstResponseMP.Shared.Extensions;

public static class ListExtensions
{
    private static readonly Random Random = new Random();
    public static T? SelectRandom<T>(this List<T?> list) => list[Random.Next(Math.Max(0, list.Count - 1))] ?? default;
}