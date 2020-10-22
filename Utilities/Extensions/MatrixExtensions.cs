using System;

namespace Utilities.Extensions
{
    public static class MatrixExtensions
    {
        public static double[,] Multiply(this Array source, Array operand)
        {
            if ((source.Rank != 2) || (operand.Rank != 2))
            {
                throw new ArgumentException("Rank of both operands should be equal 2!");
            }

            if (source.GetLength(1) != operand.GetLength(0))
            {
                throw new InvalidOperationException("Width of a first operand should match height of a second!");
            }

            double[,] result = new double[source.GetLength(0), operand.GetLength(1)];

            for (var i = 0; i < result.GetLength(0); i++)
            {
                for (var j = 0; j < result.GetLength(1); j++)
                {
                    double elementProduct = 0;

                    for (var k = 0; k < source.GetLength(1); k++)
                    {
                        elementProduct += (double)source.GetValue(i, k) * (double)operand.GetValue(k, j);
                    }

                    result[i, j] = elementProduct;
                }
            }

            return result;
        }

        /// <summary>
        /// Makes a copy of a matrix. Changes to the copy should not affect the original.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>A copy of the matrix.</returns>
        public static double[,] Copy(double[,] matrix)
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
        /// Transposes a matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The transposed matrix.</returns>
        public static double[,] Transpose(double[,] matrix)
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
        public static double[] MultiplyVector(double[,] matrix, double[] vector)
        {
            double[,] vectorReshaped = new double[vector.Length, 1];
            for (int i = 0; i < vector.Length; i++)
            {
                vectorReshaped[i, 0] = vector[i];
            }

            double[,] resultMatrix = MultiplyGeneral(matrix, vectorReshaped);
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
        public static double[,] MultiplyGeneral(double[,] lhs, double[,] rhs)
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
        public static double[,] Subtract(double[,] lhs, double[,] rhs)
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

    }
}