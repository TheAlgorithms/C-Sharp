using Algorithms.Search;
using Utilities.Exceptions;

namespace Algorithms.Tests.Search;

public static class FastSearcherTests
{
    [Test]
    public static void FindIndex_ItemPresent_IndexCorrect()
    {
        // Arrange
        var searcher = new FastSearcher();
        var arr = Helper.GetSortedArray(1000);
        var present = Helper.GetItemIn(arr);

        // Act
        var index = searcher.FindIndex(arr, present);

        // Assert
        arr[index].Should().Be(present);
    }

    [TestCase(new[] { 1, 2 }, 1)]
    [TestCase(new[] { 1, 2 }, 2)]
    [TestCase(new[] { 1, 2, 3, 3, 3 }, 2)]
    public static void FindIndex_ItemPresentInSpecificCase_IndexCorrect(int[] arr, int present)
    {
        // Arrange
        var searcher = new FastSearcher();

        // Act
        var index = searcher.FindIndex(arr, present);

        // Assert
        arr[index].Should().Be(present);
    }

    [Test]
    public static void FindIndex_ItemPresentInArrayOfDuplicates_IndexCorrect()
    {
        // Arrange
        var searcher = new FastSearcher();
        var arr = CreateArrayOfDuplicates(1000, 0); // Helper for large duplicate arrays
        var present = 0;

        // Act
        var index = searcher.FindIndex(arr, present);

        // Assert
        arr[index].Should().Be(0);
    }

    [TestCase(new int[0], 2)] // Empty array
    [TestCase(new[] { 1, 2, 3 }, 4)] // Item missing in array
    public static void FindIndex_ItemMissing_ItemNotFoundExceptionThrown(int[] arr, int missing)
    {
        // Arrange
        var searcher = new FastSearcher();

        // Act
        Action act = () => searcher.FindIndex(arr, missing);

        // Assert
        act.Should().Throw<ItemNotFoundException>();
    }

    [Test]
    public static void FindIndex_ItemMissingInArrayOfDuplicates_ItemNotFoundExceptionThrown()
    {
        // Arrange
        var searcher = new FastSearcher();
        var arr = CreateArrayOfDuplicates(1000, 0); // Helper for large duplicate arrays
        var missing = 1;

        // Act
        Action act = () => searcher.FindIndex(arr, missing);

        // Assert
        act.Should().Throw<ItemNotFoundException>();
    }

    [Test]
    public static void FindIndex_ItemOutOfRange_ItemNotFoundExceptionThrown()
    {
        // Arrange
        var searcher = new FastSearcher();
        var arr = Helper.GetSortedArray(1000);
        var smaller = Helper.GetItemSmallerThanAllIn(arr);
        var bigger = Helper.GetItemBiggerThanAllIn(arr);

        // Act & Assert
        Action act1 = () => searcher.FindIndex(arr, smaller);
        Action act2 = () => searcher.FindIndex(arr, bigger);

        act1.Should().Throw<ItemNotFoundException>();
        act2.Should().Throw<ItemNotFoundException>();
    }

    private static int[] CreateArrayOfDuplicates(int length, int value)
    {
        var arr = new int[length];
        Array.Fill(arr, value);
        return arr;
    }
}
