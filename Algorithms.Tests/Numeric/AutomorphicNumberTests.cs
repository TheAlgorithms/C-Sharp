using Algorithms.Numeric;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Algorithms.Tests.Numeric;

public class AutomorphicNumberTests
{
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(25)]
    [TestCase(76)]
    [TestCase(376)]
    [TestCase(625)]
    [TestCase(9376)]
    [TestCase(90625)]
    [TestCase(109376)]
    
    public void TestAutomorphicNumbers(int number)
    {
        Assert.That(AutomorphicNumber.IsAutomorphic(number), Is.True);
    }

    [TestCase(2)]
    [TestCase(3)]
    [TestCase(7)]
    [TestCase(18)]
    [TestCase(79)]
    [TestCase(356)]
    [TestCase(623)]
    [TestCase(9876)]
    [TestCase(90635)]
    [TestCase(119376)]
    [TestCase(891625)]
    [TestCase(2990625)]
    [TestCase(7209376)]
    [TestCase(12891625)]
    [TestCase(87129396)]
    public void TestNonAutomorphicNumbers(int number)
    {
        Assert.That(AutomorphicNumber.IsAutomorphic(number), Is.False);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void TestInvalidAutomorphicNumbers(int number)
    {
        Assert.Throws(Is.TypeOf<ArgumentException>()
            .And.Message.EqualTo($"An automorphic number must always be positive."),
            delegate
            {
                AutomorphicNumber.IsAutomorphic(number);
            });
    }

    [TestCase(1, 100)]
    public void TestAutomorphicNumberSequence(int lower, int upper)
    {
        List<long> automorphicList = new() { 1, 5, 6, 25, 76 };
        Assert.That(AutomorphicNumber.GetAutomorphicNumbers(lower, upper), Is.EqualTo(automorphicList));
    }

    [TestCase(8, 12)]
    public void TestNoAutomorphicNumberInTheSequence(int lower, int upper)
    {
        List<long> automorphicList = new();
        Assert.That(AutomorphicNumber.GetAutomorphicNumbers(lower, upper), Is.EqualTo(automorphicList));
    }

    [TestCase(25,25)]
    public void TestAutomorphicNumberSequenceSameBounds(int lower, int upper)
    {
        List<long> automorphicList = new() { 25 };
        Assert.That(AutomorphicNumber.GetAutomorphicNumbers(lower, upper), Is.EqualTo(automorphicList));
    }

    [TestCase(-1,1)]
    [TestCase(0, 1)]
    public void TestAutomorphicNumberSequenceInvalidLowerBound(int lower, int upper)
    {
        Assert.Throws(Is.TypeOf<ArgumentException>()
            .And.Message.EqualTo($"Lower bound must be greater than 0."),
            delegate
            {
                AutomorphicNumber.GetAutomorphicNumbers(lower, upper);
            });
    }

    [TestCase(1, -1)]
    [TestCase(10, -1)]
    public void TestAutomorphicNumberSequenceInvalidUpperBound(int lower, int upper)
    {
        Assert.Throws(Is.TypeOf<ArgumentException>()
            .And.Message.EqualTo($"Upper bound must be greater than 0."),
            delegate
            {
                AutomorphicNumber.GetAutomorphicNumbers(lower, upper);
            });
    }

    [TestCase(25, 2)]
    public void TestAutomorphicNumberSequenceReversedBounds(int lower, int upper)
    {
        Assert.Throws(Is.TypeOf<ArgumentException>()
            .And.Message.EqualTo($"The lower bound must be less than or equal to the upper bound."),
            delegate
            {
                AutomorphicNumber.GetAutomorphicNumbers(lower, upper);
            });
    }
}
