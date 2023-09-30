using System.Collections.Generic;
using Algorithms.Other;
using NUnit.Framework;

namespace Algorithms.Tests.Other
{
    public static class DecisionsConvolutionsTest
    {
        [Test]
        public static void Verify_Linear_Convolution()
        {
            // Arrange
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

            var expectedMatrix = new List<decimal> { 7, 6, 5, 8, 5, 6 };

            var priorities = new List<decimal> { 1, 1, 1, 1, 0.545m, 0.583m };

            // Act
            var optimizedMatrix = DecisionsConvolutions.Linear(matrix, priorities);

            // Assert
            Assert.AreEqual(optimizedMatrix, expectedMatrix);
        }

        [Test]
        public static void Verify_MaxMin_Convolution()
        {
            // Arrange
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

            var expectedMatrix = new List<decimal> { 7, 6, 5, 8, 5, 6 };

            var priorities = new List<decimal> { 1, 1, 1, 1, 0.545m, 0.583m };

            // Act
            var optimizedMatrix = DecisionsConvolutions.MaxMin(matrix, priorities);

            // Assert
            Assert.AreEqual(optimizedMatrix, expectedMatrix);
        }
    }
}
