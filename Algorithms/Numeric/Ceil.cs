namespace Algorithms.Numeric;

/// <summary>
///     Perform ceiling operation on a number.
/// </summary>
public static class Ceil
{
    /// <summary>
    ///    Returns the smallest integer greater than or equal to the number.
    /// </summary>
    /// <typeparam name="T">Type of number.</typeparam>
    /// <param name="inputNum">Number to find the ceiling of.</param>
    /// <returns>Ceiling value of the number.</returns>
    public static T CeilVal<T>(T inputNum) where T : INumber<T>
    {
        T intPart = T.CreateChecked(Convert.ToInt32(inputNum));

        return inputNum > intPart ? intPart + T.One : intPart;
    }
}
