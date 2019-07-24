using System;

namespace Algorithms.Numeric.GreatestCommonDivisor
{
    /// <summary>
    /// TODO.
    /// </summary>
    public class BinaryGreatestCommonDivisorFinder : IGreatestCommonDivisorFinder
    {
        /// <summary>
        /// Finds greatest common divisor for numbers u and v
        /// using binary algorithm.
        /// Wiki: https://en.wikipedia.org/wiki/Binary_GCD_algorithm.
        /// </summary>
        /// <param name="u">TODO.</param>
        /// <param name="v">TODO. 2.</param>
        /// <returns>Greatest common divisor.</returns>
        public int Find(int u, int v)
        {
            // GCD(0, 0) = 0
            if (u == 0 && v == 0)
            {
                return int.MaxValue;
            }

            // GCD(0, v) = v; GCD(u, 0) = u
            if (u == 0 || v == 0)
            {
                return u + v;
            }

            // GCD(-a, -b) = GCD(-a, b) = GCD(a, -b) = GCD(a, b)
            u = Math.Sign(u) * u;
            v = Math.Sign(v) * v;

            // Let shift := lg K, where K is the greatest power of 2 dividing both u and v
            var shift = 0;
            while (((u | v) & 1) == 0)
            {
                u >>= 1;
                v >>= 1;
                shift++;
            }

            while ((u & 1) == 0)
            {
                u >>= 1;
            }

            // From here on, u is always odd
            do
            {
                // Remove all factors of 2 in v as they are not common
                // v is not zero, so while will terminate
                while ((v & 1) == 0)
                {
                    v >>= 1;
                }

                // Now u and v are both odd. Swap if necessary so u <= v,
                if (u > v)
                {
                    var t = v;
                    v = u;
                    u = t;
                }

                // Here v >= u and v - u is even
                v -= u;
            }
            while (v != 0);

            // Restore common factors of 2
            return u << shift;
        }
    }
}
