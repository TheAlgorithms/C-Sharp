namespace Algorithms.Numeric;

/// <summary>
///     Add the integers without arithmetic operation.
/// </summary>
public static class AdditionWithoutArithmetic
{
    /// <summary>
    ///    Returns the sum of two integers.
    /// </summary>
    /// <param name="first">First number to add.</param>
    /// <param name="second">Second number to add.</param>
    /// <returns>Sum of the two numbers.</returns>
    public static int CalculateAdditionWithoutArithmetic(int first, int second)
    {
        while (second != 0)
        {
            int c = first & second;      // Carry
            first ^= second;             // Sum without carry
            second = c << 1;            // Carry shifted left
        }

        return first;
    }
}
