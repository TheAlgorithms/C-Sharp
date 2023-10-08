using System;
using Algorithms.Crypto.Paddings;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Crypto.Paddings;

public class Iso10126D2PaddingTests
{
    private readonly Iso10126D2Padding padding = new Iso10126D2Padding();

    [Test]
    public void AddPadding_WhenInputOffsetIsLessThanInputDataLength_ShouldNotThrowException()
    {
        var inputData = new byte[10];
        const int inputOffset = 5;

        Action act = () => padding.AddPadding(inputData, inputOffset);

        act.Should().NotThrow<ArgumentException>();
    }

    [Test]
    public void AddPadding_WhenInputOffsetIsEqualToInputDataLength_ShouldThrowException()
    {
        var inputData = new byte[10];
        const int inputOffset = 10;

        Action act = () => padding.AddPadding(inputData, inputOffset);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
    }

    [Test]
    public void AddPadding_WhenInputOffsetIsGreaterThanInputDataLength_ShouldThrowException()
    {
        var inputData = new byte[10];
        const int inputOffset = 128;

        Action act = () => padding.AddPadding(inputData, inputOffset);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
    }

    [Test]
    public void AddPadding_WhenInputArrayIsValid_ShouldReturnCorrectPaddingSize()
    {
        var inputData = new byte[10];
        const int inputOffset = 5;

        var result = padding.AddPadding(inputData, inputOffset);

        result.Should().Be(5);
    }

    [Test]
    public void RemovePadding_WhenLengthIsLessThanOne_ShouldThrowATantrum()
    {
        var inputData = new byte[] { 0 };

        Action act = () => padding.RemovePadding(inputData);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid padding length");
    }

    [Test]
    public void RemovePadding_WhenPaddingLengthIsGreaterThanInputDataLength_ShouldThrowAnException()
    {
        var inputData = new byte[] { 2 };

        Action act = () => padding.RemovePadding(inputData);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid padding length");
    }

    [Test]
    public void RemovePadding_WhenInputDataIsValid_ShouldReturnCorrectData()
    {
        var inputData = new byte[] { 1, 2, 3, 1 };
        var expected = new byte[] { 1, 2, 3 };

        var result = padding.RemovePadding(inputData);

        result.Should().Equal(expected);
    }

    [Test]
    public void GetPaddingCount_WhenInputIsNull_ShouldThrowAnException()
    {
        byte[]? input = null;

        Action act = () => padding.GetPaddingCount(input!);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Input cannot be null (Parameter 'input')");
    }

    [Test]
    public void GetPaddingCount_WhenPaddingBlockIsCorrupted_ShouldThrowAnException()
    {
        var input = new byte[] { 1, 2, 3, 4, 5, 7 };

        Action act = () => padding.GetPaddingCount(input);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Padding block is corrupted");
    }

    [Test]
    public void GetPaddingCount_WhenInputDataIsValid_ShouldReturnCorrectPaddingCount()
    {
        var input = new byte[] { 1, 2, 3, 4, 1 };

        var result = padding.GetPaddingCount(input);

        result.Should().Be(1);
    }
}
