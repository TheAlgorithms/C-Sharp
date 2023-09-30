using System.Collections.Generic;
using Algorithms.Other;
using NUnit.Framework;

namespace Algorithms.Tests.Other
{
    public static class ParetoOptimizationTests
    {
        [Test]
        public static void Verify_Pareto_Optimization()
        {
            // Arrange
            var paretoOptimization = new ParetoOptimization();

            var matrix = new List<List<decimal>>
            {
                new() { 7, 6, 5, 8, 5, 6 },
                new() { 4, 8, 4, 4, 5, 3 },
                new() { 3, 8, 1, 4, 5, 2 },
                new() { 5, 6, 3, 6, 4, 5 },
                new() { 1, 4, 8, 6, 3, 6 },
                new() { 5, 1, 8, 6, 5, 1 },
                new() { 6, 8, 3, 6, 3, 5 }
            };

            var expectedMatrix = new List<List<decimal>>
            {
                new() { 7, 6, 5, 8, 5, 6 },
                new() { 4, 8, 4, 4, 5, 3 },
                new() { 1, 4, 8, 6, 3, 6 },
                new() { 5, 1, 8, 6, 5, 1 },
                new() { 6, 8, 3, 6, 3, 5 }
            };

            // Act
            var optimizedMatrix = paretoOptimization.Optimize(matrix);

            // Assert
            Assert.AreEqual(optimizedMatrix, expectedMatrix);
        }
    }
}
