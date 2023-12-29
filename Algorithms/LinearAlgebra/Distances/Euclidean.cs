using System;
using System.Linq;

namespace Algorithms.LinearAlgebra.Distances;

/// <summary>
/// Implementation for Euclidean distance.
/// </summary>
public static class Euclidean
{
    /// <summary>
    /// Calculate Euclidean distance for two N-Dimensional points.
    /// </summary>
    /// <param name="point1">First N-Dimensional point.</param>
    /// <param name="point2">Second N-Dimensional point.</param>
    /// <returns>Calculated Euclidean distance.</returns>
    public static double Distance(double[] point1, double[] point2)
    {
        if (point1.Length != point2.Length)
        {
            throw new ArgumentException("Both points should have the same dimensionality");
        }

        // distance = sqrt((x1-y1)^2 + (x2-y2)^2 + ... + (xn-yn)^2)
        return Math.Sqrt(point1.Zip(point2, (x1, x2) => (x1 - x2) * (x1 - x2)).Sum());
    }
}
