namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Class that implements bogo sort algorithm.
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public class BogoSorter<T> : IComparisonSorter<T>
{
    private readonly Random random = new();

    /// <summary>
    ///     Sorts array using specified comparer,
    ///     randomly shuffles elements until array is sorted,
    ///     internal, in-place, unstable,
    ///     worst-case time complexity: unbounded (infinite),
    ///     average time complexity: O((n+1)!),
    ///     space complexity: O(n),
    ///     where n - array length.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    /// <param name="comparer">Compares elements.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
        while (!IsSorted(array, comparer))
        {
            Shuffle(array);
        }
    }

    private bool IsSorted(T[] array, IComparer<T> comparer)
    {
        for (var i = 0; i < array.Length - 1; i++)
        {
            if (comparer.Compare(array[i], array[i + 1]) > 0)
            {
                return false;
            }
        }

        return true;
    }

    private void Shuffle(T[] array)
    {
        var taken = new bool[array.Length];
        var newArray = new T[array.Length];
        for (var i = 0; i < array.Length; i++)
        {
            int nextPos;
            do
            {
                nextPos = random.Next(0, int.MaxValue) % array.Length;
            }
            while (taken[nextPos]);

            taken[nextPos] = true;
            newArray[nextPos] = array[i];
        }

        for (var i = 0; i < array.Length; i++)
        {
            array[i] = newArray[i];
        }
    }
}
