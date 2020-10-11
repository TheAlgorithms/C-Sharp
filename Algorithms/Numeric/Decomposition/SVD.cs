using System;

namespace Algorithms.Numeric.Decomposition
{
    /// <summary>
    /// Singular Vector Decomposition decomposes any general matrix into its
    /// singular values and a set of orthonormal bases.
    /// </summary>
    public static class SVD
    {
        /// <summary>
        /// Makes a copy of a matrix. Changes to the copy should not affect the original.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>A copy of the matrix.</returns>
        public static double[,] MatrixCopy(double[,] matrix)
        {
            double[,] result = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result[i, j] = matrix[i, j];
                }
            }

            return result;
        }

        /// <summary>
        /// Makes a copy of a vector. Changes to the copy should not affect the original.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The copy.</returns>
        public static double[] VectorCopy(double[] vector)
        {
            double[] result = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                result[i] = vector[i];
            }

            return result;
        }

        /// <summary>
        /// Transposes a matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The transposed matrix.</returns>
        public static double[,] MatrixTranspose(double[,] matrix)
        {
            double[,] result = new double[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }

        /// <summary>
        /// Multiplies a matrix by a vector.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="vector">The vector.</param>
        /// <returns>The product of the matrix and the vector, which is a vector.</returns>
        /// <exception cref="ArgumentException">Dimensions of matrix and vector do not match.</exception>
        public static double[] MatrixTimesVector(double[,] matrix, double[] vector)
        {
            double[,] vectorReshaped = new double[vector.Length, 1];
            for (int i = 0; i < vector.Length; i++)
            {
                vectorReshaped[i, 0] = vector[i];
            }

            double[,] resultMatrix = MatrixMultiply(matrix, vectorReshaped);
            double[] result = new double[resultMatrix.GetLength(0)];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = resultMatrix[i, 0];
            }

            return result;
        }

        /// <summary>
        /// Performs matrix multiplication.
        /// </summary>
        /// <param name="lhs">The LHS matrix.</param>
        /// <param name="rhs">The RHS matrix.</param>
        /// <returns>The product of the two matrices.</returns>
        /// <exception cref="ArgumentException">Dimensions of matrices do not match.</exception>
        public static double[,] MatrixMultiply(double[,] lhs, double[,] rhs)
        {
            if (lhs.GetLength(1) != rhs.GetLength(0))
            {
                throw new ArgumentException("Number of columns in LHS must be same as number of rows in RHS");
            }

            double[,] result = new double[lhs.GetLength(0), rhs.GetLength(1)];
            for (int i = 0; i < lhs.GetLength(0); i++)
            {
                for (int j = 0; j < rhs.GetLength(1); j++)
                {
                    for (int k = 0; k < lhs.GetLength(1); k++)
                    {
                        result[i, j] += lhs[i, k] * rhs[k, j];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Performs matrix subtraction.
        /// </summary>
        /// <param name="lhs">The LHS matrix.</param>
        /// <param name="rhs">The RHS matrix.</param>
        /// <returns>The difference of the two matrices.</returns>
        /// <exception cref="ArgumentException">Dimensions of matrices do not match.</exception>
        public static double[,] MatrixSubtract(double[,] lhs, double[,] rhs)
        {
            if (lhs.GetLength(0) != rhs.GetLength(0) ||
                lhs.GetLength(1) != rhs.GetLength(1))
            {
                throw new ArgumentException("Dimensions of matrices must be the same");
            }

            double[,] result = new double[lhs.GetLength(0), lhs.GetLength(1)];
            for (int i = 0; i < lhs.GetLength(0); i++)
            {
                for (int j = 0; j < lhs.GetLength(1); j++)
                {
                    result[i, j] = lhs[i, j] - rhs[i, j];
                }
            }

            return result;
        }

        /// <summary>
        /// Computes the outer product of two vectors.
        /// </summary>
        /// <param name="lhs">The LHS vector.</param>
        /// <param name="rhs">The RHS vector.</param>
        /// <returns>The outer product of the two vector.</returns>
        public static double[,] VectorOuterProduct(double[] lhs, double[] rhs)
        {
            double[,] result = new double[lhs.Length, rhs.Length];
            for (int i = 0; i < lhs.Length; i++)
            {
                for (int j = 0; j < rhs.Length; j++)
                {
                    result[i, j] = lhs[i] * rhs[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Computes the dot product of two vectors.
        /// </summary>
        /// <param name="lhs">The LHS vector.</param>
        /// <param name="rhs">The RHS vector.</param>
        /// <returns>The dot product of the two vector.</returns>
        /// <exception cref="ArgumentException">Dimensions of vectors do not match.</exception>
        public static double VectorDot(double[] lhs, double[] rhs)
        {
            if (lhs.Length != rhs.Length)
            {
                throw new ArgumentException("Dot product arguments must have same dimension");
            }

            double result = 0;
            for (int i = 0; i < lhs.Length; i++)
            {
                result += lhs[i] * rhs[i];
            }

            return result;
        }

        /// <summary>
        /// Computes the magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The magnitude.</returns>
        public static double Magnitude(double[] vector)
        {
            double magnitude = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                magnitude += vector[i] * vector[i];
            }

            magnitude = Math.Sqrt(magnitude);
            return magnitude;
        }

        /// <summary>
        /// Returns the unit vector in the direction of a given vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="epsilon">Error margin, if vector magnitude is below this value
        /// then do not do anything to the vector, to prevent numerical errors.</param>
        /// <returns>The unit vector.</returns>
        public static double[] Normalize(double[] vector, double epsilon = 1E-5)
        {
            double magnitude = Magnitude(vector);

            if (magnitude < epsilon)
            {
                return VectorCopy(vector);
            }

            double[] result = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                result[i] = vector[i] / magnitude;
            }

            return result;
        }

        /// <summary>
        /// Computes a random unit vector.
        /// </summary>
        /// <param name="dimensions">The dimensions of the required vector.</param>
        /// <returns>The unit vector.</returns>
        public static double[] RandomUnitVector(int dimensions)
        {
            Random random = new Random();
            double[] result = new double[dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                result[i] = 2 * random.NextDouble() - 1;
            }

            result = Normalize(result);
            return result;
        }

        /// <summary>
        /// Computes a single singular vector for the given matrix, corresponding to the largest singular value.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="epsilon">The error margin.</param>
        /// <param name="max_iterations">The maximum number of iterations.</param>
        /// <returns>A singular vector, with dimension equal to number of columns of the matrix.</returns>
        public static double[] Decompose1D(double[,] matrix, double epsilon = 1E-5, int max_iterations = 100)
        {
            int n = matrix.GetLength(1);
            int iterations = 0;
            double[] lastIteration;
            double[] currIteration = RandomUnitVector(n);
            double[,] b = MatrixMultiply(MatrixTranspose(matrix), matrix);
            do
            {
                lastIteration = VectorCopy(currIteration);
                currIteration = MatrixTimesVector(b, lastIteration);
                currIteration = Normalize(currIteration, epsilon);
                iterations++;
            }
            while (VectorDot(lastIteration, currIteration) < 1 - epsilon && iterations < max_iterations);

            return currIteration;
        }

        /// <summary>
        /// Computes the SVD for the given matrix, with singular values arranged from greatest to least.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="epsilon">The error margin.</param>
        /// <param name="max_iterations">The maximum number of iterations.</param>
        /// <returns>The SVD.</returns>
        public static (double[,] U, double[] S, double[,] V) Decompose(double[,] matrix, double epsilon = 1E-5, int max_iterations = 100)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            int numValues = Math.Min(m, n);

            // sigmas is be a diagonal matrix, hence only a vector is needed
            double[] sigmas = new double[numValues];
            double[,] us = new double[m, numValues];
            double[,] vs = new double[n, numValues];

            // keep track of progress
            double[,] remaining = MatrixCopy(matrix);

            // for each singular value
            for (int i = 0; i < numValues; i++)
            {
                // compute the v singular vector
                double[] v = Decompose1D(remaining, epsilon, max_iterations);
                double[] u = MatrixTimesVector(matrix, v);

                // compute the contribution of this pair of singular vectors
                double[,] contrib = VectorOuterProduct(u, v);

                // extract the singular value
                double s = Magnitude(u);

                // v and u should be unit vectors
                u = Normalize(u);

                // save u, v and s into the result
                for (int j = 0; j < u.Length; j++)
                {
                    us[j, i] = u[j];
                }

                for (int j = 0; j < v.Length; j++)
                {
                    vs[j, i] = v[j];
                }

                sigmas[i] = s;

                // remove the contribution of this pair and compute the rest
                remaining = MatrixSubtract(remaining, contrib);
            }

            return (U: us, S: sigmas, V: vs);
        }
    }
}
