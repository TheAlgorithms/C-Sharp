using System;
using System.Collections.Generic;
using DataStructures.Hashing.NumberTheory;
using NUnit.Framework;

namespace DataStructures.Tests.Hashing.NumberTheory;

[TestFixture]
public static class PrimeNumberTests
{
    private static readonly object[] IsPrimeSource =
    {
        new object[] { 0, false },
        new object[] { 1, false },
        new object[] { 2, true },
        new object[] { 3, true },
        new object[] { 4, false },
        new object[] { 5, true },
        new object[] { 6, false },
        new object[] { 7, true },
        new object[] { 8, false },
        new object[] { 9, false },
        new object[] { 10, false },
        new object[] { 11, true },
        new object[] { 12, false },
        new object[] { 13, true },
        new object[] { 14, false },
        new object[] { 15, false },
        new object[] { 16, false },
        new object[] { 17, true },
        new object[] { 18, false },
        new object[] { 19, true },
        new object[] { 20, false },
        new object[] { 21, false },
        new object[] { 22, false },
        new object[] { 23, true },
        new object[] { 24, false },
        new object[] { 25, false },
        new object[] { 26, false },
        new object[] { 27, false },
        new object[] { 28, false },
        new object[] { 29, true },
        new object[] { 30, false },
        new object[] { 31, true },
        new object[] { 32, false },
        new object[] { 33, false },
        new object[] { 34, false },
        new object[] { 35, false },
        new object[] { 36, false },
        new object[] { 37, true },
        new object[] { 38, false },
        new object[] { 39, false },
        new object[] { 40, false },
    };

    private static readonly object[] NextPrimeSource =
    {
        new object[] { 0, 1, false, 2 },
        new object[] { 1, 1, false, 2 },
        new object[] { 3, 1, false, 5 },
        new object[] { 4, 1, false, 5 },
        new object[] { 5, 1, false, 7 },
        new object[] { 6, 1, false, 7 },
        new object[] { 7, 1, false, 11 },
        new object[] { 8, 1, false, 11 },
        new object[] { 9, 1, false, 11 },
        new object[] { 10, 1, false, 11 },
        new object[] { 11, 1, false, 13 },
        new object[] { 12, 1, false, 13 },
        new object[] { 13, 1, false, 17 },
        new object[] { 14, 1, false, 17 },
        new object[] { 15, 1, false, 17 },
        new object[] { 16, 1, false, 17 },
        new object[] { 17, 1, false, 19 },
        new object[] { 18, 1, false, 19 },
        new object[] { 19, 1, false, 23 },
        new object[] { 20, 1, false, 23 },
        new object[] { 21, 1, false, 23 },
        new object[] { 22, 1, false, 23 },
        new object[] { 23, 1, false, 29 },
        new object[] { 24, 1, false, 29 },
        new object[] { 25, 1, false, 29 },
        new object[] { 26, 1, false, 29 },
        new object[] { 27, 1, false, 29 },
        new object[] { 28, 1, false, 29 },
        new object[] { 29, 1, false, 31 },
        new object[] { 4, 1, true, 3 },
        new object[] { 5, 1, true, 3 },
        new object[] { 6, 1, true, 5 },
        new object[] { 7, 1, true, 5 },
        new object[] { 8, 1, true, 7 },
        new object[] { 9, 1, true, 7 },
        new object[] { 10, 1, true, 7 }
    };

    [TestCaseSource(nameof(IsPrimeSource))]
    public static void IsPrimeTest(int number, bool expected)
    {
        var actual = PrimeNumber.IsPrime(number);
        Assert.That(expected, Is.EqualTo(actual));
    }

    [TestCaseSource(nameof(NextPrimeSource))]
    public static void NextPrimeTest(int number, int factor, bool desc, int expected)
    {
        var actual = PrimeNumber.NextPrime(number, factor, desc);
        Assert.That(expected, Is.EqualTo(actual));
    }
}
