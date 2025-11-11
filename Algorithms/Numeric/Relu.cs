namespace Algorithms.Numeric;

/// <summary>
///     Implementation of the Rectified Linear Unit (ReLU) function.
///     ReLU is defined as: ReLU(x) = max(0, x).
///     It is commonly used as an activation function in neural networks.
/// </summary>
public static class Relu
{
    /// <summary>
    ///     Compute the Rectified Linear Unit (ReLU) for a single value.
    /// </summary>
    /// <param name="input">The input real number.</param>
    /// <returns>The output real number (>= 0).</returns>
    public static double Compute(double input)
    {
        return Math.Max(0.0, input);
    }

    /// <summary>
    ///     Compute the Rectified Linear Unit (ReLU) element-wise for a vector.
    /// </summary>
    /// <param name="input">The input vector of real numbers.</param>
    /// <returns>The output vector where each element is max(0, input[i]).</returns>
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

        var output = new double[input.Length];

        for (var i = 0; i < input.Length; i++)
        {
            output[i] = Math.Max(0.0, input[i]);
        }

        return output;
    }
}
