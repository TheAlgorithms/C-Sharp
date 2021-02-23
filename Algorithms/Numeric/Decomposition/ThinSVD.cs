using System;
using Utilities.Extensions;
using M = Utilities.Extensions.MatrixExtensions;
using V = Utilities.Extensions.VectorExtensions;

namespace Algorithms.Numeric.Decomposition
{
    /// <summary>
    /// Singular Vector Decomposition decomposes any general matrix into its
    /// singular values and a set of orthonormal bases.
    /// </summary>
    public static class ThinSvd
    {
        /// <summary>
        /// Computes a random unit vector.
        /// </summary>
        /// <param name="dimensions">The dimensions of the required vector.</param>
        /// <returns>The unit vector.</returns>
        public static double[] RandomUnitVector(int dimensions)
        {
            Random random = new ();
            double[] result = new double[dimensions];
            for (var i = 0; i < dimensions; i++)
            {
                result[i] = 2 * random.NextDouble() - 1;
            }

            var magnitude = result.Magnitude();
            result = result.Scale(1 / magnitude);
            return result;
        }

        /// <summary>
        /// Computes a single singular vector for the given matrix, corresponding to the largest singular value.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>A singular vector, with dimension equal to number of columns of the matrix.</returns>
        public static double[] Decompose1D(double[,] matrix) =>
            Decompose1D(matrix, 1E-5, 100);

        /// <summary>
        /// Computes a single singular vector for the given matrix, corresponding to the largest singular value.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="epsilon">The error margin.</param>
        /// <param name="maxIterations">The maximum number of iterations.</param>
        /// <returns>A singular vector, with dimension equal to number of columns of the matrix.</returns>
        public static double[] Decompose1D(double[,] matrix, double epsilon, int maxIterations)
        {
            var n = matrix.GetLength(1);
            var iterations = 0;
            double mag;
            double[] lastIteration;
            double[] currIteration = RandomUnitVector(n);
            double[,] b = matrix.Transpose().Multiply(matrix);
            do
            {
                lastIteration = currIteration.Copy();
                currIteration = b.MultiplyVector(lastIteration);
                currIteration = currIteration.Scale(100);
                mag = currIteration.Magnitude();
                if (mag > epsilon)
                {
                    currIteration = currIteration.Scale(1 / mag);
                }

                iterations++;
            }
            while (lastIteration.Dot(currIteration) < 1 - epsilon && iterations < maxIterations);

            return currIteration;
        }

        public static (double[,] U, double[] S, double[,] V) Decompose(double[,] matrix) =>
            Decompose(matrix, 1E-5, 100);

        /// <summary>
        /// Computes the SVD for the given matrix, with singular values arranged from greatest to least.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="epsilon">The error margin.</param>
        /// <param name="maxIterations">The maximum number of iterations.</param>
        /// <returns>The SVD.</returns>
        public static (double[,] U, double[] S, double[,] V) Decompose(double[,] matrix, double epsilon, int maxIterations)
        {
            var m = matrix.GetLength(0);
            var n = matrix.GetLength(1);
            var numValues = Math.Min(m, n);

            // sigmas is be a diagonal matrix, hence only a vector is needed
            double[] sigmas = new double[numValues];
            double[,] us = new double[m, numValues];
            double[,] vs = new double[n, numValues];

            // keep track of progress
            double[,] remaining = matrix.Copy();

            // for each singular value
            for (var i = 0; i < numValues; i++)
            {
                // compute the v singular vector
                double[] v = Decompose1D(remaining, epsilon, maxIterations);
                double[] u = matrix.MultiplyVector(v);

                // compute the contribution of this pair of singular vectors
                double[,] contrib = u.OuterProduct(v);

                // extract the singular value
                var s = u.Magnitude();

                // v and u should be unit vectors
                if (s < epsilon)
                {
                    u = new double[m];
                    v = new double[n];
                }
                else
                {
                    u = u.Scale(1 / s);
                }

                // save u, v and s into the result
                for (var j = 0; j < u.Length; j++)
                {
                    us[j, i] = u[j];
                }

                for (var j = 0; j < v.Length; j++)
                {
                    vs[j, i] = v[j];
                }

                sigmas[i] = s;

                // remove the contribution of this pair and compute the rest
                remaining = remaining.Subtract(contrib);
            }

            return (U: us, S: sigmas, V: vs);
        }
    }
}
