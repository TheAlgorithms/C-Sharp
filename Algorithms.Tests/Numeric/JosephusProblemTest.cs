using System;
using Algorithms.Numeric;
using NUnit.Framework;


namespace Algorithms.Tests.Numeric;

public class JosephusProblemTest
{

    [TestCase(10, 0)]
    [TestCase(10, -1)]
    public void JosephusProblemInvalidStepSize(long groupSize, long step)
    {
        Assert.Throws(Is.TypeOf<ArgumentException>()
                .And.Message.EqualTo("The step cannot be smaller than 1"),
            delegate { JosephusProblem.FindWinner(groupSize, step); });
    }

    [TestCase(10, 12)]
    public void JosephusProblemStepSizeGreaterThanGroup(long groupSize, long step)
    {
        Assert.Throws(Is.TypeOf<ArgumentException>()
                .And.Message.EqualTo("The step cannot be greater than the size of the group"),
            delegate { JosephusProblem.FindWinner(groupSize, step); });
    }

    [TestCase(10, 2, 5)]
    [TestCase(10, 8, 1)]
    [TestCase(254, 18, 92)]
    [TestCase(3948, 614, 2160)]
    [TestCase(86521, 65903, 29473)]
    public void JosephusProblemWinnerCalculation(long groupSize, long step, long position)
    {
        Assert.That(JosephusProblem.FindWinner(groupSize, step), Is.EqualTo(position));
    }
}
