using System;
using System.Globalization;
using Algorithms.Other;
using NUnit.Framework;

namespace Algorithms.Tests.Other;

/// <summary>
///     A class for testing the Meeus's Julian Easter algorithm.
/// </summary>
public static class JulianEasterTest
{
    [TestCaseSource(nameof(CalculateCases))]
    public static void CalculateTest(int year, DateTime expected)
    {
        var result = JulianEaster.Calculate(year);

        Assert.That(result, Is.EqualTo(expected));
    }

    private static readonly object[] CalculateCases =
    {
        new object[] { 1800, new DateTime(1800, 04, 08, 00, 00, 00, DateTimeKind.Utc) },
        new object[] { 1950, new DateTime(1950, 03, 27, 00, 00, 00, DateTimeKind.Utc) },
        new object[] { 1991, new DateTime(1991, 03, 25, 00, 00, 00, DateTimeKind.Utc) },
        new object[] { 2000, new DateTime(2000, 04, 17, 00, 00, 00, DateTimeKind.Utc) },
        new object[] { 2199, new DateTime(2199, 04, 07, 00, 00, 00, DateTimeKind.Utc) }
    };

}
