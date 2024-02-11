using System;
using Algorithms.Sorters.Comparison;
using Algorithms.Tests.Helpers;
using NUnit.Framework;

namespace Algorithms.Tests.Sorters.Comparison;

/// <summary>
///     Class for testing merge sorter algorithm.
/// </summary>
public static class MergeSorterTests
{
    [Test]
    public static void TestOnMergeSorter(
        [Random(0, 1000, 100, Distinct = true)]
        int n)
    {
        // Arrange
        var sorter = new MergeSorter<int>();
        var intComparer = new IntComparer();
        var (correctArray, testArray) = RandomHelper.GetArrays(n);

        // Act
        sorter.Sort(testArray, intComparer);
        Array.Sort(correctArray);

        // Assert
        Assert.That(testArray, Is.EqualTo(correctArray));
    }
}
