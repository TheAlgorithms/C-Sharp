namespace Algorithms.Shufflers;

/// <summary>
///     Recursive Shuffler is a recursive version of
///     Fisher-Yates shuffle algorithm. This can only be used
///     for educational purposes due to stack depth limits.
/// </summary>
/// <typeparam name="T">Type array input.</typeparam>
public class RecursiveShuffler<T> : IShuffler<T>
{
    /// <summary>
    /// This is the public overload method that calls the private overload method.
    /// </summary>
    /// <param name="array">Array to shuffle.</param>
    /// <param name="seed">Random generator seed. Used to repeat the shuffle.</param>
    public void Shuffle(T[] array, int? seed = null)
    {
        Shuffle(array, array.Length - 1, seed);
    }

    /// <summary>
    /// First, it will check the length of the array on the base case.
    /// Next, if there's still items left, it will shuffle the sub-array.
    /// Lastly, it will randomly select index from 0 to number of items of the array
    /// then swap the elements array[items] and array[index].
    /// </summary>
    /// <param name="array">Array to shuffle.</param>
    /// <param name="items">Number of items in the array.</param>
    /// <param name="seed">Random generator seed. Used to repeat the shuffle.</param>
    private void Shuffle(T[] array, int items, int? seed)
    {
        if (items <= 0)
        {
            return;
        }

        Shuffle(array, items - 1, seed);
        var random = seed is null ? new Random() : new Random(seed.Value);
        int index = random.Next(items + 1);
        (array[items], array[index]) = (array[index], array[items]);
        (array[items], array[index]) = (array[index], array[items]);
    }
}
