using System;
using System.Collections.Generic;
using Algorithms.Financial;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Financial;

public static class PresentValueTests
{
    [Test]
    public static void Present_Value_General_Tests()
    {
        PresentValue.Calculate(0.13, [10.0, 20.70, -293.0, 297.0])
            .Should()
            .Be(4.69);

        PresentValue.Calculate(0.07, [-109129.39, 30923.23, 15098.93, 29734.0, 39.0])
            .Should()
            .Be(-42739.63);

        PresentValue.Calculate(0.07, [109129.39, 30923.23, 15098.93, 29734.0, 39.0])
            .Should()
            .Be(175519.15);

        PresentValue.Calculate(0.0, [109129.39, 30923.23, 15098.93, 29734.0, 39.0])
            .Should()
            .Be(184924.55);
    }

    [Test]
    public static void Present_Value_Exception_Tests()
    {
        Assert.Throws<ArgumentException>(() => PresentValue.Calculate(-1.0, [10.0, 20.70, -293.0, 297.0]));
        Assert.Throws<ArgumentException>(() => PresentValue.Calculate(1.0, []));
    }
}
