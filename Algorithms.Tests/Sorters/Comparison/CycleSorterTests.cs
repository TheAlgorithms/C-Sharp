using Algorithms.Sorters.Comparison;
using Algorithms.Tests.Helpers;

namespace Algorithms.Tests.Sorters.Comparison;

public static class CycleSorterTests
{
    [Test]
    public static void ArraySorted(
        [Random(0, 1000, 100, Distinct = true)]
        int n)
    {
        // Arrange
        var sorter = new CycleSorter<int>();
        var intComparer = new IntComparer();
        var (correctArray, testArray) = RandomHelper.GetArrays(n);

        // Act
        sorter.Sort(testArray, intComparer);
        Array.Sort(correctArray);

        // Assert
        Assert.That(testArray, Is.EqualTo(correctArray));
    }
}
