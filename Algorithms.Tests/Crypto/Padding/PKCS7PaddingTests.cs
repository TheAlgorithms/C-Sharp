using System;
using Algorithms.Crypto.Paddings;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Crypto.Padding;

public class PKCS7PaddingTests
{
    private const int defaultBlockSize = 16;

    [Test]
    public void Constructor_WhenBlockSizeIsLessThanOne_ShouldThrowArgumentOutOfRangeException()
    {
        const int blockSize = 0;

        Action act = () => new PKCS7Padding(blockSize);

        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Invalid block size: 0 (Parameter 'blockSize')");
    }

    [Test]
    public void Constructor_WhenBlockSizeIsMoreThan255_ShouldThrowArgumentOutOfRangeException()
    {
        const int blockSize = 256;

        Action act = () => new PKCS7Padding(blockSize);

        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Invalid block size: 256 (Parameter 'blockSize')");
    }

    [Test]
    public void Constructor_WhenBlockSizeIsWithinValidRange_ShouldNotThrowAFit()
    {
        const int blockSize = 128;

        Action act = () => new PKCS7Padding(blockSize);

        act.Should().NotThrow();
    }

    [Test]
    public void AddPadding_WhenNotEnoughSpaceInInputArrayForPadding_ShouldThrowArgumentException()
    {
        var padding = new PKCS7Padding(defaultBlockSize);
        const int inputOffset = 1;

        var size16Input = new byte[16];

        Action act = () => padding.AddPadding(size16Input, inputOffset);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
    }

    [Test]
    public void AddPadding_WhenInputArrayHasEnoughSpace_ShouldReturnCorrectPaddingSize()
    {
        var padding = new PKCS7Padding(defaultBlockSize);
        const int inputOffset = defaultBlockSize;

        var size32Input = new byte[32];

        var result = padding.AddPadding(size32Input, inputOffset);

        result.Should().Be(defaultBlockSize);
    }

    [Test]
    public void AddPadding_WhenAppliedToAnInputArray_ShouldAddCorrectPKCS7Padding()
    {
        var padding = new PKCS7Padding(defaultBlockSize);
        const int inputOffset = defaultBlockSize;

        var size32Input = new byte[32];

        var result = padding.AddPadding(size32Input, inputOffset);

        for (var i = 0; i < defaultBlockSize - 1; i++)
        {
            size32Input[inputOffset + i].Should().Be(defaultBlockSize);
        }
    }

    [Test]
    public void RemovePadding_WhenAppliedToAValidInputArray_ShouldRemovePKCS7PaddingCorrectly()
    {
        var paddingSize = 5;
        var size32Input = new byte[32];
        for (var i = 0; i < paddingSize; i++)
        {
            size32Input[size32Input.Length - 1 - i] = (byte)paddingSize;
        }

        var padding = new PKCS7Padding(defaultBlockSize);

        var output = padding.RemovePadding(size32Input);

        output.Length.Should().Be(size32Input.Length - paddingSize);
    }

    [Test]
    public void RemovePadding_WhenInputLengthNotMultipleOfBlockSize_ShouldThrowArgumentException()
    {
        var input = new byte[defaultBlockSize + 1]; // Length is not a multiple of blockSize
        var padding = new PKCS7Padding(defaultBlockSize);

        Action act = () => padding.RemovePadding(input);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Input length must be a multiple of block size");
    }

    [Test]
    public void RemovePadding_WhenInvalidPaddingLength_ShouldThrowArgumentException()
    {
        var size32Input = new byte[32];

        size32Input[^1] = (byte)(defaultBlockSize + 1); // Set invalid padding length
        var padding = new PKCS7Padding(defaultBlockSize);

        Action act = () => padding.RemovePadding(size32Input);

        act.Should().Throw<ArgumentException>().WithMessage("Invalid padding length");
    }

    [Test]
    public void RemovePadding_WhenInvalidPadding_ShouldThrowArgumentException()
    {
        var size32Input = new byte[32];

        size32Input[^1] = (byte)(defaultBlockSize); // Set valid padding length
        size32Input[^2] = (byte)(defaultBlockSize - 1); // Set invalid padding byte
        var padding = new PKCS7Padding(defaultBlockSize);

        Action act = () => padding.RemovePadding(size32Input);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid padding");
    }

    [Test]
    public void GetPaddingCount_WhenInputArrayIsValid_ShouldReturnCorrectPaddingCount()
    {
        var paddingSize = 5;
        var size32Input = new byte[32];

        for (var i = 0; i < paddingSize; i++)
        {
            size32Input[size32Input.Length - 1 - i] = (byte)paddingSize; // Add padding bytes at the end of the array
        }

        var padding = new PKCS7Padding(defaultBlockSize);

        var output = padding.GetPaddingCount(size32Input);

        output.Should().Be(paddingSize);
    }

    [Test]
    public void GetPaddingCount_WhenInvalidPadding_ShouldThrowArgumentException()
    {
        var size32Input = new byte[32];

        size32Input[^1] = defaultBlockSize;
        size32Input[^2] = defaultBlockSize - 1;

        var padding = new PKCS7Padding(defaultBlockSize);

        Action act = () => padding.GetPaddingCount(size32Input);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Padding block is corrupted");
    }
}
