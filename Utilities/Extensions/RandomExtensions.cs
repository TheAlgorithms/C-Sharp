using System;
using System.Linq;

namespace Utilities.Extensions
{
    public static class RandomExtensions
    {
        public static double[] NextVector(this Random rand, int size)
        {
            var vector = Enumerable.Range(0, size)
                .Select(_ => rand.NextDouble()).ToArray();
            var norm = vector.Magnitude();
            return vector.Select(x => x / norm).ToArray();
        }
    }
}
