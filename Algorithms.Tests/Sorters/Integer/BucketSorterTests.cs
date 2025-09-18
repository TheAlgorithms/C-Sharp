using Algorithms.Sorters.Integer;
using Algorithms.Tests.Helpers;

namespace Algorithms.Tests.Sorters.Integer;

public static class BucketSorterTests
{
    [Test]
    public static void ArraySorted(
        [Random(0, 1000, 1000, Distinct = true)]
        int n)
    {
        // Arrange
        var sorter = new BucketSorter();
        var (correctArray, testArray) = RandomHelper.GetArrays(n);

        // Act
        sorter.Sort(testArray);
        Array.Sort(correctArray);

        // Assert
        Assert.That(testArray, Is.EqualTo(correctArray));
    }
}
