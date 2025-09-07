namespace Algorithms.Shufflers;

/// <summary>
///     Naive Shuffle is a simple and incorrect shuffling algorithm
///     that randomly swaps every element with any other element in the array.
/// </summary>
/// <typeparam name="T">Type array input.</typeparam>
public class NaiveShuffler<T> : IShuffler<T>
{
    /// <summary>
    /// First, it loop from 0 to n - 1.
    /// Next, it will randomly pick any j in the array.
    /// Lastly, it will swap array[i] with array[j].
    /// </summary>
    /// <param name="array">Array to shuffle.</param>
    /// <param name="seed">Random generator seed. Used to repeat the shuffle.</param>
    public void Shuffle(T[] array, int? seed = null)
    {
        var random = seed is null ? new Random() : new Random(seed.Value);
        for (int i = 0; i < array.Length; i++)
        {
            int j = random.Next(array.Length);
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
