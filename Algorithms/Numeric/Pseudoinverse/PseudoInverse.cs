using System;
using Algorithms.Numeric.Decomposition;
using Utilities.Extensions;

namespace Algorithms.Numeric.Pseudoinverse;

/// <summary>
///     The Moore–Penrose pseudo-inverse A+ of a matrix A,
///     is a general way to find the solution to the following system of linear equations:
///     ~b = A ~y. ~b e R^m; ~y e R^n; A e Rm×n.
///     There are varios methods for construction the pseudo-inverse.
///     This one is based on Singular Value Decomposition (SVD).
/// </summary>
public static class PseudoInverse
{
    /// <summary>
    ///     Return the pseudoinverse of a matrix based on the Moore-Penrose Algorithm.
    ///     using Singular Value Decomposition (SVD).
    /// </summary>
    /// <param name="inMat">Input matrix to find its inverse to.</param>
    /// <returns>The inverse matrix approximation of the input matrix.</returns>
    public static double[,] PInv(double[,] inMat)
    {
        // To compute the SVD of the matrix to find Sigma.
        var (u, s, v) = ThinSvd.Decompose(inMat);

        // To take the reciprocal of each non-zero element on the diagonal.
        var len = s.Length;

        var sigma = new double[len];
        for (var i = 0; i < len; i++)
        {
            sigma[i] = Math.Abs(s[i]) < 0.0001 ? 0 : 1 / s[i];
        }

        // To construct a diagonal matrix based on the vector result.
        var diag = sigma.ToDiagonalMatrix();

        // To construct the pseudo-inverse using the computed information above.
        var matinv = u.Multiply(diag).Multiply(v.Transpose());

        // To Transpose the result matrix.
        return matinv.Transpose();
    }
}
