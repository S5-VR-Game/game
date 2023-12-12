using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    /// <summary>
    /// Shuffles the given array using the unity <see cref="Random"/> object
    /// </summary>
    /// <param name="array">array to be shuffled</param>
    public static void Shuffle<T>(this T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (array[k], array[n]) = (array[n], array[k]);
        }
    }
    
    /// <summary>
    /// Shuffles the given list using the unity <see cref="Random"/> object
    /// </summary>
    /// <param name="list">list to be shuffled</param>
    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}