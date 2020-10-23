using System;

namespace Utilities.Extensions
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Makes a vector of zeroes.
        /// </summary>
        /// <param name="dimensions">The number of dimensions of the vector.</param>
        /// <returns>The vector.</returns>
        public static double[] Zero(int dimensions)
        {
            var result = new double[dimensions];
            return result;
        }

        /// <summary>
        /// Makes a copy of a vector. Changes to the copy should not affect the original.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The copy.</returns>
        public static double[] Copy(double[] vector)
        {
            var result = new double[vector.Length];
            for (var i = 0; i < vector.Length; i++)
            {
                result[i] = vector[i];
            }

            return result;
        }

        /// <summary>
        /// Computes the outer product of two vectors.
        /// </summary>
        /// <param name="lhs">The LHS vector.</param>
        /// <param name="rhs">The RHS vector.</param>
        /// <returns>The outer product of the two vector.</returns>
        public static double[,] OuterProduct(double[] lhs, double[] rhs)
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
        /// Computes the dot product of two vectors.
        /// </summary>
        /// <param name="lhs">The LHS vector.</param>
        /// <param name="rhs">The RHS vector.</param>
        /// <returns>The dot product of the two vector.</returns>
        /// <exception cref="ArgumentException">Dimensions of vectors do not match.</exception>
        public static double Dot(double[] lhs, double[] rhs)
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
        /// Computes the magnitude of a vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The magnitude.</returns>
        public static double Magnitude(double[] vector)
        {
            var magnitude = Dot(vector, vector);
            magnitude = Math.Sqrt(magnitude);
            return magnitude;
        }

        /// <summary>
        /// Returns the scaled vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="factor">Scale factor.</param>
        /// <returns>The unit vector.</returns>
        public static double[] Scale(double[] vector, double factor)
        {
            var result = new double[vector.Length];
            for (var i = 0; i < vector.Length; i++)
            {
                result[i] = vector[i] * factor;
            }

            return result;
        }
        
        public static double[,] ToColumnVector(this double[] source)
        {
            var columnVector = new double[source.Length, 1];

            for (var i = 0; i < source.Length; i++)
            {
                columnVector[i, 0] = source[i];
            }

            return columnVector;
        }

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
    }
}
