using System;
using Algorithms.Crypto.Paddings;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Crypto.Paddings;

public class X932PaddingTests
{
    private readonly X932Padding zeroPadding = new X932Padding(false);
    private readonly X932Padding randomPadding = new X932Padding(true);

    [Test]
    public void AddPadding_WhenCalledWithZeroPadding_ShouldReturnCorrectCode()
    {
        var inputData = new byte[10];
        const int inputOffset = 5;

        var result = zeroPadding.AddPadding(inputData, inputOffset);

        result.Should().Be(5);
    }

    [Test]
    public void AddPadding_WhenCalledWithZeroPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()
    {
        var inputData = new byte[10];
        const int inputOffset = 10;

        Action action = () => zeroPadding.AddPadding(inputData, inputOffset);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
    }

    [Test]
    public void AddPadding_WhenCalledWithZeroPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()
    {
        var inputData = new byte[10];
        const int inputOffset = 11;

        Action action = () => zeroPadding.AddPadding(inputData, inputOffset);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
    }


    [Test]
    public void AddPadding_WhenCalledWithRandomPadding_ShouldReturnCorrectCode()
    {
        var inputData = new byte[10];
        const int inputOffset = 5;

        var result = randomPadding.AddPadding(inputData, inputOffset);

        result.Should().Be(5);
    }

    [Test]
    public void AddPadding_WhenCalledWithRandomPaddingAndOffsetIsEqualToDataLength_ShouldThrowArgumentException()
    {
        var inputData = new byte[10];
        const int inputOffset = 10;

        Action action = () => randomPadding.AddPadding(inputData, inputOffset);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
    }

    [Test]
    public void AddPadding_WhenCalledWithRandomPaddingAndOffsetIsGreaterThanDataLength_ShouldThrowArgumentException()
    {
        var inputData = new byte[10];
        const int inputOffset = 11;

        Action action = () => randomPadding.AddPadding(inputData, inputOffset);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
    }

    [Test]
    public void AddPadding_WhenCalledWithZeroPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()
    {
        var inputData = new byte[10];
        const int inputOffset = 0;

        var result = zeroPadding.AddPadding(inputData, inputOffset);

        result.Should().Be(10);
    }

    [Test]
    public void AddPadding_WhenCalledWithRandomPaddingAndOffsetIsZero_ShouldReturnLengthOfInputData()
    {
        var inputData = new byte[10];
        const int inputOffset = 0;

        var result = randomPadding.AddPadding(inputData, inputOffset);

        result.Should().Be(10);
    }

    [Test]
    public void AddPadding_WhenCalledWithRandomPadding_ShouldFillThePaddingWithRandomValues()
    {
        var inputData = new byte[10];
        const int inputOffset = 5;

        var result = randomPadding.AddPadding(inputData, inputOffset);

        for (var i = inputOffset; i < inputData.Length - 1; i++)
        {
            inputData[i].Should().BeInRange(0, 255);
        }

        inputData[^1].Should().Be((byte)result);
    }

    [Test]
    public void RemovePadding_WhenCalledInEmptyArray_ShouldReturnAnEmptyArray()
    {
        var result = zeroPadding.RemovePadding(Array.Empty<byte>());

        result.Should().AllBeEquivalentTo(Array.Empty<byte>());
    }

    [Test]
    public void RemovePadding_WhenCalledOnArrayWithValidPadding_ShouldRemovePadding()
    {
        var inputData = new byte[] { 1, 2, 3, 2 };
        var expectedOutput = new byte[] { 1, 2 };

        var result = zeroPadding.RemovePadding(inputData);

        result.Should().BeEquivalentTo(expectedOutput);
    }

    [Test]
    public void RemovePadding_WithInvalidPadding_ThrowsArgumentException()
    {
        var inputData = new byte[] { 1, 2, 3, 5 };

        Action act = () => zeroPadding.RemovePadding(inputData);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid padding length");
    }

    [Test]
    public void GetPaddingCount_WithValidPadding_ReturnsCorrectCount()
    {
        var inputData = new byte[] { 1, 2, 3, 2 };

        var result = zeroPadding.GetPaddingCount(inputData);

        result.Should().Be(2);
    }

    [Test]
    public void GetPaddingCount_WithInvalidPadding_ThrowsArgumentException()
    {
        var inputData = new byte[] { 1, 2, 3, 5 };

        Action action = () => zeroPadding.GetPaddingCount(inputData);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Pad block corrupted");
    }
}
