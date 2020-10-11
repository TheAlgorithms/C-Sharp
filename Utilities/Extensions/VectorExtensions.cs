using System;

namespace Utilities.Extensions
{
    public static class VectorExtensions
    {
        public static double Dot(this double[] source, double[] operand)
        {
            if (source.Length != operand.Length)
            {
                throw new InvalidOperationException("The width of a first operand should match the height of a second.");
            }

            double dotProduct = 0;

            for (var i = 0; i < source.Length; i++)
            {
                dotProduct += source[i] * operand[i];
            }

            return dotProduct;
        }

        public static double Norm(this double[] source) => Math.Sqrt(source.Dot(source));

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
