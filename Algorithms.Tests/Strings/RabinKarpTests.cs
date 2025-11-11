using System;
using System.Collections.Generic;
using Algorithms.Strings;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Strings;

/// <summary>
/// Tests for Rabin-Karp string matching algorithm.
/// </summary>
public class RabinKarpTests
{
    [Test]
    public void FindAllOccurrences_SingleMatch_ReturnsCorrectIndex()
    {
        var text = "hello world";
        var pattern = "world";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(1);
        result[0].Should().Be(6);
    }

    [Test]
    public void FindAllOccurrences_MultipleMatches_ReturnsAllIndices()
    {
        var text = "abababa";
        var pattern = "aba";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(3);
        result.Should().ContainInOrder(0, 2, 4);
    }

    [Test]
    public void FindAllOccurrences_NoMatch_ReturnsEmptyList()
    {
        var text = "hello world";
        var pattern = "xyz";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().BeEmpty();
    }

    [Test]
    public void FindAllOccurrences_PatternLongerThanText_ReturnsEmptyList()
    {
        var text = "hi";
        var pattern = "hello";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().BeEmpty();
    }

    [Test]
    public void FindAllOccurrences_PatternEqualsText_ReturnsZero()
    {
        var text = "test";
        var pattern = "test";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(1);
        result[0].Should().Be(0);
    }

    [Test]
    public void FindAllOccurrences_SingleCharacterPattern_ReturnsAllOccurrences()
    {
        var text = "aaa";
        var pattern = "a";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(3);
        result.Should().ContainInOrder(0, 1, 2);
    }

    [Test]
    public void FindAllOccurrences_OverlappingMatches_ReturnsAllIndices()
    {
        var text = "aaaa";
        var pattern = "aa";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(3);
        result.Should().ContainInOrder(0, 1, 2);
    }

    [Test]
    public void FindAllOccurrences_CaseSensitive_ReturnsCorrectMatches()
    {
        var text = "Hello hello HELLO";
        var pattern = "hello";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(1);
        result[0].Should().Be(6);
    }

    [Test]
    public void FindAllOccurrences_SpecialCharacters_ReturnsCorrectIndices()
    {
        var text = "a@b#c@d#e@f";
        var pattern = "@";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(3);
        result.Should().ContainInOrder(1, 5, 9);
    }

    [Test]
    public void FindAllOccurrences_NumbersInText_ReturnsCorrectIndices()
    {
        var text = "123456789123";
        var pattern = "123";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(2);
        result.Should().ContainInOrder(0, 9);
    }

    [Test]
    public void FindAllOccurrences_WhitespacePattern_ReturnsCorrectIndices()
    {
        var text = "a b c d";
        var pattern = " ";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(3);
        result.Should().ContainInOrder(1, 3, 5);
    }

    [Test]
    public void FindAllOccurrences_LongText_ReturnsCorrectIndices()
    {
        var text = new string('a', 1000) + "pattern" + new string('b', 1000);
        var pattern = "pattern";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(1);
        result[0].Should().Be(1000);
    }

    [Test]
    public void FindAllOccurrences_NullText_ThrowsArgumentNullException()
    {
        string? text = null;
        var pattern = "test";

        Action act = () => RabinKarp.FindAllOccurrences(text!, pattern);

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("text");
    }

    [Test]
    public void FindAllOccurrences_NullPattern_ThrowsArgumentNullException()
    {
        var text = "test";
        string? pattern = null;

        Action act = () => RabinKarp.FindAllOccurrences(text, pattern!);

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("pattern");
    }

    [Test]
    public void FindAllOccurrences_EmptyPattern_ThrowsArgumentException()
    {
        var text = "test";
        var pattern = string.Empty;

        Action act = () => RabinKarp.FindAllOccurrences(text, pattern);

        act.Should().Throw<ArgumentException>()
            .WithParameterName("pattern");
    }

    [Test]
    public void FindAllOccurrences_EmptyText_ReturnsEmptyList()
    {
        var text = string.Empty;
        var pattern = "test";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().BeEmpty();
    }

    [Test]
    public void FindFirst_PatternExists_ReturnsFirstIndex()
    {
        var text = "abababa";
        var pattern = "aba";

        var result = RabinKarp.FindFirst(text, pattern);

        result.Should().Be(0);
    }

    [Test]
    public void FindFirst_PatternNotFound_ReturnsMinusOne()
    {
        var text = "hello";
        var pattern = "xyz";

        var result = RabinKarp.FindFirst(text, pattern);

        result.Should().Be(-1);
    }

    [Test]
    public void Contains_PatternExists_ReturnsTrue()
    {
        var text = "hello world";
        var pattern = "world";

        var result = RabinKarp.Contains(text, pattern);

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_PatternNotFound_ReturnsFalse()
    {
        var text = "hello world";
        var pattern = "xyz";

        var result = RabinKarp.Contains(text, pattern);

        result.Should().BeFalse();
    }

    [Test]
    public void FindAllOccurrences_RepeatingPattern_ReturnsAllOccurrences()
    {
        var text = "testtest";
        var pattern = "test";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(2);
        result.Should().ContainInOrder(0, 4);
    }

    [Test]
    public void FindAllOccurrences_PatternAtEnd_ReturnsCorrectIndex()
    {
        var text = "hello world";
        var pattern = "rld";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(1);
        result[0].Should().Be(8);
    }

    [Test]
    public void FindAllOccurrences_PatternAtStart_ReturnsZero()
    {
        var text = "hello world";
        var pattern = "hel";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(1);
        result[0].Should().Be(0);
    }

    [Test]
    public void FindAllOccurrences_UnicodeCharacters_ReturnsCorrectIndices()
    {
        var text = "Hello 世界 World";
        var pattern = "世界";

        var result = RabinKarp.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(1);
        result[0].Should().Be(6);
    }
}
