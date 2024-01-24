using System;
using System.Linq;
using Algorithms.Search;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Utilities.Exceptions;

namespace Algorithms.Tests.Search;

public static class LinearSearcherTests
{
    [Test]
    public static void Find_ItemPresent_ItemCorrect([Random(0, 1_000_000, 100)] int n)
    {
        // Arrange
        var searcher = new LinearSearcher<int>();
        var random = Randomizer.CreateRandomizer();
        var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).ToArray();

        // Act
        var expectedItem = Array.Find(arrayToSearch, x => x == arrayToSearch[n / 2]);
        var actualItem = searcher.Find(arrayToSearch, x => x == arrayToSearch[n / 2]);

        // Assert
        Assert.That(actualItem, Is.EqualTo(expectedItem));
    }

    [Test]
    public static void FindIndex_ItemPresent_IndexCorrect([Random(0, 1_000_000, 100)] int n)
    {
        // Arrange
        var searcher = new LinearSearcher<int>();
        var random = Randomizer.CreateRandomizer();
        var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).ToArray();

        // Act
        var expectedIndex = Array.FindIndex(arrayToSearch, x => x == arrayToSearch[n / 2]);
        var actualIndex = searcher.FindIndex(arrayToSearch, x => x == arrayToSearch[n / 2]);

        // Assert
        Assert.That(actualIndex, Is.EqualTo(expectedIndex));
    }

    [Test]
    public static void Find_ItemMissing_ItemNotFoundExceptionThrown([Random(0, 1_000_000, 100)] int n)
    {
        // Arrange
        var searcher = new LinearSearcher<int>();
        var random = Randomizer.CreateRandomizer();
        var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).ToArray();

        // Act
        // Assert
        _ = Assert.Throws<ItemNotFoundException>(() => searcher.Find(arrayToSearch, _ => false));
    }

    [Test]
    public static void FindIndex_ItemMissing_MinusOneReturned([Random(0, 1_000_000, 100)] int n)
    {
        // Arrange
        var searcher = new LinearSearcher<int>();
        var random = Randomizer.CreateRandomizer();
        var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).ToArray();

        // Act
        var actualIndex = searcher.FindIndex(arrayToSearch, _ => false);

        // Assert
        Assert.That(actualIndex, Is.EqualTo(-1));
    }
}
