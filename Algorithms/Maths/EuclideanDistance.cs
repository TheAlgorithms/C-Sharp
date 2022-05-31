using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Maths
{
    public static class EuclideanDistance
    {
        /// <summary>
        /// Calculate the euclidean distance between two N-dimensional points.
        /// See https://en.wikipedia.org/wiki/Euclidean_distance.
        /// </summary>
        /// <param name="point1">First point or origin point.</param>
        /// <param name="point2">Second point or target point.</param>
        /// <returns>Computed euclidean distance.</returns>
        /// <exception cref="ArgumentException">
        /// Exception thrown when the points have different dimensions.
        /// </exception>
        public static double Distance(double[] point1, double[] point2)
        {
            if (point1.Length != point2.Length)
            {
                throw new ArgumentException("Both points should have the same length.");
            }

            // distance = sqrt( (p1[0] - p2[0])^2 + (p1[1] - p2[1])^2 + ... + (p1[N-1] - p2[N-1])^2 )
            return Math.Sqrt(point1.Zip(point2, (x1, x2) => Math.Pow(x1 - x2, 2)).Sum());
        }
    }
}
