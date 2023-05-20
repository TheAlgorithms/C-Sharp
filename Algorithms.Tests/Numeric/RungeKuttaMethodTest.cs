using Algorithms.Numeric;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Algorithms.Tests.Numeric
{
    public static class RungeKuttaTest
    {
        [Test]
        public static void TestLinearEquation()
        {
            Func<double, double, double> exampleEquation = (x, _) => x;
            List<double[]> points = RungeKuttaMethod.ClassicRungeKuttaMethod(0, 4, 0.001, 0, exampleEquation);
            var yEnd = points[^1][1];
            yEnd.Should().BeApproximately(8, 0.01);
        }

        [Test]
        public static void TestExampleFunciton()
        {
            Func<double, double, double> exampleEquation = (_, y) => y;
            List<double[]> points = RungeKuttaMethod.ClassicRungeKuttaMethod(0, 4, 0.0125, 1, exampleEquation);
            var yEnd = points[^1][1];
            yEnd.Should().BeApproximately(54.598, 0.0005);
        }

        [Test]
        public static void StepsizeIsZeroOrNegative_ThrowsArgumentOutOfRangeException()
        {
            Func<double, double, double> exampleEquation = (x, _) => x;
            Assert.Throws<ArgumentOutOfRangeException>(() => RungeKuttaMethod.ClassicRungeKuttaMethod(0, 4, 0, 0, exampleEquation));
        }

        [Test]
        public static void StartIsLargerThanEnd_ThrowsArgumentOutOfRangeException()
        {
            Func<double, double, double> exampleEquation = (x, _) => x;
            Assert.Throws<ArgumentOutOfRangeException>(() => RungeKuttaMethod.ClassicRungeKuttaMethod(0, -4, 0.1, 0, exampleEquation));
        }
    }
}
