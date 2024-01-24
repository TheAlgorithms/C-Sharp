using Algorithms.Search;
using NUnit.Framework;
using System;
using System.Linq;
using FluentAssertions;

namespace Algorithms.Tests.Search;

public class JumpSearcherTests
{
    [Test]
    public void FindIndex_ItemPresent_ItemCorrect([Random(1, 1000, 100)] int n)
    {
        // Arrange
        var searcher = new JumpSearcher<int>();
        var sortedArray = Enumerable.Range(0, n).Select(_ => TestContext.CurrentContext.Random.Next(1_000_000)).OrderBy(x => x).ToArray();
        var expectedIndex = TestContext.CurrentContext.Random.Next(sortedArray.Length);

        // Act
        var actualIndex = searcher.FindIndex(sortedArray, sortedArray[expectedIndex]);

        // Assert
        sortedArray[actualIndex].Should().Be(sortedArray[expectedIndex]);
    }

    [Test]
    public void FindIndex_ItemMissing_MinusOneReturned([Random(1, 1000, 10)] int n, [Random(-100, 1100, 10)] int missingItem)
    {
        // Arrange
        var searcher = new JumpSearcher<int>();
        var sortedArray = Enumerable.Range(0, n).Select(_ => TestContext.CurrentContext.Random.Next(1_000_000)).Where(x => x != missingItem).OrderBy(x => x).ToArray();
        var expectedIndex = -1;

        // Act
        var actualIndex = searcher.FindIndex(sortedArray, missingItem);

        // Assert
        Assert.That(actualIndex, Is.EqualTo(expectedIndex));
    }

    [Test]
    public void FindIndex_ArrayEmpty_MinusOneReturned([Random(-100, 1100, 10)] int missingItem)
    {
        // Arrange
        var searcher = new JumpSearcher<int>();
        var sortedArray = Array.Empty<int>();
        var expectedIndex = -1;

        // Act
        var actualIndex = searcher.FindIndex(sortedArray, missingItem);

        // Assert
        Assert.That(actualIndex, Is.EqualTo(expectedIndex));
    }

    [TestCase(null, "abc")]
    [TestCase(new[] { "abc", "def", "ghi" }, null)]
    [TestCase(null, null)]
    public void FindIndex_ArrayNull_ItemNull_ArgumentNullExceptionThrown(string[] sortedArray, string searchItem)
    {
        // Arrange
        var searcher = new JumpSearcher<string>();

        // Act, Assert
        _ = Assert.Throws<ArgumentNullException>(() => searcher.FindIndex(sortedArray, searchItem));
    }
}
