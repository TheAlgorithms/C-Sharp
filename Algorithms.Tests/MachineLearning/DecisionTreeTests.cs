using NUnit.Framework;
using Algorithms.MachineLearning;
using System;

namespace Algorithms.Tests.MachineLearning;

[TestFixture]
public class DecisionTreeTests
{
    [Test]
    public void Fit_ThrowsOnEmptyInput()
    {
        var tree = new DecisionTree();
        Assert.Throws<ArgumentException>(() => tree.Fit(Array.Empty<int[]>(), Array.Empty<int>()));
    }

    [Test]
    public void Fit_ThrowsOnMismatchedLabels()
    {
        var tree = new DecisionTree();
        int[][] X = { new[] { 1, 2 } };
        int[] y = { 1, 0 };
        Assert.Throws<ArgumentException>(() => tree.Fit(X, y));
    }

    [Test]
    public void Predict_ThrowsIfNotTrained()
    {
        var tree = new DecisionTree();
        Assert.Throws<InvalidOperationException>(() => tree.Predict(new[] { 1, 2 }));
    }

    [Test]
    public void Predict_ThrowsOnFeatureMismatch()
    {
        var tree = new DecisionTree();
        int[][] X = { new[] { 1, 2 } };
        int[] y = { 1 };
        tree.Fit(X, y);
        Assert.Throws<ArgumentException>(() => tree.Predict(new[] { 1 }));
    }

    [Test]
    public void FitAndPredict_WorksOnSimpleData()
    {
        // Simple OR logic
        int[][] X =
        {
            new[] { 0, 0 },
            new[] { 0, 1 },
            new[] { 1, 0 },
            new[] { 1, 1 }
        };
        int[] y = { 0, 1, 1, 1 };
        var tree = new DecisionTree();
        tree.Fit(X, y);
        Assert.That(tree.Predict(new[] { 0, 0 }), Is.EqualTo(0));
        Assert.That(tree.Predict(new[] { 0, 1 }), Is.EqualTo(1));
        Assert.That(tree.Predict(new[] { 1, 0 }), Is.EqualTo(1));
        Assert.That(tree.Predict(new[] { 1, 1 }), Is.EqualTo(1));
    }

    [Test]
    public void FeatureCount_ReturnsCorrectValue()
    {
        var tree = new DecisionTree();
        int[][] X = { new[] { 1, 2, 3 } };
        int[] y = { 1 };
        tree.Fit(X, y);
        Assert.That(tree.FeatureCount, Is.EqualTo(3));
    }

    [Test]
    public void Predict_FallbacksToZeroForUnseenValue()
    {
        int[][] X = { new[] { 0, 0 }, new[] { 1, 1 } };
        int[] y = { 0, 1 };
        var tree = new DecisionTree();
        tree.Fit(X, y);
        // Value 2 is unseen in feature 0
        Assert.That(tree.Predict(new[] { 2, 0 }), Is.EqualTo(0));
    }
}
