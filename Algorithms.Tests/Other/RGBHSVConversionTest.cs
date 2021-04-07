using System;
using NUnit.Framework;
using FluentAssertions;

namespace Algorithms.Other
{
    public static class RGBHSVConversionTest
    {
        [Test]
        public static void HueOutOfRange_ThrowsArgumentOutOfRangeException()
        {
            Action act = () => Algorithms.Other.RGBHSVConversion.HSVToRGB(400, 0, 0, out byte red, out byte green, out byte blue);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public static void SaturationOutOfRange_ThrowsArgumentOutOfRangeException()
        {
            Action act = () => Algorithms.Other.RGBHSVConversion.HSVToRGB(0, 2, 0, out byte red, out byte green, out byte blue);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public static void ValueOutOfRange_ThrowsArgumentOutOfRangeException()
        {
            Action act = () => Algorithms.Other.RGBHSVConversion.HSVToRGB(0, 0, 2, out byte red, out byte green, out byte blue);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        // expected RGB-values taken from https://www.rapidtables.com/convert/color/hsv-to-rgb.html
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0, 0, 1, 255, 255, 255)]
        [TestCase(0, 1, 1, 255, 0, 0)]
        [TestCase(60, 1, 1, 255, 255, 0)]
        [TestCase(120, 1, 1, 0, 255, 0)]
        [TestCase(180, 0.5, 0.5, 64, 128, 128)]
        [TestCase(234, 0.14, 0.88, 193, 196, 224)]
        public static void TestRGBOutput(
            double hue,
            double saturation,
            double value,
            byte expectedRed,
            byte exptectedGreen,
            byte exptectedBlue)
        {
            Algorithms.Other.RGBHSVConversion.HSVToRGB(hue, saturation, value, out byte red, out byte green, out byte blue);
            red.Should().Be(expectedRed);
            green.Should().Be(exptectedGreen);
            blue.Should().Be(exptectedBlue);
        }

        // Parameters of test-cases for TestRGBOutput reversed
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(255, 255, 255, 0, 0, 1)]
        [TestCase(255, 0, 0, 0, 1, 1)]
        [TestCase(255, 255, 0, 60, 1, 1)]
        [TestCase(0, 255, 0, 120, 1, 1)]
        [TestCase(64, 128, 128, 180, 0.5, 0.5)]
        [TestCase(193, 196, 224, 234, 0.14, 0.88)]
        public static void TestHSVOutput(
            byte red,
            byte green,
            byte blue,
            double expectedHue,
            double expectedSaturation,
            double expectedValue)
        {
            Algorithms.Other.RGBHSVConversion.RGBToHSV(red, green, blue, out double hue, out double saturation, out double value);

            // approximate-assertions needed because of small deviations due to converting between byte-values and double-values.
            hue.Should().BeApproximately(expectedHue, 0.2);
            saturation.Should().BeApproximately(expectedSaturation, 0.002);
            value.Should().BeApproximately(expectedValue, 0.002);
        }
    }
}