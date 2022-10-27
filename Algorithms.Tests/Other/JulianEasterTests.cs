using System;
using System.Globalization;
using Algorithms.Other;
using NUnit.Framework;

namespace Algorithms.Tests.Other
{
    /// <summary>
    ///     A class for testing the Meeus's Julian Easter algorithm.
    /// </summary>
    public static class JulianEasterTest
    {
        private static readonly JulianCalendar Calendar = new();

        [TestCaseSource(nameof(CalculateCases))]
        public static void CalculateTest(int year, DateTime expected)
        {
            var result = JulianEaster.Calculate(year);

            Assert.AreEqual(expected, result);
        }

        private static readonly object[] CalculateCases =
        {
            new object[] { 1800, new DateTime(1800, 04, 08, Calendar) },
            new object[] { 1950, new DateTime(1950, 03, 27, Calendar) },
            new object[] { 1991, new DateTime(1991, 03, 25, Calendar) },
            new object[] { 2000, new DateTime(2000, 04, 17, Calendar) },
            new object[] { 2199, new DateTime(2199, 04, 07, Calendar) }
        };
    }
}
