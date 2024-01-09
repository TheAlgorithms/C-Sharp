using System;

namespace Utilities.Extensions;

public static class VectorExtensions
{
    /// <summary>
    ///     Makes a copy of a vector. Changes to the copy should not affect the original.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>The copy.</returns>
    public static double[] Copy(this double[] vector)
    {
        var result = new double[vector.Length];
        for (var i = 0; i < vector.Length; i++)
        {
            result[i] = vector[i];
        }

        return result;
    }

    /// <summary>
    ///     Computes the outer product of two vectors.
    /// </summary>
    /// <param name="lhs">The LHS vector.</param>
    /// <param name="rhs">The RHS vector.</param>
    /// <returns>The outer product of the two vector.</returns>
    public static double[,] OuterProduct(this double[] lhs, double[] rhs)
    {
        var result = new double[lhs.Length, rhs.Length];
        for (var i = 0; i < lhs.Length; i++)
        {
            for (var j = 0; j < rhs.Length; j++)
            {
                result[i, j] = lhs[i] * rhs[j];
            }
        }

        return result;
    }

    /// <summary>
    ///     Computes the dot product of two vectors.
    /// </summary>
    /// <param name="lhs">The LHS vector.</param>
    /// <param name="rhs">The RHS vector.</param>
    /// <returns>The dot product of the two vector.</returns>
    /// <exception cref="ArgumentException">Dimensions of vectors do not match.</exception>
    public static double Dot(this double[] lhs, double[] rhs)
    {
        if (lhs.Length != rhs.Length)
        {
            throw new ArgumentException("Dot product arguments must have same dimension");
        }

        double result = 0;
        for (var i = 0; i < lhs.Length; i++)
        {
            result += lhs[i] * rhs[i];
        }

        return result;
    }

    /// <summary>
    ///     Computes the magnitude of a vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>The magnitude.</returns>
    public static double Magnitude(this double[] vector)
    {
        var magnitude = Dot(vector, vector);
        magnitude = Math.Sqrt(magnitude);
        return magnitude;
    }

    /// <summary>
    ///     Returns the scaled vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="factor">Scale factor.</param>
    /// <returns>The unit vector.</returns>
    public static double[] Scale(this double[] vector, double factor)
    {
        var result = new double[vector.Length];
        for (var i = 0; i < vector.Length; i++)
        {
            result[i] = vector[i] * factor;
        }

        return result;
    }

    /// <summary>
    ///     Transpose 1d row vector to column vector.
    /// </summary>
    /// <param name="source">Input 1d vector.</param>
    /// <returns>Column vector.</returns>
    public static double[,] ToColumnVector(this double[] source)
    {
        var columnVector = new double[source.Length, 1];

        for (var i = 0; i < source.Length; i++)
        {
            columnVector[i, 0] = source[i];
        }

        return columnVector;
    }

    /// <summary>
    ///     Transpose column vector to 1d row vector.
    /// </summary>
    /// <param name="source">Input column vector.</param>
    /// <returns>Row vector.</returns>
    /// <exception cref="InvalidOperationException">The column vector should have only 1 element in width.</exception>
    public static double[] ToRowVector(this double[,] source)
    {
        if (source.GetLength(1) != 1)
        {
            throw new InvalidOperationException("The column vector should have only 1 element in width.");
        }

        var rowVector = new double[source.Length];

        for (var i = 0; i < rowVector.Length; i++)
        {
            rowVector[i] = source[i, 0];
        }

        return rowVector;
    }

    /// <summary>
    ///     Generates a diagonal matrix from an specified vector.
    /// </summary>
    /// <param name="vector">The input vector.</param>
    /// <returns>A Diagonal matrix.</returns>
    public static double[,] ToDiagonalMatrix(this double[] vector)
    {
        var len = vector.Length;
        var result = new double[len, len];

        for (var i = 0; i < len; i++)
        {
            result[i, i] = vector[i];
        }

        return result;
    }
}
