using System;
using System.Globalization;

namespace Algorithms.Other;

/// <summary>
///     Date of Easter calculated with Meeus's Julian algorithm.
///     The algorithm is described in Jean Meeus' <a href="https://archive.org/details/astronomicalalgorithmsjeanmeeus1991/page/n73/mode/2up">Astronomical Algorithms (1991, p. 69)</a>.
/// </summary>
public static class JulianEaster
{
    /// <summary>
    ///     Calculates the date of Easter.
    /// </summary>
    /// <param name="year">Year to calculate the date of Easter.</param>
    /// <returns>Date of Easter as a DateTime.</returns>
    public static DateTime Calculate(int year)
    {
        var a = year % 4;
        var b = year % 7;
        var c = year % 19;
        var d = (19 * c + 15) % 30;
        var e = (2 * a + 4 * b - d + 34) % 7;
        var month = (int)Math.Floor((d + e + 114) / 31M);
        var day = ((d + e + 114) % 31) + 1;

        DateTime easter = new(year, month, day, 00, 00, 00, DateTimeKind.Utc);

        return easter;
    }
}
