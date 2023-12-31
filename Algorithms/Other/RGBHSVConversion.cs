using System;

namespace Algorithms.Other;

/// <summary>
///     The RGB color model is an additive color model in which red, green, and
///     blue light are added together in various ways to reproduce a broad array of
///     colors. The name of the model comes from the initials of the three additive
///     primary colors, red, green, and blue. Meanwhile, the HSV representation
///     models how colors appear under light. In it, colors are represented using
///     three components: hue, saturation and (brightness-)value. This class
///     provides methods for converting colors from one representation to the other.
///     (description adapted from https://en.wikipedia.org/wiki/RGB_color_model and
///     https://en.wikipedia.org/wiki/HSL_and_HSV).
/// </summary>
public static class RgbHsvConversion
{
    /// <summary>
    ///     Conversion from the HSV-representation to the RGB-representation.
    /// </summary>
    /// <param name="hue">Hue of the color.</param>
    /// <param name="saturation">Saturation of the color.</param>
    /// <param name="value">Brightness-value of the color.</param>
    /// <returns>The tuple of RGB-components.</returns>
    public static (byte red, byte green, byte blue) HsvToRgb(
        double hue,
        double saturation,
        double value)
    {
        if (hue < 0 || hue > 360)
        {
            throw new ArgumentOutOfRangeException(nameof(hue), $"{nameof(hue)} should be between 0 and 360");
        }

        if (saturation < 0 || saturation > 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(saturation),
                $"{nameof(saturation)} should be between 0 and 1");
        }

        if (value < 0 || value > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(value)} should be between 0 and 1");
        }

        var chroma = value * saturation;
        var hueSection = hue / 60;
        var secondLargestComponent = chroma * (1 - Math.Abs(hueSection % 2 - 1));
        var matchValue = value - chroma;

        return GetRgbBySection(hueSection, chroma, matchValue, secondLargestComponent);
    }

    /// <summary>
    ///     Conversion from the RGB-representation to the HSV-representation.
    /// </summary>
    /// <param name="red">Red-component of the color.</param>
    /// <param name="green">Green-component of the color.</param>
    /// <param name="blue">Blue-component of the color.</param>
    /// <returns>The tuple of HSV-components.</returns>
    public static (double hue, double saturation, double value) RgbToHsv(
        byte red,
        byte green,
        byte blue)
    {
        var dRed = (double)red / 255;
        var dGreen = (double)green / 255;
        var dBlue = (double)blue / 255;
        var value = Math.Max(Math.Max(dRed, dGreen), dBlue);
        var chroma = value - Math.Min(Math.Min(dRed, dGreen), dBlue);
        var saturation = value.Equals(0) ? 0 : chroma / value;
        double hue;

        if (chroma.Equals(0))
        {
            hue = 0;
        }
        else if (value.Equals(dRed))
        {
            hue = 60 * (0 + (dGreen - dBlue) / chroma);
        }
        else if (value.Equals(dGreen))
        {
            hue = 60 * (2 + (dBlue - dRed) / chroma);
        }
        else
        {
            hue = 60 * (4 + (dRed - dGreen) / chroma);
        }

        hue = (hue + 360) % 360;

        return (hue, saturation, value);
    }

    private static (byte red, byte green, byte blue) GetRgbBySection(
        double hueSection,
        double chroma,
        double matchValue,
        double secondLargestComponent)
    {
        byte red;
        byte green;
        byte blue;

        if (hueSection >= 0 && hueSection <= 1)
        {
            red = ConvertToByte(chroma + matchValue);
            green = ConvertToByte(secondLargestComponent + matchValue);
            blue = ConvertToByte(matchValue);
        }
        else if (hueSection > 1 && hueSection <= 2)
        {
            red = ConvertToByte(secondLargestComponent + matchValue);
            green = ConvertToByte(chroma + matchValue);
            blue = ConvertToByte(matchValue);
        }
        else if (hueSection > 2 && hueSection <= 3)
        {
            red = ConvertToByte(matchValue);
            green = ConvertToByte(chroma + matchValue);
            blue = ConvertToByte(secondLargestComponent + matchValue);
        }
        else if (hueSection > 3 && hueSection <= 4)
        {
            red = ConvertToByte(matchValue);
            green = ConvertToByte(secondLargestComponent + matchValue);
            blue = ConvertToByte(chroma + matchValue);
        }
        else if (hueSection > 4 && hueSection <= 5)
        {
            red = ConvertToByte(secondLargestComponent + matchValue);
            green = ConvertToByte(matchValue);
            blue = ConvertToByte(chroma + matchValue);
        }
        else
        {
            red = ConvertToByte(chroma + matchValue);
            green = ConvertToByte(matchValue);
            blue = ConvertToByte(secondLargestComponent + matchValue);
        }

        return (red, green, blue);
    }

    private static byte ConvertToByte(double input) => (byte)Math.Round(255 * input);
}
