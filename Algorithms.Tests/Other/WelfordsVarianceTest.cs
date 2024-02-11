using Algorithms.Other;
using NUnit.Framework;

namespace Algorithms.Tests.Other;

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

        Assert.That(welfordsVariance.Count, Is.EqualTo(4));
        Assert.That(welfordsVariance.Mean, Is.EqualTo(10).Within(0.0000001));
        Assert.That(welfordsVariance.Variance, Is.EqualTo(22.5).Within(0.0000001));
        Assert.That(welfordsVariance.SampleVariance, Is.EqualTo(30).Within(0.0000001));
    }

    [Test]
    public void WelfordVariance_Example2()
    {
        var stats = new WelfordsVariance();
        stats.AddValue(100000004);
        stats.AddValue(100000007);
        stats.AddValue(100000013);
        stats.AddValue(100000016);
        Assert.That(stats.Count, Is.EqualTo(4));
        Assert.That(stats.Mean, Is.EqualTo(100000010).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(22.5).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(30).Within(0.0000001));
    }

    [Test]
    public void WelfordVariance_Example3()
    {
        var stats = new WelfordsVariance();
        stats.AddValue(1000000004);
        stats.AddValue(1000000007);
        stats.AddValue(1000000013);
        stats.AddValue(1000000016);
        Assert.That(stats.Count, Is.EqualTo(4));
        Assert.That(stats.Mean, Is.EqualTo(1000000010).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(22.5).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(30).Within(0.0000001));
    }

    [Test]
    public void WelfordVariance_Example4()
    {
        var stats = new WelfordsVariance();
        stats.AddValue(6);
        stats.AddValue(2);
        stats.AddValue(3);
        stats.AddValue(1);
        Assert.That(stats.Count, Is.EqualTo(4));
        Assert.That(stats.Mean, Is.EqualTo(3).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(3.5).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(4.6666667).Within(0.0000001));
    }

    [Test]
    public void WelfordVariance_Example5()
    {
        var stats = new WelfordsVariance(new double[] { 2, 2, 5, 7 });
        Assert.That(stats.Count, Is.EqualTo(4));
        Assert.That(stats.Mean, Is.EqualTo(4).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(4.5).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(6).Within(0.0000001));
    }

    [Test]
    public void WelfordVariance_Example6()
    {
        var stats = new WelfordsVariance();
        stats.AddRange(new double[] { 2, 4, 4, 4, 5, 5, 7, 9 });
        Assert.That(stats.Count, Is.EqualTo(8));
        Assert.That(stats.Mean, Is.EqualTo(5).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(4).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(4.5714286).Within(0.0000001));
    }

    [Test]
    public void WelfordVariance_Example7()
    {
        var stats = new WelfordsVariance();
        stats.AddRange(new double[] { 9, 2, 5, 4, 12, 7, 8, 11, 9, 3, 7, 4, 12, 5, 4, 10, 9, 6, 9, 4 });
        Assert.That(stats.Count, Is.EqualTo(20));
        Assert.That(stats.Mean, Is.EqualTo(7).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(8.9).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(9.3684211).Within(0.0000001));
    }

    [Test]
    public void WelfordVariance_Example8()
    {
        var stats = new WelfordsVariance();
        stats.AddRange(new [] { 51.3, 55.6, 49.9, 52.0 });
        Assert.That(stats.Count, Is.EqualTo(4));
        Assert.That(stats.Mean, Is.EqualTo(52.2).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(4.4250000).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(5.9000000).Within(0.0000001));
    }

    [Test]
    public void WelfordVariance_Example9()
    {
        var stats = new WelfordsVariance();
        stats.AddRange(new double[] { -5, -3, -1, 1, 3 });
        Assert.That(stats.Count, Is.EqualTo(5));
        Assert.That(stats.Mean, Is.EqualTo(-1).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(8).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(10).Within(0.0000001));
    }

    [Test]
    public void WelfordVariance_Example10()
    {
        var stats = new WelfordsVariance();
        stats.AddRange(new double[] { -1, 0, 1 });
        Assert.That(stats.Count, Is.EqualTo(3));
        Assert.That(stats.Mean, Is.EqualTo(0).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(0.6666667).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(1).Within(0.0000001));
    }

    [Test]
    public void WelfordVariance_NoValue()
    {
        var stats = new WelfordsVariance();
        Assert.That(stats.Count, Is.EqualTo(0));
        Assert.That(stats.Mean, Is.EqualTo(double.NaN));
        Assert.That(stats.Variance, Is.EqualTo(double.NaN));
        Assert.That(stats.SampleVariance, Is.EqualTo(double.NaN));
    }

    [Test]
    public void WelfordVariance_OneValue()
    {
        var stats = new WelfordsVariance();
        stats.AddValue(1);
        Assert.That(stats.Count, Is.EqualTo(1));
        Assert.That(stats.Mean, Is.EqualTo(double.NaN));
        Assert.That(stats.Variance, Is.EqualTo(double.NaN));
        Assert.That(stats.SampleVariance, Is.EqualTo(double.NaN));
    }

    [Test]
    public void WelfordVariance_TwoValues()
    {
        var stats = new WelfordsVariance();
        stats.AddValue(1);
        stats.AddValue(2);
        Assert.That(stats.Count, Is.EqualTo(2));
        Assert.That(stats.Mean, Is.EqualTo(1.5).Within(0.0000001));
        Assert.That(stats.Variance, Is.EqualTo(0.25).Within(0.0000001));
        Assert.That(stats.SampleVariance, Is.EqualTo(0.5).Within(0.0000001));
    }
}
