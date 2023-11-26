using UnityEngine;

public static class Extensions
{
    /// <summary>
    /// Shuffles the given list using the unity <see cref="Random"/> object
    /// </summary>
    /// <param name="list">list to be shuffled</param>
    public static void Shuffle<T>(this T[] list)
    {
        int n = list.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}