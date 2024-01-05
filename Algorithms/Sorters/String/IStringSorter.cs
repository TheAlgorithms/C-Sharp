namespace Algorithms.Sorters.String;

/// <summary>
///     Sorts array of strings without comparing them.
/// </summary>
public interface IStringSorter
{
    /// <summary>
    ///     Sorts array in ascending order.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    void Sort(string[] array);
}
