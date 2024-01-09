using System;
using System.Linq;

namespace Utilities.Extensions;

public static class RandomExtensions
{
    /// <summary>
    ///     Returns a random normalized vector of the specified size.
    /// </summary>
    /// <param name="rand">The random number generator.</param>
    /// <param name="size">The size of the vector to return.</param>
    /// <returns>A random normalized vector.</returns>
    public static double[] NextVector(this Random rand, int size)
    {
        var vector = Enumerable.Range(0, size)
            .Select(_ => rand.NextDouble()).ToArray();
        var norm = vector.Magnitude();
        return vector.Select(x => x / norm).ToArray();
    }
}
