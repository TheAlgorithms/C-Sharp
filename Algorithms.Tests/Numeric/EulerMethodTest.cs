using System;
using Algorithms.Numeric;
using NUnit.Framework;
using System.Collections.Generic; 
using FluentAssertions;

namespace Algorithms.Tests.Numeric
{
    public static class EulerMethodTest
    {
        [Test]
        public static void TestLinearEquation()
        {
            Func<double, double, double> exampleEquation = (x, y) => x;
            List<double[]> points =  Algorithms.Numeric.EulerMethod.EulerFull(0, 4, 0.001, 0, exampleEquation);
            double yEnd = points[points.Count - 1][1];
            yEnd.Should().BeApproximately(8, 0.01);
        }
        
        [Test]
        public static void TestExampleWikipedia()
        {
            // example from https://en.wikipedia.org/wiki/Euler_method
            Func<double, double, double> exampleEquation = (x, y) => y;
            List<double[]> points =  Algorithms.Numeric.EulerMethod.EulerFull(0, 4, 0.0125, 1, exampleEquation);
            double yEnd = points[points.Count - 1][1];
            yEnd.Should().BeApproximately(53.26, 0.01);
        }
        
        [Test]
        public static void TestExampleGeeksForGeeks()
        {
            // example from https://www.geeksforgeeks.org/euler-method-solving-differential-equation/
            // Euler method: y_n+1 = y_n + stepSize * f(x_n, y_n)
            // differential equation: f(x, y) = x + y + x * y
            // initial conditions: x_0 = 0; y_0 = 1; stepSize = 0.025
            // solution:
            //     y_1 = 1 + 0.025 * (0 + 1 + 0 * 1) = 1.025
            //     y_2 = 1.025 + 0.025 * (0.025 + 1.025 + 0.025 * 1.025) = 1.051890625
            Func<double, double, double> exampleEquation = (x, y) => x + y + x * y;
            List<double[]> points =  Algorithms.Numeric.EulerMethod.EulerFull(0, 0.05, 0.025, 1, exampleEquation);
            double y_1 = points[1][1];
            double y_2 = points[2][1];
            Assert.AreEqual(y_1, 1.025);
            Assert.AreEqual(y_2, 1.051890625);
        }
        
        [Test]
        public static void StepsizeIsZeroOrNegative_ThrowsArgumentOutOfRangeException()
        {
            Func<double, double, double> exampleEquation = (x, y) => x;
            Assert.Throws<ArgumentOutOfRangeException>(() => Algorithms.Numeric.EulerMethod.EulerFull(0, 4, 0, 0, exampleEquation));
        }
        
        [Test]
        public static void StartIsLargerThanEnd_ThrowsArgumentOutOfRangeException()
        {
            Func<double, double, double> exampleEquation = (x, y) => x;
            Assert.Throws<ArgumentOutOfRangeException>(() => Algorithms.Numeric.EulerMethod.EulerFull(0, -4, 0.1, 0, exampleEquation));
        }
    }
}