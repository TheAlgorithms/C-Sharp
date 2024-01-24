using System;
using System.Linq;
using Algorithms.Sorters.Comparison;
using Algorithms.Tests.Helpers;
using NUnit.Framework;

namespace Algorithms.Tests.Sorters.Comparison;

public static class TimSorterTests
{
    private static readonly IntComparer IntComparer = new();

    [Test]
    public static void ArraySorted(
        [Random(0, 10_000, 2000)] int n)
    {
        // Arrange
        var sorter = new TimSorter<int>();
        var (correctArray, testArray) = RandomHelper.GetArrays(n);

        // Act
        sorter.Sort(testArray, IntComparer);
        Array.Sort(correctArray, IntComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(testArray));
    }

    [Test]
    public static void TinyArray()
    {
        // Arrange
        var sorter = new TimSorter<int>();
        var tinyArray = new[] { 1 };
        var correctArray = new[] { 1 };

        // Act
        sorter.Sort(tinyArray, IntComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(tinyArray));
    }

    [Test]
    public static void SmallChunks()
    {
        // Arrange
        var sorter = new TimSorter<int>();
        var (correctArray, testArray) = RandomHelper.GetArrays(800);
        Array.Sort(correctArray, IntComparer);
        Array.Sort(testArray, IntComparer);

        var max = testArray.Max();
        var min = testArray.Min();

        correctArray[0] = max;
        correctArray[800-1] = min;
        testArray[0] = max;
        testArray[800 - 1] = min;

        // Act
        sorter.Sort(testArray, IntComparer);
        Array.Sort(correctArray, IntComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(testArray));
    }
}
