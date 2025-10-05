using NUnit.Framework;
using Algorithms.MachineLearning;
using System;

namespace Algorithms.Tests.MachineLearning;

[TestFixture]
public class LogisticRegressionTests
{
    [Test]
    public void Fit_ThrowsOnEmptyInput()
    {
        var model = new LogisticRegression();
        Assert.Throws<ArgumentException>(() => model.Fit(Array.Empty<double[]>(), Array.Empty<int>()));
    }

    [Test]
    public void Fit_ThrowsOnMismatchedLabels()
    {
        var model = new LogisticRegression();
        double[][] X = { new double[] { 1, 2 } };
        int[] y = { 1, 0 };
        Assert.Throws<ArgumentException>(() => model.Fit(X, y));
    }

    [Test]
    public void FitAndPredict_WorksOnSimpleData()
    {
        // Simple AND logic
        double[][] X =
        {
            new[] { 0.0, 0.0 },
            new[] { 0.0, 1.0 },
            new[] { 1.0, 0.0 },
            new[] { 1.0, 1.0 }
        };
        int[] y = { 0, 0, 0, 1 };
        var model = new LogisticRegression();
        model.Fit(X, y, epochs: 2000, learningRate: 0.1);
        Assert.That(model.Predict(new double[] { 0, 0 }), Is.EqualTo(0));
        Assert.That(model.Predict(new double[] { 0, 1 }), Is.EqualTo(0));
        Assert.That(model.Predict(new double[] { 1, 0 }), Is.EqualTo(0));
        Assert.That(model.Predict(new double[] { 1, 1 }), Is.EqualTo(1));
    }

    [Test]
    public void PredictProbability_ThrowsOnFeatureMismatch()
    {
        var model = new LogisticRegression();
        double[][] X = { new double[] { 1, 2 } };
        int[] y = { 1 };
        model.Fit(X, y);
        Assert.Throws<ArgumentException>(() => model.PredictProbability(new double[] { 1 }));
    }

    [Test]
    public void FeatureCount_ReturnsCorrectValue()
    {
        var model = new LogisticRegression();
        double[][] X = { new double[] { 1, 2, 3 } };
        int[] y = { 1 };
        model.Fit(X, y);
        Assert.That(model.FeatureCount, Is.EqualTo(3));
    }
}
