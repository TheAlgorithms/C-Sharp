using System;

namespace Utilities.Extensions
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Makes a copy of a vector. Changes to the copy should not affect the original.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The copy.</returns>
        public static double[] Copy(double[] vector)
        {
            double[] result = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
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
        public static double Dot(double[] lhs, double[] rhs)
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
        /// <returns>The unit vector.</returns>
        public static double[] Normalize(double[] vector)
        {
            return Normalize(vector, 1E-5);
        }

        /// <summary>
        /// Returns the unit vector in the direction of a given vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="epsilon">Error margin, if vector magnitude is below this value
        /// then do not do anything to the vector, to prevent numerical errors.</param>
        /// <returns>The unit vector.</returns>
        public static double[] Normalize(double[] vector, double epsilon)
        {
            double magnitude = Magnitude(vector);

            if (magnitude < epsilon)
            {
                return Copy(vector);
            }

            double[] result = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                result[i] = vector[i] / magnitude;
            }

            return result;
        }
    }
}