using System;
using System.Linq;
using Algorithms.Crypto.Paddings;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Crypto.Paddings;

public class Iso7816D4PaddingTests
{
    private readonly Iso7816D4Padding padding = new Iso7816D4Padding();

    [Test]
    public void AddPadding_WhenCalledWithValidInput_ShouldReturnCorrectPadding()
    {
        var inputData = new byte[10];
        var inputOffset = 5;

        var result = padding.AddPadding(inputData, inputOffset);

        result.Should().Be(5);
        inputData[5].Should().Be(80);
        inputData.Skip(6).Should().AllBeEquivalentTo(0);
    }

    [Test]
    public void AddPadding_WhenCalledWithInvalidInput_ShouldThrowArgumentException()
    {
        var inputData = new byte[10];
        var inputOffset = 11;

        Action act = () => padding.AddPadding(inputData, inputOffset);

        act.Should().Throw<ArgumentException>()
            .WithMessage("not enough space in input array for padding");
    }

    [Test]
    public void AddPadding_WhenCalledWithZeroOffset_ShouldReturnCorrectPadding()
    {
        var inputData = new byte[10];
        var inputOffset = 0;

        var result = padding.AddPadding(inputData, inputOffset);

        result.Should().Be(10);
        inputData[0].Should().Be(80);
        inputData.Skip(1).Should().AllBeEquivalentTo(0);
    }

    [Test]
    public void AddPadding_WhenCalledWithOffsetEqualToLength_ShouldThrowArgumentException()
    {
        var inputData = new byte[10];
        var inputOffset = 10;

        Action act = () => padding.AddPadding(inputData, inputOffset);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
    }

    [Test]
    public void AddPadding_WhenCalledWithEmptyArray_ShouldThrowArgumentException()
    {
        var inputData = new byte[0];
        var inputOffset = 0;

        Action act = () => padding.AddPadding(inputData, inputOffset);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
    }

    [Test]
    public void RemovePadding_WhenCalledWithValidInput_shouldReturnCorrectData()
    {
        var inputData = new byte[] { 1, 2, 3, 4, 5, 0x80, 0, 0, 0 };

        var result = padding.RemovePadding(inputData);

        result.Should().Equal(new byte[] { 1, 2, 3, 4, 5 });
    }

    [Test]
    public void RemovePadding_WhenCalledWithInvalidInput_ShouldThrowArgumentException()
    {
        var inputData = new byte[] { 1, 2, 3, 4, 5 };

        Action act = () => padding.RemovePadding(inputData);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid padding");
    }

    [Test]
    public void RemovePadding_WhenCalledWithArrayContainingOnlyPadding_ShouldReturnEmptyArray()
    {
        var inputData = new byte[] { 0x80, 0, 0, 0, 0 };

        var result = padding.RemovePadding(inputData);

        result.Should().BeEmpty();
    }

    [Test]
    public void RemovePadding_WhenCalledWithArrayNotContainingStartOfPadding_ShouldThrowArgumentException()
    {
        var input = new byte[] { 1, 2, 3, 4, 5, 0, 0, 0, 0 };

        Action act = () => padding.RemovePadding(input);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid padding");
    }

    [Test]
    public void GetPaddingCount_WhenCalledWithValidInput_ShouldReturnCorrectCount()
    {
        var inputData = new byte[] { 1, 2, 3, 4, 5, 0x80, 0, 0, 0 };

        var result = padding.GetPaddingCount(inputData);

        result.Should().Be(4);
    }

    [Test]
    public void GetPaddingCount_WhenCalledWithInvalidInput_ShouldThrowArgumentException()
    {
        var inputData = new byte[] { 1, 2, 3, 4, 5 };

        Action act = () => padding.GetPaddingCount(inputData);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Pad block corrupted");
    }

    [Test]
    public void GetPaddingCount_WhenCalledWithEmptyArray_ShouldThrowArgumentException()
    {
        var inputData = Array.Empty<byte>();

        Action act = () => padding.GetPaddingCount(inputData);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Pad block corrupted");
    }

    [Test]
    public void GetPaddingCount_WhenCalledWithArrayContainingOnlyPadding_ShouldReturnCorrectCount()
    {
        var inputData = new byte[] { 0x80, 0x00, 0x00 };

        var result = padding.GetPaddingCount(inputData);

        result.Should().Be(3);
    }

    [Test]
    public void GetPaddingCount_WhenCalledWithArrayNotContainingStartOfPadding_ShouldThrowAnException()
    {
        var inputData = new byte[] { 1, 2, 3, 4, 5, 0, 0, 0, 0 };

        Action act = () => padding.GetPaddingCount(inputData);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Pad block corrupted");
    }
}
