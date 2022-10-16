using Algorithms.Other;
using NUnit.Framework;

namespace Algorithms.Tests.Other
{
    public class WelfordsVarianceTest
    {
        [Test]
        public void WelfordVariance_Example1()
        {
            var welfordsVariance = new WelfordsVariance();
            welfordsVariance.AddValue(4);
            welfordsVariance.AddValue(7);
            welfordsVariance.AddValue(13);
            welfordsVariance.AddValue(16);

            Assert.AreEqual(4, welfordsVariance.Count);
            Assert.AreEqual(10, welfordsVariance.Mean, 0.0000001);
            Assert.AreEqual(22.5, welfordsVariance.Variance, 0.0000001);
            Assert.AreEqual(30, welfordsVariance.SampleVariance, 0.0000001);
        }

        [Test]
        public void WelfordVariance_Example2()
        {
            var stats = new WelfordsVariance();
            stats.AddValue(100000004);
            stats.AddValue(100000007);
            stats.AddValue(100000013);
            stats.AddValue(100000016);
            Assert.AreEqual(4, stats.Count);
            Assert.AreEqual(100000010, stats.Mean, 0.0000001);
            Assert.AreEqual(22.5, stats.Variance, 0.0000001);
            Assert.AreEqual(30, stats.SampleVariance, 0.0000001);
        }

        [Test]
        public void WelfordVariance_Example3()
        {
            var stats = new WelfordsVariance();
            stats.AddValue(1000000004);
            stats.AddValue(1000000007);
            stats.AddValue(1000000013);
            stats.AddValue(1000000016);
            Assert.AreEqual(4, stats.Count);
            Assert.AreEqual(1000000010, stats.Mean, 0.0000001);
            Assert.AreEqual(22.5, stats.Variance, 0.0000001);
            Assert.AreEqual(30, stats.SampleVariance, 0.0000001);
        }

        [Test]
        public void WelfordVariance_Example4()
        {
            var stats = new WelfordsVariance();
            stats.AddValue(6);
            stats.AddValue(2);
            stats.AddValue(3);
            stats.AddValue(1);
            Assert.AreEqual(4, stats.Count);
            Assert.AreEqual(3, stats.Mean, 0.0000001);
            Assert.AreEqual(3.5, stats.Variance, 0.0000001);
            Assert.AreEqual(4.6666667, stats.SampleVariance, 0.0000001);
        }

        [Test]
        public void WelfordVariance_Example5()
        {
            var stats = new WelfordsVariance(new double[] { 2, 2, 5, 7 });
            Assert.AreEqual(4, stats.Count);
            Assert.AreEqual(4, stats.Mean, 0.0000001);
            Assert.AreEqual(4.5, stats.Variance, 0.0000001);
            Assert.AreEqual(6, stats.SampleVariance, 0.0000001);
        }

        [Test]
        public void WelfordVariance_Example6()
        {
            var stats = new WelfordsVariance();
            stats.AddRange(new double[] { 2, 4, 4, 4, 5, 5, 7, 9 });
            Assert.AreEqual(8, stats.Count);
            Assert.AreEqual(5, stats.Mean, 0.0000001);
            Assert.AreEqual(4, stats.Variance, 0.0000001);
            Assert.AreEqual(4.5714286, stats.SampleVariance, 0.0000001);
        }

        [Test]
        public void WelfordVariance_Example7()
        {
            var stats = new WelfordsVariance();
            stats.AddRange(new double[] { 9, 2, 5, 4, 12, 7, 8, 11, 9, 3, 7, 4, 12, 5, 4, 10, 9, 6, 9, 4 });
            Assert.AreEqual(20, stats.Count);
            Assert.AreEqual(7, stats.Mean, 0.0000001);
            Assert.AreEqual(8.9, stats.Variance, 0.0000001);
            Assert.AreEqual(9.3684211, stats.SampleVariance, 0.0000001);
        }

        [Test]
        public void WelfordVariance_Example8()
        {
            var stats = new WelfordsVariance();
            stats.AddRange(new [] { 51.3, 55.6, 49.9, 52.0 });
            Assert.AreEqual(4, stats.Count);
            Assert.AreEqual(52.2, stats.Mean, 0.0000001);
            Assert.AreEqual(4.4250000, stats.Variance, 0.0000001);
            Assert.AreEqual(5.9000000, stats.SampleVariance, 0.0000001);
        }

        [Test]
        public void WelfordVariance_Example9()
        {
            var stats = new WelfordsVariance();
            stats.AddRange(new double[] { -5, -3, -1, 1, 3 });
            Assert.AreEqual(5, stats.Count);
            Assert.AreEqual(-1, stats.Mean, 0.0000001);
            Assert.AreEqual(8, stats.Variance, 0.0000001);
            Assert.AreEqual(10, stats.SampleVariance, 0.0000001);
        }

        [Test]
        public void WelfordVariance_Example10()
        {
            var stats = new WelfordsVariance();
            stats.AddRange(new double[] { -1, 0, 1 });
            Assert.AreEqual(3, stats.Count);
            Assert.AreEqual(0, stats.Mean, 0.0000001);
            Assert.AreEqual(0.6666667, stats.Variance, 0.0000001);
            Assert.AreEqual(1, stats.SampleVariance, 0.0000001);
        }

        [Test]
        public void WelfordVariance_NoValue()
        {
            var stats = new WelfordsVariance();
            Assert.AreEqual(0, stats.Count);
            Assert.AreEqual(double.NaN, stats.Mean);
            Assert.AreEqual(double.NaN, stats.Variance);
            Assert.AreEqual(double.NaN, stats.SampleVariance);
        }

        [Test]
        public void WelfordVariance_OneValue()
        {
            var stats = new WelfordsVariance();
            stats.AddValue(1);
            Assert.AreEqual(1, stats.Count);
            Assert.AreEqual(double.NaN, stats.Mean);
            Assert.AreEqual(double.NaN, stats.Variance);
            Assert.AreEqual(double.NaN, stats.SampleVariance);
        }

        [Test]
        public void WelfordVariance_TwoValues()
        {
            var stats = new WelfordsVariance();
            stats.AddValue(1);
            stats.AddValue(2);
            Assert.AreEqual(2, stats.Count);
            Assert.AreEqual(1.5, stats.Mean, 0.0000001);
            Assert.AreEqual(0.25, stats.Variance, 0.0000001);
            Assert.AreEqual(0.5, stats.SampleVariance, 0.0000001);
        }
    }
}
