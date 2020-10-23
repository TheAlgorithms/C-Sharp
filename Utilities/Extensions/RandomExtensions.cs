using System;
using System.Linq;
using static Utilities.Extensions.VectorExtensions;

namespace Utilities.Extensions
{
    public static class RandomExtensions
    {
        public static double[] NextVector(this Random rand, int size)
        {
            var vector = Enumerable.Range(0, size)
                .Select(_ => rand.NextDouble()).ToArray();
            var norm = Magnitude(vector);
            return vector.Select(x => x / norm).ToArray();
        }
    }
}