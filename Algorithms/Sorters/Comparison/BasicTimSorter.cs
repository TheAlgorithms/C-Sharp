namespace Algorithms.Sorters.Comparison;

/// <summary>
/// A basic implementation of the TimSort algorithm for sorting arrays.
/// </summary>
/// <typeparam name="T">The type of elements in the array.</typeparam>
public class BasicTimSorter<T>
{
    private readonly int minRuns = 32;
    private readonly IComparer<T> comparer;

    /// <summary>
    /// Initializes a new instance of the <see cref="BasicTimSorter{T}"/> class.
    /// </summary>
    /// <param name="comparer">The comparer to use for comparing elements.</param>
    public BasicTimSorter(IComparer<T> comparer)
    {
        this.comparer = comparer ?? Comparer<T>.Default;
    }

    /// <summary>
    /// Sorts the specified array using the TimSort algorithm.
    /// </summary>
    /// <param name="array">The array to sort.</param>
    public void Sort(T[] array)
    {
        var n = array.Length;

        // Step 1: Sort small pieces of the array using Insertion Sort
        for (var i = 0; i < n; i += minRuns)
        {
            InsertionSort(array, i, Math.Min(i + minRuns - 1, n - 1));
        }

        // Step 2: Merge sorted runs using Merge Sort
        for (var size = minRuns; size < n; size *= 2)
        {
            for (var left = 0; left < n; left += 2 * size)
            {
                var mid = left + size - 1;
                var right = Math.Min(left + 2 * size - 1, n - 1);

                if (mid < right)
                {
                    Merge(array, left, mid, right);
                }
            }
        }
    }

    /// <summary>
    /// Sorts a portion of the array using the Insertion Sort algorithm.
    /// </summary>
    /// <param name="array">The array to sort.</param>
    /// <param name="left">The starting index of the portion to sort.</param>
    /// <param name="right">The ending index of the portion to sort.</param>
    private void InsertionSort(T[] array, int left, int right)
    {
        for (var i = left + 1; i <= right; i++)
        {
            var key = array[i];
            var j = i - 1;

            // Move elements of array[0..i-1], that are greater than key,
            // to one position ahead of their current position
            while (j >= left && comparer.Compare(array[j], key) > 0)
            {
                array[j + 1] = array[j];
                j--;
            }

            array[j + 1] = key;
        }
    }

    /// <summary>
    /// Merges two sorted subarrays into a single sorted subarray.
    /// </summary>
    /// <param name="array">The array containing the subarrays to merge.</param>
    /// <param name="left">The starting index of the first subarray.</param>
    /// <param name="mid">The ending index of the first subarray.</param>
    /// <param name="right">The ending index of the second subarray.</param>
    private void Merge(T[] array, int left, int mid, int right)
    {
        // Create segments for left and right subarrays
        var leftSegment = new ArraySegment<T>(array, left, mid - left + 1);
        var rightSegment = new ArraySegment<T>(array, mid + 1, right - mid);

        // Convert segments to arrays
        var leftArray = leftSegment.ToArray();
        var rightArray = rightSegment.ToArray();

        var i = 0;
        var j = 0;
        var k = left;

        // Merge the two subarrays back into the main array
        while (i < leftArray.Length && j < rightArray.Length)
        {
            array[k++] = comparer.Compare(leftArray[i], rightArray[j]) <= 0 ? leftArray[i++] : rightArray[j++];
        }

        // Copy remaining elements from leftArray, if any
        while (i < leftArray.Length)
        {
            array[k++] = leftArray[i++];
        }

        // Copy remaining elements from rightArray, if any
        while (j < rightArray.Length)
        {
            array[k++] = rightArray[j++];
        }
    }
}
