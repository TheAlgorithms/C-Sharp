using System.Collections.Generic;
using System.Linq;
using DataStructures.Bag;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests;

internal class BagTests
{
    [Test]
    public void Add_ShouldIncreaseCount()
    {
        // Arrange & Act
        var bag = new Bag<int>
        {
            1,
            2,
            1
        };

        // Assert
        bag.Count.Should().Be(3);
    }

    [Test]
    public void Add_ShouldHandleDuplicates()
    {
        // Arrange & Act
        var bag = new Bag<string>
        {
            "apple",
            "apple"
        };

        // Assert
        bag.Count.Should().Be(2);
        bag.Should().Contain("apple");
    }

    [Test]
    public void Clear_ShouldEmptyTheBag()
    {
        // Arrange
        var bag = new Bag<int>
        {
            1,
            2
        };

        // Act
        bag.Clear();

        // Assert
        bag.IsEmpty().Should().BeTrue();
        bag.Count.Should().Be(0);
    }

    [Test]
    public void IsEmpty_ShouldReturnTrueForEmptyBag()
    {
        // Arrange
        var bag = new Bag<int>();

        // Act & Assert
        bag.IsEmpty().Should().BeTrue();
    }

    [Test]
    public void IsEmpty_ShouldReturnFalseForNonEmptyBag()
    {
        // Arrange
        var bag = new Bag<int>
        {
            1
        };

        // Act & Assert
        bag.IsEmpty().Should().BeFalse();
    }

    [Test]
    public void GetEnumerator_ShouldIterateAllItems()
    {
        // Arrange
        var bag = new Bag<int>
        {
            1,
            2,
            1
        };

        // Act
        var items = bag.ToList();

        // Assert
        items.Count.Should().Be(3);
        items.Should().Contain(1);
        items.Should().Contain(2);
    }

    [Test]
    public void Count_ShouldReturnZeroForEmptyBag()
    {
        // Arrange
        var bag = new Bag<int>();

        // Act & Assert
        bag.Count.Should().Be(0);
    }

    [Test]
    public void Count_ShouldReturnCorrectCount()
    {
        // Arrange
        var bag = new Bag<int>
        {
            1,
            2,
            1
        };

        // Act & Assert
        bag.Count.Should().Be(3);
    }

    [Test]
    public void IEnumerableGetEnumerator_YieldsAllItemsWithCorrectMultiplicity()
    {
        // Arrange
        var bag = new Bag<string>
        {
            "apple",
            "banana",
            "apple"
        };
        var genericBag = bag as System.Collections.IEnumerable;

        // Act
        var enumerator = genericBag.GetEnumerator();
        var items = new List<object>();
        while (enumerator.MoveNext())
        {
            items.Add(enumerator.Current!);
        }

        // Assert
        items.Count(i => (string)i == "apple").Should().Be(2);
        items.Count(i => (string)i == "banana").Should().Be(1);
        items.Count.Should().Be(3);
        items.Should().BeEquivalentTo(["apple", "apple", "banana"]);
    }
}
