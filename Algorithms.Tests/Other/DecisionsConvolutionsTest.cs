using Algorithms.Other;
using NUnit.Framework;
using System.Collections.Generic;

namespace Algorithms.Tests.Other;

public static class DecisionsConvolutionsTest
{
    [Test]
    public static void Verify_Linear_Convolution()
    {
        // Arrange
        var matrix = new List<List<decimal>>
        {
            new List<decimal> { 7, 6, 5, 8, 5, 6 },
            new List<decimal> { 4, 8, 4, 4, 5, 3 },
            new List<decimal> { 3, 8, 1, 4, 5, 2 },
            new List<decimal> { 5, 6, 3, 6, 4, 5 },
            new List<decimal> { 1, 4, 8, 6, 3, 6 },
            new List<decimal> { 5, 1, 8, 6, 5, 1 },
            new List<decimal> { 6, 8, 3, 6, 3, 5 }
        };

        var expectedMatrix = new List<decimal> { 7, 6, 5, 8, 5, 6 };

        var priorities = new List<decimal> { 1, 1, 1, 1, 0.545m, 0.583m };

        // Act
        var optimizedMatrix = DecisionsConvolutions.Linear(matrix, priorities);

        // Assert
        Assert.That(expectedMatrix, Is.EqualTo(optimizedMatrix));
    }

    [Test]
    public static void Verify_MaxMin_Convolution()
    {
        // Arrange
        var matrix = new List<List<decimal>>
        {
            new List<decimal> { 7, 6, 5, 8, 5, 6 },
            new List<decimal> { 4, 8, 4, 4, 5, 3 },
            new List<decimal> { 3, 8, 1, 4, 5, 2 },
            new List<decimal> { 5, 6, 3, 6, 4, 5 },
            new List<decimal> { 1, 4, 8, 6, 3, 6 },
            new List<decimal> { 5, 1, 8, 6, 5, 1 },
            new List<decimal> { 6, 8, 3, 6, 3, 5 }
        };

        var expectedMatrix = new List<decimal> { 7, 6, 5, 8, 5, 6 };

        var priorities = new List<decimal> { 1, 1, 1, 1, 0.545m, 0.583m };

        // Act
        var optimizedMatrix = DecisionsConvolutions.MaxMin(matrix, priorities);

        // Assert
        Assert.That(expectedMatrix, Is.EqualTo(optimizedMatrix));
    }
}
