using Algorithms.Other;
using NUnit.Framework;

namespace Algorithms.Tests.Other;

public class PollardsRhoFactorizingTests
{
    [TestCase(8051, 97)]
    [TestCase(105, 21)]
    [TestCase(253, 11)]
    [TestCase(10403, 101)]
    [TestCase(187, 11)]
    public void SimpleTest(int number, int expectedResult)
    {
        var result = PollardsRhoFactorizing.Calculate(number);
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
