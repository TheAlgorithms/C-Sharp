namespace Algorithms.Numeric;

/// <summary>
///     Implementation of the Hyperbolic Tangent (Tanh) function.
///     Tanh is an activation function that takes a real number as input and squashes
///     the output to a range between -1 and 1.
///     It is defined as: tanh(x) = (exp(x) - exp(-x)) / (exp(x) + exp(-x)).
///     https://en.wikipedia.org/wiki/Hyperbolic_function#Hyperbolic_tangent.
/// </summary>
public static class Tanh
{
    /// <summary>
    ///     Compute the Hyperbolic Tangent (Tanh) function for a single value.
    ///     The Math.Tanh() method is used for efficient and accurate computation.
    /// </summary>
    /// <param name="input">The input real number.</param>
    /// <returns>The output real number in the range [-1, 1].</returns>
    public static double Compute(double input)
    {
        // For a single double, we can directly use the optimized Math.Tanh method.
        return Math.Tanh(input);
    }

    /// <summary>
    ///     Compute the Hyperbolic Tangent (Tanh) function element-wise for a vector.
    /// </summary>
    /// <param name="input">The input vector of real numbers.</param>
    /// <returns>The output vector of real numbers, where each element is in the range [-1, 1].</returns>
    public static double[] Compute(double[] input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        if (input.Length == 0)
        {
            throw new ArgumentException("Array is empty.");
        }

        var outputVector = new double[input.Length];

        for (var index = 0; index < input.Length; index++)
        {
            // Apply Tanh to each element using the optimized Math.Tanh method.
            outputVector[index] = Math.Tanh(input[index]);
        }

        return outputVector;
    }
}
