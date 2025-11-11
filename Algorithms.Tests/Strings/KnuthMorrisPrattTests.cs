using System;
using Algorithms.Strings;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Strings;

/// <summary>
/// Tests for Knuth-Morris-Pratt string matching algorithm.
/// </summary>
public class KnuthMorrisPrattTests
{
    [Test]
    public void FindAllOccurrences_SingleMatch_ReturnsCorrectIndex()
    {
        var text = "hello world";
        var pattern = "world";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(1);
        result[0].Should().Be(6);
    }

    [Test]
    public void FindAllOccurrences_MultipleMatches_ReturnsAllIndices()
    {
        var text = "abababa";
        var pattern = "aba";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(3);
        result.Should().ContainInOrder(0, 2, 4);
    }

    [Test]
    public void FindAllOccurrences_NoMatch_ReturnsEmptyList()
    {
        var text = "hello world";
        var pattern = "xyz";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().BeEmpty();
    }

    [Test]
    public void FindAllOccurrences_PatternLongerThanText_ReturnsEmptyList()
    {
        var text = "hi";
        var pattern = "hello";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().BeEmpty();
    }

    [Test]
    public void FindAllOccurrences_PatternEqualsText_ReturnsZero()
    {
        var text = "test";
        var pattern = "test";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(1);
        result[0].Should().Be(0);
    }

    [Test]
    public void FindAllOccurrences_OverlappingMatches_ReturnsAllIndices()
    {
        var text = "aaaa";
        var pattern = "aa";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(3);
        result.Should().ContainInOrder(0, 1, 2);
    }

    [Test]
    public void FindAllOccurrences_RepeatingPattern_ReturnsAllOccurrences()
    {
        var text = "abcabcabc";
        var pattern = "abc";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(3);
        result.Should().ContainInOrder(0, 3, 6);
    }

    [Test]
    public void FindAllOccurrences_SingleCharacter_ReturnsAllOccurrences()
    {
        var text = "aaa";
        var pattern = "a";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(3);
        result.Should().ContainInOrder(0, 1, 2);
    }

    [Test]
    public void FindAllOccurrences_CaseSensitive_ReturnsCorrectMatches()
    {
        var text = "Hello hello HELLO";
        var pattern = "hello";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(1);
        result[0].Should().Be(6);
    }

    [Test]
    public void FindAllOccurrences_SpecialCharacters_ReturnsCorrectIndices()
    {
        var text = "a@b#c@d#e@f";
        var pattern = "@";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(3);
        result.Should().ContainInOrder(1, 5, 9);
    }

    [Test]
    public void FindAllOccurrences_NullText_ThrowsArgumentNullException()
    {
        string? text = null;
        var pattern = "test";

        Action act = () => KnuthMorrisPratt.FindAllOccurrences(text!, pattern);

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("text");
    }

    [Test]
    public void FindAllOccurrences_NullPattern_ThrowsArgumentNullException()
    {
        var text = "test";
        string? pattern = null;

        Action act = () => KnuthMorrisPratt.FindAllOccurrences(text, pattern!);

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("pattern");
    }

    [Test]
    public void FindAllOccurrences_EmptyPattern_ThrowsArgumentException()
    {
        var text = "test";
        var pattern = string.Empty;

        Action act = () => KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        act.Should().Throw<ArgumentException>()
            .WithParameterName("pattern");
    }

    [Test]
    public void FindAllOccurrences_EmptyText_ReturnsEmptyList()
    {
        var text = string.Empty;
        var pattern = "test";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().BeEmpty();
    }

    [Test]
    public void FindFirst_PatternExists_ReturnsFirstIndex()
    {
        var text = "abababa";
        var pattern = "aba";

        var result = KnuthMorrisPratt.FindFirst(text, pattern);

        result.Should().Be(0);
    }

    [Test]
    public void FindFirst_PatternNotFound_ReturnsMinusOne()
    {
        var text = "hello";
        var pattern = "xyz";

        var result = KnuthMorrisPratt.FindFirst(text, pattern);

        result.Should().Be(-1);
    }

