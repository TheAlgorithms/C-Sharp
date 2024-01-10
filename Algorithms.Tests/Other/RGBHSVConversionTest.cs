using System;
using Algorithms.Other;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Other;

public static class RgbHsvConversionTest
{
    [Test]
    public static void HueOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        Action act = () => RgbHsvConversion.HsvToRgb(400, 0, 0);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public static void SaturationOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        Action act = () => RgbHsvConversion.HsvToRgb(0, 2, 0);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public static void ValueOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        Action act = () => RgbHsvConversion.HsvToRgb(0, 0, 2);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    // expected RGB-values taken from https://www.rapidtables.com/convert/color/hsv-to-rgb.html
    [TestCase(0, 0, 0, 0, 0, 0)]
    [TestCase(0, 0, 1, 255, 255, 255)]
    [TestCase(0, 1, 1, 255, 0, 0)]
    [TestCase(60, 1, 1, 255, 255, 0)]
    [TestCase(120, 1, 1, 0, 255, 0)]
    [TestCase(240, 1, 1, 0, 0, 255)]
    [TestCase(300, 1, 1, 255, 0, 255)]
    [TestCase(180, 0.5, 0.5, 64, 128, 128)]
    [TestCase(234, 0.14, 0.88, 193, 196, 224)]
    [TestCase(330, 0.75, 0.5, 128, 32, 80)]
    public static void TestRgbOutput(
        double hue,
        double saturation,
        double value,
        byte expectedRed,
        byte exptectedGreen,
        byte exptectedBlue)
    {
        var rgb = RgbHsvConversion.HsvToRgb(hue, saturation, value);
        rgb.Item1.Should().Be(expectedRed);
        rgb.Item2.Should().Be(exptectedGreen);
        rgb.Item3.Should().Be(exptectedBlue);
    }

    // Parameters of test-cases for TestRGBOutput reversed
    [TestCase(0, 0, 0, 0, 0, 0)]
    [TestCase(255, 255, 255, 0, 0, 1)]
    [TestCase(255, 0, 0, 0, 1, 1)]
    [TestCase(255, 255, 0, 60, 1, 1)]
    [TestCase(0, 255, 0, 120, 1, 1)]
    [TestCase(0, 0, 255, 240, 1, 1)]
    [TestCase(255, 0, 255, 300, 1, 1)]
    [TestCase(64, 128, 128, 180, 0.5, 0.5)]
    [TestCase(193, 196, 224, 234, 0.14, 0.88)]
    [TestCase(128, 32, 80, 330, 0.75, 0.5)]
    public static void TestHsvOutput(
        byte red,
        byte green,
        byte blue,
        double expectedHue,
        double expectedSaturation,
        double expectedValue)
    {
        var hsv = RgbHsvConversion.RgbToHsv(red, green, blue);

        // approximate-assertions needed because of small deviations due to converting between byte-values and double-values.
        hsv.Item1.Should().BeApproximately(expectedHue, 0.2);
        hsv.Item2.Should().BeApproximately(expectedSaturation, 0.002);
        hsv.Item3.Should().BeApproximately(expectedValue, 0.002);
    }
}
