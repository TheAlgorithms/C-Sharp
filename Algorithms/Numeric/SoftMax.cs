using System;

namespace Algorithms.Numeric;

/// <summary>
///     Implementation of the SoftMax function.
///     Its a function that takes as input a vector of K real numbers, and normalizes
///     it into a probability distribution consisting of K probabilities proportional
///     to the exponentials of the input numbers. After softmax, the elements of the vector always sum up to 1.
///     https://en.wikipedia.org/wiki/Softmax_function.
/// </summary>
public static class SoftMax
{
    /// <summary>
    ///    Compute the SoftMax function.
    ///    The SoftMax function is defined as:
    ///    softmax(x_i) = exp(x_i) / sum(exp(x_j)) for j = 1 to n
    ///    where x_i is the i-th element of the input vector.
    ///    The elements of the output vector are the probabilities of the input vector, the output sums up to 1.
    /// </summary>
    /// <param name="input">The input vector of real numbers.</param>
    /// <returns>The output vector of real numbers.</returns>
    public static double[] Compute(double[] input)
    {
        if (input.Length == 0)
        {
            throw new ArgumentException("Array is empty.");
        }

        var exponentVector = new double[input.Length];
        var sum = 0.0;
        for (var index = 0; index < input.Length; index++)
        {
            exponentVector[index] = Math.Exp(input[index]);
            sum += exponentVector[index];
        }

        for (var index = 0; index < input.Length; index++)
        {
            exponentVector[index] /= sum;
        }

        return exponentVector;
    }
}
