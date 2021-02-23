using System;
using System.Linq;
using Utilities.Extensions;

namespace Algorithms.LinearAlgebra.Eigenvalue
{
    /// <summary>
    /// Power iteration method - eigenvalue numeric algorithm, based on recurrent relation:
    /// Li+1 = (A * Li) / || A * Li ||, where Li - eigenvector approximation.
    /// </summary>
    public static class PowerIteration
    {
        /// <summary>
        /// Returns approximation of the dominant eigenvalue and eigenvector of <paramref name="source"/> matrix.
        /// </summary>
        /// <list type="bullet">
        /// <item>
        /// <description>The algorithm will not converge if the start vector is orthogonal to the eigenvector.</description>
        /// </item>
        /// <item>
        /// <description>The <paramref name="source"/> matrix must be square-shaped.</description>
        /// </item>
        /// </list>
        /// <param name="source">Source square-shaped matrix.</param>
        /// <param name="startVector">Start vector.</param>
        /// <param name="error">Accuracy of the result.</param>
        /// <returns>Dominant eigenvalue and eigenvector pair.</returns>
        /// <exception cref="ArgumentException">The <paramref name="source"/> matrix is not square-shaped.</exception>
        /// <exception cref="ArgumentException">The length of the start vector doesn't equal the size of the source matrix.</exception>
        public static (double eigenvalue, double[] eigenvector) Dominant(double[,] source, double[] startVector, double error = 0.00001)
        {
            if (source.GetLength(0) != source.GetLength(1))
            {
                throw new ArgumentException("The source matrix is not square-shaped.");
            }

            if (source.GetLength(0) != startVector.Length)
            {
                throw new ArgumentException("The length of the start vector doesn't equal the size of the source matrix.");
            }

            double eigenNorm;
            double[] previousEigenVector;
            double[] currentEigenVector = startVector;

            do
            {
                previousEigenVector = currentEigenVector;
                currentEigenVector = source.Multiply(
                    previousEigenVector.ToColumnVector())
                .ToRowVector();

                eigenNorm = currentEigenVector.Magnitude();
                currentEigenVector = currentEigenVector.Select(x => x / eigenNorm).ToArray();
            }
            while (Math.Abs(currentEigenVector.Dot(previousEigenVector)) < 1.0 - error);

            var eigenvalue = source.Multiply(currentEigenVector.ToColumnVector()).ToRowVector().Magnitude();

            return (eigenvalue: eigenvalue, eigenvector: currentEigenVector);
        }

        /// <summary>
        /// Returns approximation of the dominant eigenvalue and eigenvector of <paramref name="source"/> matrix.
        /// Random normalized vector is used as the start vector to decrease chance of orthogonality to the eigenvector.
        /// </summary>
        /// <list type="bullet">
        /// <item>
        /// <description>The algorithm will not converge if the start vector is orthogonal to the eigenvector.</description>
        /// </item>
        /// <item>
        /// <description>The <paramref name="source"/> matrix should be square-shaped.</description>
        /// </item>
        /// </list>
        /// <param name="source">Source square-shaped matrix.</param>
        /// <param name="error">Accuracy of the result.</param>
        /// <returns>Dominant eigenvalue and eigenvector pair.</returns>
        /// <exception cref="ArgumentException">The <paramref name="source"/> matrix is not square-shaped.</exception>
        /// <exception cref="ArgumentException">The length of the start vector doesn't equal the size of the source matrix.</exception>
        public static (double eigenvalue, double[] eigenvector) Dominant(double[,] source, double error = 0.00001) =>
            Dominant(source, new Random().NextVector(source.GetLength(1)), error);
    }
}
