using System;

namespace Algorithms.Numeric;

/// <summary>
///     Provides the Sigmoid (Logistic) function, commonly used as an activation
///     function in neural networks to squash values into the range (0, 1).
///     Formula: sigma(x) = 1 / (1 + e^(-x)).
/// </summary>
public static class Sigmoid
{
    /// <summary>
    ///     Calculates the value of the Sigmoid function for a given input x.
    /// </summary>
    /// <param name="x">The input number.</param>
    /// <returns>The Sigmoid value, a double between 0 and 1.</returns>
    public static double Calculate(double x)
    {
        // The Sigmoid function is 1 / (1 + e^(-x))
        // We use Math.Exp(-x) to calculate e^(-x)
        double exponent = Math.Exp(-x);

        // The result is 1.0 divided by (1.0 + exponent)
        return 1.0 / (1.0 + exponent);
    }
}
