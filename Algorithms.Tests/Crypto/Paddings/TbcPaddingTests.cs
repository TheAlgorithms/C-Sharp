using System;
using Algorithms.Crypto.Paddings;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Crypto.Paddings;

public class TbcPaddingTests
{
    private readonly TbcPadding padding = new TbcPadding();

    [Test]
    public void AddPadding_WhenInputOffsetIsZero_ShouldPadWithLastBit()
    {
        var input = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var inputOffset = 0;

        var result = padding.AddPadding(input, inputOffset);

        result.Should().Be(4);
        input.Should().BeEquivalentTo(new byte[]{0xff, 0xff, 0xff, 0xff});
    }

    [Test]
    public void AddPadding_WhenInputOffsetIsPositive_ShouldPadWithPreviousBit()
    {
        var input = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var inputOffset = 2;

        var result = padding.AddPadding(input, inputOffset);

        result.Should().Be(2);
        input.Should().BeEquivalentTo(new byte[] { 0x01, 0x02, 0xff, 0xff });
    }

    [Test]
    public void AddPadding_WhenInputOffsetIsGreaterThanLength_ShouldThrowArgumentException()
    {
        var input = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        var inputOffset = 5;

        Action act = () => padding.AddPadding(input, inputOffset);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Not enough space in input array for padding");
    }

    [Test]
    public void AddPadding_WhenLastBitIsZero_ShouldPadWith0xFF()
    {
        var input = new byte[] { 0x02 };
        const int inputOffset = 0;

        var result = padding.AddPadding(input, inputOffset);

        result.Should().Be(1);
        input.Should().BeEquivalentTo(new byte[] { 0xFF });
    }

    [Test]
    public void AddPadding_WhenLastBitIsOne_ShouldPadWith0x00()
    {
        var input = new byte[] { 0x03 };
        const int inputOffset = 0;

        var result = padding.AddPadding(input, inputOffset);

        result.Should().Be(1);
        input.Should().BeEquivalentTo(new byte[] { 0x00 });
    }

    [Test]
    public void RemovePadding_WhenCalledWithPaddedData_ShouldReturnUnpaddedData()
    {
        var paddedData = new byte[] { 0x01, 0x02, 0x03, 0xff, 0xff };
        var expectedData = new byte[] { 0x01, 0x02, 0x03 };

        var result = padding.RemovePadding(paddedData);

        result.Should().BeEquivalentTo(expectedData);
    }

    [Test]
    public void RemovePadding_WhenCalledWithUnpaddedData_ShouldReturnsSameData()
    {
        var unpaddedData = new byte[] { 0x01, 0x02, 0x03 };

        var result = padding.RemovePadding(unpaddedData);

        result.Should().BeEquivalentTo(unpaddedData);
    }

    [Test]
    public void RemovePadding_WhenCalledWithEmptyArray_ShouldReturnEmptyArray()
    {
        var emptyData = Array.Empty<byte>();

        var result = padding.RemovePadding(emptyData);

        result.Should().BeEquivalentTo(emptyData);
    }

    [Test]
    public void RemovePadding_WhenCalledWithSingleBytePaddedData_ShouldReturnEmptyArray()
    {
        var singleBytePaddedData = new byte[] { 0xff };

        var result = padding.RemovePadding(singleBytePaddedData);

        result.Should().BeEmpty();
    }

    [Test]
    public void RemovePadding_WhenCalledWitAllBytesPadded_ShouldReturnEmptyArray()
    {
        var allBytesPaddedData = new byte[] { 0xff, 0xff, 0xff };
        var emptyData = Array.Empty<byte>();

        var result = padding.RemovePadding(allBytesPaddedData);

        result.Should().BeEquivalentTo(emptyData);
    }

    [Test]
    public void GetPaddingBytes_WhenCalledWithPaddedData_ShouldReturnCorrectPaddingCount()
    {

        var paddedData = new byte[] { 0x01, 0x02, 0x03, 0xff, 0xff };
        const int expectedPaddingCount = 2;

        var result = padding.GetPaddingCount(paddedData);

        result.Should().Be(expectedPaddingCount);
    }

    [Test]
    public void GetPaddingBytes_WhenCalledWithUnpaddedData_ShouldReturnZero()
    {
        var unpaddedData = new byte[] { 0x01, 0x02, 0x03 };

        Action action = () => padding.GetPaddingCount(unpaddedData);

        action.Should().Throw<ArgumentException>()
            .WithMessage("No padding found");
    }

    [Test]
    public void GetPaddingBytes_WhenCalledWithEmptyArray_ShouldReturnZero()
    {
        var emptyData = Array.Empty<byte>();

        Action action = () => padding.GetPaddingCount(emptyData);

        action.Should().Throw<ArgumentException>()
            .WithMessage("No padding found.");
    }
}
