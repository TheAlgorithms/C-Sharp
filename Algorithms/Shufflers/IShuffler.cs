namespace Algorithms.Shufflers;

/// <summary>
///     Shuffles array.
/// </summary>
/// <typeparam name="T">Type of array item.</typeparam>
public interface IShuffler<in T>
{
    /// <summary>
    ///     Shuffles array.
    /// </summary>
    /// <param name="array">Array to Shuffle.</param>
    void Shuffle(T[] array, int? seed = null);
}
