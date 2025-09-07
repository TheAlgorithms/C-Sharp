namespace Algorithms.Shufflers;

/// <summary>
///     LINQ Shuffle is a simple shuffling algorithm,
///     where the elements within a collection are shuffled using
///     LINQ queries and lambda expressions in C#.
/// </summary>
/// <typeparam name="T">Type array input.</typeparam>
public class LinqShuffler<T>
{
    /// <summary>
    /// First, it will generate a random value for each element.
    /// Next, it will sort the elements based on these generated
    /// random numbers using OrderBy.
    /// </summary>
    /// <param name="array">Array to shuffle.</param>
    /// <param name="seed">Random generator seed. Used to repeat the shuffle.</param>
    public T[] Shuffle(T[] array, int? seed = null)
    {
        var random = seed is null ? new Random() : new Random(seed.Value);
        return array.OrderBy(x => random.Next()).ToArray();
    }
}
