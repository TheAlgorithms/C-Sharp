using System;

namespace Algorithms.Numeric
{
    public static class CombinationsCalculation
    {
        /// <summary>
        /// Calculates the number of possible arrangements by selecting only a few objects from a set with no repetition.
        /// </summary>
        /// <param name="n">The total number of elements in a set.</param>
        /// <param name="k">The number of selected objects.</param>
        /// <returns>The number of combinations that k objects can be selected from a set of size n.</returns>
        /// <exception cref="ArgumentException">If n&lt;0, or k&lt;0 or n&lt;n.</exception>
        /// <remarks>We can use the factorial calculation for the calculation of the combinations, but this method will work
        /// for a relatively small value of n before the factorial exceeds the <see cref="long.MaxValue"/>. This implementation uses the following observations.
        /// We know that nC(k+1) = (n - k) * nCk / (k + 1).
        /// We also know that nCk for k=1 is nCk = n.
        /// Thus we can compute the next term with the formula: nC(k+1) = (n - k) * nCk / k + 1.
        /// </remarks>
        public static long Calculate(int n, int k)
        {
            if(n < 0)
            {
                throw new ArgumentException($"Expected n to be non-negative. Actual n value: {n}");
            }

            if (k < 0)
            {
                throw new ArgumentException($"Expected k to be non-negative. Actual k value: {k}");
            }

            if (n < k)
            {
                throw new ArgumentException($"Expected n to be greater than or equal to k. Actual values n:{n}, k:{k}");
            }

            long combinations = 1;

            for(var i = 0; i < k; i++)
            {
                var next = (n - i) * combinations / (i + 1);
                combinations = next;
            }

            return combinations;
        }
    }
}