    [Test]
    public void Contains_PatternExists_ReturnsTrue()
    {
        var text = "hello world";
        var pattern = "world";

        var result = KnuthMorrisPratt.Contains(text, pattern);

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_PatternNotFound_ReturnsFalse()
    {
        var text = "hello world";
        var pattern = "xyz";

        var result = KnuthMorrisPratt.Contains(text, pattern);

        result.Should().BeFalse();
    }

    [Test]
    public void CountOccurrences_MultipleMatches_ReturnsCorrectCount()
    {
        var text = "abababa";
        var pattern = "aba";

        var result = KnuthMorrisPratt.CountOccurrences(text, pattern);

        result.Should().Be(3);
    }

    [Test]
    public void CountOccurrences_NoMatches_ReturnsZero()
    {
        var text = "hello";
        var pattern = "xyz";

        var result = KnuthMorrisPratt.CountOccurrences(text, pattern);

        result.Should().Be(0);
    }

    [Test]
    public void BuildFailureFunction_SimplePattern_ReturnsCorrectLPS()
    {
        var pattern = "ababaca";

        var lps = KnuthMorrisPratt.BuildFailureFunction(pattern);

        lps.Should().Equal(0, 0, 1, 2, 3, 0, 1);
    }

    [Test]
    public void BuildFailureFunction_RepeatingPattern_ReturnsCorrectLPS()
    {
        var pattern = "aaaa";

        var lps = KnuthMorrisPratt.BuildFailureFunction(pattern);

        lps.Should().Equal(0, 1, 2, 3);
    }

    [Test]
    public void BuildFailureFunction_NoRepeats_ReturnsAllZeros()
    {
        var pattern = "abcd";

        var lps = KnuthMorrisPratt.BuildFailureFunction(pattern);

        lps.Should().Equal(0, 0, 0, 0);
    }

    [Test]
    public void BuildFailureFunction_SingleCharacter_ReturnsZero()
    {
        var pattern = "a";

        var lps = KnuthMorrisPratt.BuildFailureFunction(pattern);

        lps.Should().Equal(0);
    }

    [Test]
    public void FindAllEndIndices_MultipleMatches_ReturnsCorrectEndIndices()
    {
        var text = "abababa";
        var pattern = "aba";

        var result = KnuthMorrisPratt.FindAllEndIndices(text, pattern);

        result.Should().HaveCount(3);
        result.Should().ContainInOrder(3, 5, 7);
    }

    [Test]
    public void FindAllEndIndices_NoMatches_ReturnsEmptyList()
    {
        var text = "hello";
        var pattern = "xyz";

        var result = KnuthMorrisPratt.FindAllEndIndices(text, pattern);

        result.Should().BeEmpty();
    }

    [Test]
    public void StartsWith_TextStartsWithPattern_ReturnsTrue()
    {
        var text = "hello world";
        var pattern = "hello";

        var result = KnuthMorrisPratt.StartsWith(text, pattern);

        result.Should().BeTrue();
    }

    [Test]
    public void StartsWith_TextDoesNotStartWithPattern_ReturnsFalse()
    {
        var text = "hello world";
        var pattern = "world";

        var result = KnuthMorrisPratt.StartsWith(text, pattern);

        result.Should().BeFalse();
    }

    [Test]
    public void EndsWith_TextEndsWithPattern_ReturnsTrue()
    {
        var text = "hello world";
        var pattern = "world";

        var result = KnuthMorrisPratt.EndsWith(text, pattern);

        result.Should().BeTrue();
    }

    [Test]
    public void EndsWith_TextDoesNotEndWithPattern_ReturnsFalse()
    {
        var text = "hello world";
        var pattern = "hello";

        var result = KnuthMorrisPratt.EndsWith(text, pattern);

        result.Should().BeFalse();
    }

    [Test]
    public void EndsWith_NullText_ReturnsFalse()
    {
        string? text = null;
        var pattern = "test";

        var result = KnuthMorrisPratt.EndsWith(text!, pattern);

        result.Should().BeFalse();
    }

    [Test]
    public void EndsWith_NullPattern_ReturnsFalse()
    {
        var text = "test";
        string? pattern = null;

        var result = KnuthMorrisPratt.EndsWith(text, pattern!);

        result.Should().BeFalse();
    }

    [Test]
    public void FindAllOccurrences_LongText_ReturnsCorrectIndices()
    {
        var text = new string('a', 1000) + "pattern" + new string('b', 1000);
        var pattern = "pattern";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(1);
        result[0].Should().Be(1000);
    }

    [Test]
    public void FindAllOccurrences_UnicodeCharacters_ReturnsCorrectIndices()
    {
        var text = "Hello 世界 World";
        var pattern = "世界";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(1);
        result[0].Should().Be(6);
    }

    [Test]
    public void FindAllOccurrences_ComplexPattern_ReturnsCorrectIndices()
    {
        var text = "AABAACAADAABAABA";
        var pattern = "AABA";

        var result = KnuthMorrisPratt.FindAllOccurrences(text, pattern);

        result.Should().HaveCount(3);
        result.Should().ContainInOrder(0, 9, 12);
    }
}
