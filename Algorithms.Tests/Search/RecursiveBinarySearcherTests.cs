using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Search;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Search;

public static class RecursiveBinarySearcherTests
{
    [Test]
    public static void FindIndex_ItemPresent_IndexCorrect([Random(1, 1000, 100)] int n)
    {
        // Arrange
        var subject = new RecursiveBinarySearcher<int>();
        var randomizer = Randomizer.CreateRandomizer();
        var selectedIndex = randomizer.Next(0, n);
        var collection = Enumerable.Range(0, n).Select(_ => randomizer.Next(0, 1000)).OrderBy(x => x).ToList();

        // Act
        var actualIndex = subject.FindIndex(collection, collection[selectedIndex]);

        // Assert
        Assert.That(collection[actualIndex], Is.EqualTo(collection[selectedIndex]));
    }

    [Test]
    public static void FindIndex_ItemMissing_MinusOneReturned(
        [Random(0, 1000, 10)] int n,
        [Random(-100, 1100, 10)] int missingItem)
    {
        // Arrange
        var subject = new RecursiveBinarySearcher<int>();
        var random = Randomizer.CreateRandomizer();
        var collection = Enumerable.Range(0, n)
            .Select(_ => random.Next(0, 1000))
            .Where(x => x != missingItem)
            .OrderBy(x => x).ToList();

        // Act
        var actualIndex = subject.FindIndex(collection, missingItem);

        // Assert
        Assert.That(actualIndex, Is.EqualTo(-1));
    }

    [Test]
    public static void FindIndex_ArrayEmpty_MinusOneReturned([Random(100)] int itemToSearch)
    {
        // Arrange
        var subject = new RecursiveBinarySearcher<int>();
        var collection = new int[0];

        // Act
        var actualIndex = subject.FindIndex(collection, itemToSearch);

        // Assert
        Assert.That(actualIndex, Is.EqualTo(-1));
    }

    [Test]
    public static void FindIndex_NullCollection_Throws()
    {
        // Arrange
        var subject = new RecursiveBinarySearcher<int>();
        var collection = (IList<int>?)null;

        // Act
        Action act = () => subject.FindIndex(collection, 42);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}
