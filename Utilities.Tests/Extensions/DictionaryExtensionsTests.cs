using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Utilities.Extensions;

namespace Utilities.Tests.Extensions;

public class DictionaryExtensionsTests
{
    [Test]
    public void AddMany_ShouldThrowArgumentException_WhenKeyAlreadyExists()
    {
        var dictionary = new Dictionary<string, int> { ["one"] = 1 };
        var enumerable = new[] { ("one", 1), ("two", 2) };

        var action = () => dictionary.AddMany(enumerable);

        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void AddMany_ShouldAddAllKeyValuePairs()
    {
        var dictionary = new Dictionary<string, int> { ["one"] = 1 };
        var enumerable = new[] { ("two", 2), ("three", 3) };

        dictionary.AddMany(enumerable);

        dictionary.Should().HaveCount(3);

        dictionary.Should().ContainKey("one").WhoseValue.Should().Be(1);
        dictionary.Should().ContainKey("two").WhoseValue.Should().Be(2);
        dictionary.Should().ContainKey("three").WhoseValue.Should().Be(3);
    }

    [Test]
    public void AddMany_ShouldNotChangeDictionary_WhenEnumerableIsEmpty()
    {
        var dictionary = new Dictionary<string, int> { ["one"] = 1 };
        var enumerable = Array.Empty<(string, int)>();

        dictionary.AddMany(enumerable);

        dictionary.Should().HaveCount(1);
        dictionary.Should().ContainKey("one").WhoseValue.Should().Be(1);
    }

    [Test]
    public void AddMany_ShouldThrowArgumentNullException_WhenDictionaryIsNull()
    {
        Dictionary<string, int> dictionary = null!;
        var enumerable = new[] { ("one", 1) };

        var action = () => dictionary.AddMany(enumerable);

        action.Should().Throw<NullReferenceException>();
    }

    [Test]
    public void AddMany_ShouldThrowArgumentNullException_WhenEnumerableIsNull()
    {
        var dictionary = new Dictionary<string, int> { ["one"] = 1 };
        IEnumerable<(string, int)> enumerable = null!;

        var action = () => dictionary.AddMany(enumerable);

        action.Should().Throw<NullReferenceException>();
    }

    [Test]
    public void AddMany_ShouldAllowNullValues_WhenValueTypeIsNullable()
    {
        var dictionary = new Dictionary<string, int?> { ["one"] = 1 };
        var enumerable = new[] { ("two", (int?)null) };

        dictionary.AddMany(enumerable);

        dictionary.Should().HaveCount(2);
        dictionary.Should().ContainKey("two").WhoseValue.Should().Be(null);
    }


    [Test]
    public void AddMany_ShouldAllowNullValue_WhenValueIsNullable()
    {
        var dictionary = new Dictionary<int, string?>();  // Key type is int, value type is nullable string
        var enumerable = new[]
        {
                (1, null),  // null value
                (2, "banana")
            };

        dictionary.AddMany(enumerable);

        dictionary.Should().ContainKey(1).WhoseValue.Should().BeNull();
        dictionary.Should().ContainKey(2).WhoseValue.Should().Be("banana");
    }

    [Test]
    public void AddMany_ShouldThrowArgumentException_WhenAddingDuplicateKey()
    {
        var dictionary = new Dictionary<int, string>();  // Key type is int, value type is nullable string
        var enumerable = new[]
        {
                (1, "Things"),   // First entry
                (2, "Stuff"),
                (1, "That Thing")   // Duplicate key (should throw exception)
            };

        var action = () => dictionary.AddMany(enumerable);

        action.Should().Throw<ArgumentException>();  // Adding a duplicate key should throw ArgumentException
    }

    [Test]
    public void AddMany_ShouldAddManyKeyValuePairs_WhenAddingLargeEnumerable()
    {
        var dictionary = new Dictionary<int, string>();
        var enumerable = new List<(int, string)>();

        // Create a large enumerable
        for (int i = 0; i < 10000; i++)
        {
            enumerable.Add((i, "Value" + i));
        }

        dictionary.AddMany(enumerable);

        dictionary.Should().HaveCount(10000);
        dictionary[9999].Should().Be("Value9999");
    }
}
