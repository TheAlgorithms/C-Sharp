using System;
using System.Numerics;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric;

public class NewtonSquareRootTests
{
    private static readonly object[] CalculateSquareRootInput =
    {
        new object[] {BigInteger.One, BigInteger.One},
        new object[] {new BigInteger(221295376), new BigInteger(14876)},
        new object[] {new BigInteger(2530995481), new BigInteger(50309)},
        new object[] {new BigInteger(3144293476), new BigInteger(56074)},
        new object[] {new BigInteger(3844992064), new BigInteger(62008)},
        new object[] {new BigInteger(5301150481), new BigInteger(72809)},
        new object[] {new BigInteger(5551442064), new BigInteger(74508)},
        new object[] {new BigInteger(6980435401), new BigInteger(83549)},
        new object[] {new BigInteger(8036226025), new BigInteger(89645)},
    };

    [TestCaseSource(nameof(CalculateSquareRootInput))]
    public void CalculateSquareRootTest(BigInteger number, BigInteger result)
    {
        Assert.That(NewtonSquareRoot.Calculate(number), Is.EqualTo(result));
    }

    [Test]
    public void CalculateSquareRootOfZero()
    {
        Assert.That(NewtonSquareRoot.Calculate(0), Is.EqualTo(BigInteger.Zero));
    }

    [Test]
    public void CalculateSquareRootNegativeNumber()
    {
        Assert.Throws(Is.TypeOf<ArgumentException>()
            .And.Message.EqualTo("Cannot calculate the square root of a negative number."),
            delegate
            {
                NewtonSquareRoot.Calculate(BigInteger.MinusOne);
            });
    }
}
