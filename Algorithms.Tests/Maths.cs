using Algorithms.Maths;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Tests.Maths
{
    public class EuclideanDistanceTests
    {
        [Test]
        public void EuclideanDistanceTest1()
        {
            var point1 = new double[] { -7, -4 };
            var point2 = new double[] { 17, 6.5 };

            var distance = EuclideanDistance.Distance(point1, point2);

            // distance should be 26.196374.
            distance.Should().BeApproximately(26.196374, 0.00001);
        }

        [Test]
        public void EuclideanDistanceTest2()
        {
            var point1 = new double[] { 1, 4, -3.5 };
            var point2 = new double[] { 10, -5, 2 };

            var distance = EuclideanDistance.Distance(point1, point2);

            // distance should be 13.865424...
            distance.Should().BeApproximately(13.865424, 0.00001);
        }

        [Test]
        public void EuclideanDistance_ShouldThrowArgumentException()
        {
            var point1 = new double[] { 1, 2, 3 };
            var point2 = new double[] { 1, 2 };

            Action action = () => EuclideanDistance.Distance(point1, point2);

            // Should throw argumentException as long as points have different dimensions. 
            action.Should().Throw<ArgumentException>();
        }
    }
}
