using System.Collections.Generic;

public static class ListExtenstions
{
    public static void AddMany<T>(this List<T> list, params T[] elements)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            list.Add(elements[i]);
        }
    }
}