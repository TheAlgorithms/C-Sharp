using System;
using System.Linq;

namespace Utilities.Extensions
{
    public static class MatrixExtensions
    {
        public static double[,] Multiply(this double[,] source, double[,] operand)
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
                        elementProduct += source[i, k] * operand[k, j];
                    }

                    result[i, j] = elementProduct;
                }
            }

            return result;
        }

        public static double Norm(this double[] source) => Math.Sqrt(source.Sum(x => x * x));
    }
}