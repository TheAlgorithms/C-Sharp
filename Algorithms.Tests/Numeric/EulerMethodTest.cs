using System;
using Algorithms.Numeric;
using NUnit.Framework;
using System.Collections.Generic; 

namespace Algorithms.Tests.Numeric
{
    public class EulerMethodTest
    {
        [Test]
        public static void test1()
        {
            Func<double, double, double> exampleEquation1 = (x, y) => x;
            List<double[]> points =  Algorithms.Numeric.EulerMethod.eulerFull(0, 4, 0.1, 0, exampleEquation1);
            double yEnd = points[points.Count - 1][1];
            Assert.AreEqual(yEnd, 7.800000000000003);
        }
        
        [Test]
        public static void test2()
        {
            // example from https://en.wikipedia.org/wiki/Euler_method
            Func<double, double, double> exampleEquation2 = (x, y) => y;
            List<double[]> points =  Algorithms.Numeric.EulerMethod.eulerFull(0, 4, 0.1, 1, exampleEquation2);
            double yEnd = points[points.Count - 1][1];
            Assert.AreEqual(yEnd, 45.25925556817596);
        }
        
        [Test]
        public static void test3()
        {
            // example from https://www.geeksforgeeks.org/euler-method-solving-differential-equation/
            Func<double, double, double> exampleEquation3 = (x, y) => x + y + x * y;
            List<double[]> points =  Algorithms.Numeric.EulerMethod.eulerFull(0, 0.1, 0.025, 1, exampleEquation3);
            double yEnd = points[points.Count - 1][1];
            Assert.AreEqual(yEnd, 1.1116729841674804);
        }
    }
}