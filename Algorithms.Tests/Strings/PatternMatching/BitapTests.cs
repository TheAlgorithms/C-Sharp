using Algorithms.Strings.PatternMatching;

namespace Algorithms.Tests.Strings.PatternMatching;

[TestFixture]
public class BitapTests
{
    [Test]
    public void FindExactPattern_EmptyTextReturnsError()
    {
        Assert.That(Bitap.FindExactPattern("", "abc"), Is.EqualTo(-1));
    }

    [Test]
    public void FindExactPattern_EmptyPatternReturnsZero()
    {
        Assert.That(Bitap.FindExactPattern("abc", ""), Is.EqualTo(0));
    }

    [Test]
    public void FindExactPattern_PatternFoundAtBeginning()
    {
        Assert.That(Bitap.FindExactPattern("hello world", "hello"), Is.EqualTo(0));
    }

    [Test]
    public void FindExactPattern_PatternFoundInTheMiddle()
    {
        Assert.That(Bitap.FindExactPattern("abcabc", "cab"), Is.EqualTo(2));
    }

    [Test]
    public void FindExactPattern_PatternFoundAtEnd()
    {
        Assert.That(Bitap.FindExactPattern("the end", "end"), Is.EqualTo(4));
    }

    [Test]
    public void FindExactPattern_PatternNotFound()
    {
        Assert.That(Bitap.FindExactPattern("abcdefg", "xyz"), Is.EqualTo(-1));
    }

    [Test]
    public void FindExactPattern_PatternLongerThanText()
    {
        Assert.That(Bitap.FindExactPattern("short", "longerpattern"), Is.EqualTo(-1));
    }

    [Test]
    public void FindExactPattern_OverlappingPatterns()
    {
        Assert.That(Bitap.FindExactPattern("ababab", "abab"), Is.EqualTo(0));
    }

    [Test]
    public void FindExactPattern_PatternTooLongThrowsException()
    {
        var longPattern = new string('a', 32);
        Assert.Throws<ArgumentException>(() => Bitap.FindExactPattern("some text", longPattern));
    }

    [Test]
    public void FindExactPattern_SpecialCharactersInPattern()
    {
        Assert.That(Bitap.FindExactPattern("hello, world!", ", wo"), Is.EqualTo(5));
    }

    [Test]
    public void FindFuzzyPattern_EmptyTextReturnsZero()
    {
        Assert.That(Bitap.FindFuzzyPattern("", "abc", 1), Is.EqualTo(0));
    }

    [Test]
    public void FindFuzzyPattern_EmptyPatternReturnsZero()
    {
        Assert.That(Bitap.FindFuzzyPattern("def", "", 1), Is.EqualTo(0));
    }

    [Test]
    public void FindFuzzyPattern_ExactMatchFound()
    {
        Assert.That(Bitap.FindFuzzyPattern("hello world", "hello", 0), Is.EqualTo(0));
    }

    [Test]
    public void FindFuzzyPattern_FuzzyMatchWithOneMismatch()
    {
        Assert.That(Bitap.FindFuzzyPattern("hello world", "hellp", 1), Is.EqualTo(0));
    }

    [Test]
    public void FindFuzzyPattern_FuzzyMatchWithMultipleMismatches()
    {
        Assert.That(Bitap.FindFuzzyPattern("abcde", "xbcdz", 2), Is.EqualTo(0));
    }

    [Test]
    public void FindFuzzyPattern_FuzzyMatchAtEnd()
    {
        Assert.That(Bitap.FindFuzzyPattern("abcdefg", "efx", 1), Is.EqualTo(4));
    }

    [Test]
    public void FindFuzzyPattern_FuzzyMatchNotFound()
    {
        Assert.That(Bitap.FindFuzzyPattern("abcdefg", "xyz", 2), Is.EqualTo(-1));
    }

    [Test]
    public void FindFuzzyPattern_PatternTooLongReturnsNegativeOne()
    {
        var longPattern = new string('a', 32);
        Assert.That(Bitap.FindFuzzyPattern("some text", longPattern, 1), Is.EqualTo(-1));
    }
}
