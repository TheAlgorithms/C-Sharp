namespace Algorithms.Sorters.String;

/// <summary>
///     Radix sort is a non-comparative sorting algorithm. It avoids comparison by creating
///     and distributing elements into buckets according to their radix.
///     Radix sorts can be implemented to start at either the most significant digit (MSD)
///     or least significant digit (LSD).
///     MSD radix sorts are most suitable for sorting array of strings with variable length
///     in lexicographical order.
/// </summary>
public class MsdRadixStringSorter : IStringSorter
{
    /// <summary>
    ///     Sort array of strings using MSD radix sort algorithm.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    public void Sort(string[] array) => Sort(array, 0, array.Length - 1, 0, new string[array.Length]);

    private static void Sort(string[] array, int l, int r, int d, string[] temp)
    {
        if (l >= r)
        {
            return;
        }

        const int k = 256;

        var count = new int[k + 2];
        for (var i = l; i <= r; i++)
        {
            var j = Key(array[i]);
            count[j + 2]++;
        }

        for (var i = 1; i < count.Length; i++)
        {
            count[i] += count[i - 1];
        }

        for (var i = l; i <= r; i++)
        {
            var j = Key(array[i]);
            temp[count[j + 1]++] = array[i];
        }

        for (var i = l; i <= r; i++)
        {
            array[i] = temp[i - l];
        }

        for (var i = 0; i < k; i++)
        {
            Sort(array, l + count[i], l + count[i + 1] - 1, d + 1, temp);
        }

        int Key(string s) => d >= s.Length ? -1 : s[d];
    }
}
