using System;
using System.Numerics;

namespace Algorithms.Numeric;

/// <summary>
///     Calculates the sine function (sin(x)) using the Maclaurin series expansion (Taylor series centered at 0).
///     sin(x) = x - x^3/3! + x^5/5! - x^7/7! + ...
/// </summary>
public static class Sin
{
    /// <summary>
    ///     Calculates the sine of an angle x using a finite number of terms from the Maclaurin series.
    ///     Note: For high precision, use Math.Sin(). This method is for demonstrating the series expansion.
    /// </summary>
    /// <param name="x">The angle in radians.</param>
    /// <param name="terms">The number of terms (odd powers) to include in the series. Default is 20 for required precision.</param>
    /// <returns>The approximate value of sin(x).</returns>
    public static double Calculate(double x, int terms = 20) // Increased default from 15 to 20 for higher precision
    {
        if (terms <= 0)
        {
            throw new ArgumentException("The number of terms must be a positive integer.");
        }

        // Best practice for series convergence: reduce the angle to the range [-2*PI, 2*PI].
        // The series converges for all x, but convergence is faster near 0.
        x %= 2 * Math.PI;

        double result = 0.0;
        double xSquared = x * x;

        // currentPower: Stores x^k
        double currentPower = x; // Starts at x^1

        // currentFactorial: Stores k!
        double currentFactorial = 1.0; // Starts at 1!

        int sign = 1; // Starts positive

        // k is the exponent/factorial index (1, 3, 5, 7, ...)
        for (int i = 0, k = 1; i < terms; i++, k += 2)
        {
            if (k > 1)
            {
                // Iteratively update the components for the next term:
                // Current exponent k is (k-2) + 2.
                // x^k = x^(k-2) * x^2
                currentPower *= xSquared;

                // k! = (k-2)! * (k-1) * k
                // Example: 5! = 3! * 4 * 5
                currentFactorial *= (k - 1) * k;
            }

            // Calculate the term: (sign) * x^k / k!
            double term = currentPower / currentFactorial;

            result += sign * term;

            sign *= -1; // Alternate the sign for the next term
        }

        return result;
    }
}
