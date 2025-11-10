using Algorithms.Other;
using NUnit.Framework;
using FluentAssertions;

namespace Algorithms.Tests.Other;

public class BoyerMooreMajorityVoteTests
{
    [Test]
    public void FindMajority_SimpleMajority_ReturnsCorrectElement()
    {
        var nums = new[] { 3, 3, 4, 2, 3, 3, 3 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
        result.Should().Be(3);
    }

    [Test]
    public void FindMajority_AllSameElements_ReturnsThatElement()
    {
        var nums = new[] { 5, 5, 5, 5 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
        result.Should().Be(5);
    }

    [Test]
    public void FindMajority_NoMajority_ReturnsNull()
    {
        var nums = new[] { 1, 2, 3, 4 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
        result.Should().BeNull();
    }

    [Test]
    public void FindMajority_EmptyArray_ReturnsNull()
    {
        var nums = Array.Empty<int>();
        var result = BoyerMooreMajorityVote.FindMajority(nums);
        result.Should().BeNull();
    }

    [Test]
    public void FindMajority_NullArray_ReturnsNull()
    {
        int[]? nums = null;
        var result = BoyerMooreMajorityVote.FindMajority(nums!);
        result.Should().BeNull();
    }

    [Test]
    public void FindMajority_SingleElement_ReturnsThatElement()
    {
        var nums = new[] { 7 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
        result.Should().Be(7);
    }

    [Test]
    public void FindMajority_MajorityAtEnd_ReturnsCorrectElement()
    {
        var nums = new[] { 1, 2, 2, 2, 2 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
        result.Should().Be(2);
    }

    [Test]
    public void FindMajority_MajorityAtStart_ReturnsCorrectElement()
    {
        var nums = new[] { 8, 8, 8, 8, 1, 2 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
        result.Should().Be(8);
    }

    [Test]
    public void FindMajority_NegativeNumbers_ReturnsCorrectElement()
    {
        var nums = new[] { -1, -1, -1, 2, 2 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
        result.Should().Be(-1);
    }

    [Test]
    public void FindMajority_ExactlyHalf_ReturnsNull()
    {
        var nums = new[] { 1, 1, 2, 2 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
        result.Should().BeNull();
    }
}
