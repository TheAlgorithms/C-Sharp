namespace Algorithms.Numeric;

/// <summary>
///     Perform floor operation on a number.
/// </summary>
public static class Floor
{
    /// <summary>
    ///    Returns the largest integer less than or equal to the number.
    /// </summary>
    /// <typeparam name="T">Type of number.</typeparam>
    /// <param name="inputNum">Number to find the floor of.</param>
    /// <returns>Floor value of the number.</returns>
    public static T FloorVal<T>(T inputNum) where T : INumber<T>
    {
        T intPart = T.CreateChecked(Convert.ToInt32(inputNum));

        return inputNum < intPart ? intPart - T.One : intPart;
    }
}
