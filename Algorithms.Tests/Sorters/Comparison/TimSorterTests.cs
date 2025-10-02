using Algorithms.Sorters.Comparison;
using Algorithms.Tests.Helpers;

namespace Algorithms.Tests.Sorters.Comparison;

public static class TimSorterTests
{
    private static readonly IntComparer IntComparer = new();
    private static readonly TimSorterSettings Settings = new();

    [Test]
    public static void Sort_ShouldBeEquivalentToSuccessfulBasicSort(
        [Random(0, 10_000, 5000)] int n)
    {
        // Arrange
        var sorter = new TimSorter<int>(Settings, IntComparer);
        var (correctArray, testArray) = RandomHelper.GetArrays(n);

        // Act
        sorter.Sort(testArray, IntComparer);
        Array.Sort(correctArray, IntComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(testArray));
    }

    [Test]
    public static void Sort_TinyArray_ShouldSortCorrectly()
    {
        // Arrange
        var sorter = new TimSorter<int>(Settings, IntComparer);
        var tinyArray = new[] { 1 };
        var correctArray = new[] { 1 };

        // Act
        sorter.Sort(tinyArray, IntComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(tinyArray));
    }

    [Test]
    public static void Sort_SmallChunks_ShouldSortCorrectly()
    {
        // Arrange
        var sorter = new TimSorter<int>(Settings, IntComparer);
        var (correctArray, testArray) = RandomHelper.GetArrays(800);
        Array.Sort(correctArray, IntComparer);
        Array.Sort(testArray, IntComparer);

        var max = testArray.Max();
        var min = testArray.Min();

        correctArray[0] = max;
        correctArray[800 - 1] = min;
        testArray[0] = max;
        testArray[800 - 1] = min;

        // Act
        sorter.Sort(testArray, IntComparer);
        Array.Sort(correctArray, IntComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(testArray));
    }

    [Test]
    public static void Sort_ThrowsArgumentNullException_WhenArrayIsNull()
    {
        // Arrange
        var sorter = new TimSorter<int>(Settings, IntComparer);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sorter.Sort(null!, IntComparer));
    }

    [Test]
    public static void Sort_UsesDefaultComparer_WhenComparerIsNull()
    {
        // Arrange
        var sorter = new TimSorter<int>(Settings, null!);
        var (correctArray, testArray) = RandomHelper.GetArrays(20);

        // Act
        sorter.Sort(testArray, IntComparer);
        Array.Sort(correctArray, IntComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(testArray));
    }

    [Test]
    public static void Sort_AlreadySortedArray_RemainsUnchanged()
    {
        // Arrange
        var sorter = new TimSorter<int>(Settings, IntComparer);
        var array = new[] { 1, 2, 3, 4, 5 };
        var expected = new[] { 1, 2, 3, 4, 5 };

        // Act
        sorter.Sort(array, IntComparer);

        // Assert
        Assert.That(array, Is.EqualTo(expected));
    }

    [Test]
    public static void MergeAt_ShouldReturnEarly_WhenLenAIsZero()
    {
        // Arrange: left run is all less than right run's first element
        var array = Enumerable.Range(1, 25).Concat(Enumerable.Range(100, 25)).ToArray();
        var sortedArray = Enumerable.Range(1, 25).Concat(Enumerable.Range(100, 25)).ToArray();
        var sorter = new TimSorter<int>(new TimSorterSettings(), Comparer<int>.Default);

        // Act
        sorter.Sort(array, Comparer<int>.Default);

        // Assert: Array order will not have changed, and the lenA <= 0 branch should be hit
        Assert.That(sortedArray, Is.EqualTo(array));
    }

    [Test]
    public static void MergeAt_ShouldReturnEarly_WhenLenBIsZero()
    {
        // Arrange: right run is all less than left run's last element
        var array = Enumerable.Range(100, 25).Concat(Enumerable.Range(1, 25)).ToArray();
        var sortedArray = Enumerable.Range(1, 25).Concat(Enumerable.Range(100, 25)).ToArray();
        var sorter = new TimSorter<int>(new TimSorterSettings(), Comparer<int>.Default);

        // Act
        sorter.Sort(array, Comparer<int>.Default);

        // Assert: The left and right sides of the array should have swapped places, and the lenB <= 0 branch should be hit
        Assert.That(sortedArray, Is.EqualTo(array));
    }
}
