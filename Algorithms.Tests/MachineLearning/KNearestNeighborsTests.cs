using NUnit.Framework;
using Algorithms.MachineLearning;
using System;

namespace Algorithms.Tests.MachineLearning;

[TestFixture]
public class KNearestNeighborsTests
{
    [Test]
    public void Constructor_InvalidK_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new KNearestNeighbors<string>(0));
    }

    [Test]
    public void AddSample_NullFeatures_ThrowsException()
    {
        var knn = new KNearestNeighbors<string>(3);
        double[]? features = null;
        Assert.Throws<ArgumentNullException>(() => knn.AddSample(features!, "A"));
    }

    [Test]
    public void Predict_NoTrainingData_ThrowsException()
    {
        var knn = new KNearestNeighbors<string>(1);
        Assert.Throws<InvalidOperationException>(() => knn.Predict(new[] { 1.0 }));
    }

    [Test]
    public void Predict_NullFeatures_ThrowsException()
    {
        var knn = new KNearestNeighbors<string>(1);
        knn.AddSample(new[] { 1.0 }, "A");
        double[]? features = null;
        Assert.Throws<ArgumentNullException>(() => knn.Predict(features!));
    }

    [Test]
    public void EuclideanDistance_DifferentLengths_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => KNearestNeighbors<string>.EuclideanDistance(new[] { 1.0 }, new[] { 1.0, 2.0 }));
    }

    [Test]
    public void EuclideanDistance_CorrectResult()
    {
        double[] a = { 1.0, 2.0 };
        double[] b = { 4.0, 6.0 };
        double expected = 5.0;
        double actual = KNearestNeighbors<string>.EuclideanDistance(a, b);
        Assert.That(actual, Is.EqualTo(expected).Within(1e-9));
    }

    [Test]
    public void Predict_SingleNeighbor_CorrectLabel()
    {
        var knn = new KNearestNeighbors<string>(1);
        knn.AddSample(new[] { 1.0, 2.0 }, "A");
        knn.AddSample(new[] { 3.0, 4.0 }, "B");
        var label = knn.Predict(new[] { 1.1, 2.1 });
        Assert.That(label, Is.EqualTo("A"));
    }

    [Test]
    public void Predict_MajorityVote_CorrectLabel()
    {
        var knn = new KNearestNeighbors<string>(3);
        knn.AddSample(new[] { 0.0, 0.0 }, "A");
        knn.AddSample(new[] { 0.1, 0.1 }, "A");
        knn.AddSample(new[] { 1.0, 1.0 }, "B");
        var label = knn.Predict(new[] { 0.05, 0.05 });
        Assert.That(label, Is.EqualTo("A"));
    }

    [Test]
    public void Predict_TieBreaker_ReturnsConsistentLabel()
    {
        var knn = new KNearestNeighbors<string>(2);
        knn.AddSample(new[] { 0.0, 0.0 }, "A");
        knn.AddSample(new[] { 1.0, 1.0 }, "B");
        var label = knn.Predict(new[] { 0.5, 0.5 });
        Assert.That(label, Is.EqualTo("A"));
    }
}
