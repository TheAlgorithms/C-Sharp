using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Financial;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Financial;

public static class PresentValueTests
{
    [TestCase(0.13,new[] { 10.0, 20.70, -293.0, 297.0 },4.69)]
    [TestCase(0.07,new[] { -109129.39, 30923.23, 15098.93, 29734.0, 39.0 }, -42739.63)]
    [TestCase(0.07, new[] { 109129.39, 30923.23, 15098.93, 29734.0, 39.0 }, 175519.15)]
    [TestCase(0.0, new[] { 109129.39, 30923.23, 15098.93, 29734.0, 39.0 }, 184924.55)]

    public static void Present_Value_General_Tests(double discountRate,double[] cashFlow ,double expected)
    =>
        PresentValue.Calculate(discountRate, cashFlow.ToList())
           .Should()
           .Be(expected);


    [TestCase(-1.0, new[] { 10.0, 20.70, -293.0, 297.0 })]
    [TestCase(1.0,new double[] {})]

    public static void Present_Value_Exception_Tests(double discountRate, double[] cashFlow)
    => Assert.Throws<ArgumentException>(() => PresentValue.Calculate(discountRate, cashFlow.ToList()));
}
