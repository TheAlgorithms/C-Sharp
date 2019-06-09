using System;

namespace Algorithms.Numeric
{
    public static class GaussianElimination
    {
        private static int RowCount { get; set; }

        /// <summary>
        ///  Method to find a linear equation system using gaussian elimination.
        /// </summary>
        /// <param name="matrix">The key matrix to solve via algorithm.</param>
        /// <returns>whether the input matrix has a unique solution or not.</returns>
        public static bool Solve(double[,] matrix)
        {
            RowCount = matrix.GetUpperBound(0) + 1;

            if (!CanMatrixBeUsed(matrix))
            {
                throw new ArgumentException("Please use a n*(n+1) matrix with Length > 0.");
            }

            var pivot = PivotMatrix(ref matrix);
            if (!pivot)
            {
                return false;
            }

            Elimination(ref matrix);

            return BackInsertion(ref matrix);
        }

        private static bool CanMatrixBeUsed(double[,] matrix)
        {
            if (matrix == null || matrix.Length != RowCount * (RowCount + 1))
            {
                return false;
            }

            return RowCount > 1;
        }

        private static bool PivotMatrix(ref double[,] matrix)
        {
            for (int col = 0; col + 1 < RowCount; col++)
            {
                if (matrix[col, col] == 0)
                {
                    // To find a non-zero coefficient
                    int rowToSwap = col + 1;
                    for (; rowToSwap < RowCount; rowToSwap++)
                    {
                        if (matrix[rowToSwap, col] != 0)
                        {
                            break;
                        }
                    }

                    if (matrix[rowToSwap, col] != 0)
                    {
                        var tmp = new double[RowCount + 1];
                        for (int i = 0; i < RowCount; i++)
                        {
                            // To make the swap with the element above.
                            tmp[i] = matrix[rowToSwap, i];
                            matrix[rowToSwap, i] = matrix[col, i];
                            matrix[col, i] = tmp[i];
                        }
                    }
                    else
                    {
                        // To return that the matrix doesn't have a unique solution.
                        return false;
                    }
                }
            }

            return true;
        }

        private static void Elimination(ref double[,] matrix)
        {
            for (int srcRow = 0; srcRow + 1 < RowCount; srcRow++)
            {
                for (int destRow = srcRow + 1; destRow < RowCount; destRow++)
                {
                    var df = matrix[srcRow, srcRow];
                    var sf = matrix[destRow, destRow];

                    for (int i = 0; i < RowCount; i++)
                    {
                        matrix[destRow, i] = matrix[destRow, i] * df - matrix[srcRow, i] * sf;
                    }
                }
            }
        }

        private static bool BackInsertion(ref double[,] matrix)
        {
            for (int row = RowCount - 1; row >= 0; row--)
            {
                var element = matrix[row, row];
                if (element == 0)
                {
                    return false;
                }

                for (int i = 0; i < RowCount; i++)
                {
                    matrix[row, i] /= element;
                }

                for (int destRow = 0; destRow < row; destRow++)
                {
                    matrix[destRow, RowCount] -= matrix[destRow, row] * matrix[row, RowCount];
                    matrix[destRow, row] = 0;
                }
            }

            return true;
        }
    }
}