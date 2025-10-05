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

    [Test]
    public void BuildTree_ReturnsNodeWithMostCommonLabel_WhenNoFeaturesLeft()
    {
        int[][] X = { new[] { 0 }, new[] { 1 }, new[] { 2 } };
        int[] y = { 1, 0, 1 };
        var tree = new DecisionTree();
        tree.Fit(X, y);
        // All features used, fallback to most common label (0)
        Assert.That(tree.Predict(new[] { 3 }), Is.EqualTo(0));
    }

    [Test]
    public void BuildTree_ReturnsNodeWithMostCommonLabel_WhenNoFeaturesLeft_MultipleLabels()
    {
        int[][] X = { new[] { 0 }, new[] { 1 }, new[] { 2 }, new[] { 3 } };
        int[] y = { 1, 0, 1, 0 };
        var tree = new DecisionTree();
        tree.Fit(X, y);
        // Most common label is 0 (2 times)
        Assert.That(tree.Predict(new[] { 4 }), Is.EqualTo(0));
    }

    [Test]
    public void BuildTree_ReturnsNodeWithSingleLabel_WhenAllLabelsZero()
    {
        int[][] X = { new[] { 0 }, new[] { 1 } };
        int[] y = { 0, 0 };
        var tree = new DecisionTree();
        tree.Fit(X, y);
        Assert.That(tree.Predict(new[] { 0 }), Is.EqualTo(0));
        Assert.That(tree.Predict(new[] { 1 }), Is.EqualTo(0));
    }

    [Test]
    public void Entropy_ReturnsZero_WhenAllZeroOrAllOne()
    {
        var method = typeof(DecisionTree).GetMethod("Entropy", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        Assert.That(method!.Invoke(null, new object[] { new int[] { 0, 0, 0 } }), Is.EqualTo(0d));
        Assert.That(method!.Invoke(null, new object[] { new int[] { 1, 1, 1 } }), Is.EqualTo(0d));
    }

    [Test]
    public void MostCommon_ReturnsCorrectLabel()
    {
        var method = typeof(DecisionTree).GetMethod("MostCommon", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        Assert.That(method!.Invoke(null, new object[] { new int[] { 1, 0, 1, 1, 0, 0, 0 } }), Is.EqualTo(0));
        Assert.That(method!.Invoke(null, new object[] { new int[] { 1, 1, 1, 0 } }), Is.EqualTo(1));
    }

    [Test]
    public void Traverse_FallbacksToZero_WhenChildrenIsNull()
    {
        // Create a node with Children = null and Label = null
        var nodeType = typeof(DecisionTree).GetNestedType("Node", System.Reflection.BindingFlags.NonPublic);
        var node = Activator.CreateInstance(nodeType!);
        nodeType!.GetProperty("Feature")!.SetValue(node, 0);
        nodeType!.GetProperty("Label")!.SetValue(node, null);
        nodeType!.GetProperty("Children")!.SetValue(node, null);
        var method = typeof(DecisionTree).GetMethod("Traverse", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        Assert.That(method!.Invoke(null, new object[] { node!, new int[] { 99 } }), Is.EqualTo(0));
    }

    [Test]
    public void BuildTree_ReturnsNodeWithSingleLabel_WhenAllLabelsSame()
    {
        int[][] X = { new[] { 0 }, new[] { 1 }, new[] { 2 } };
        int[] y = { 1, 1, 1 };
        var tree = new DecisionTree();
        tree.Fit(X, y);
        Assert.That(tree.Predict(new[] { 0 }), Is.EqualTo(1));
        Assert.That(tree.Predict(new[] { 1 }), Is.EqualTo(1));
        Assert.That(tree.Predict(new[] { 2 }), Is.EqualTo(1));
    }

    [Test]
    public void Entropy_ReturnsZero_WhenEmptyLabels()
    {
        // Use reflection to call private static Entropy
        var method = typeof(DecisionTree).GetMethod("Entropy", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        Assert.That(method!.Invoke(null, new object[] { Array.Empty<int>() }), Is.EqualTo(0d));
    }

    [Test]
    public void BestFeature_SkipsEmptyIdxBranch()
    {
        // Feature 1 has value 2 which is never present, triggers idx.Length == 0 branch
        int[][] X = { new[] { 0, 1 }, new[] { 1, 1 } };
        int[] y = { 0, 1 };
        var method = typeof(DecisionTree).GetMethod("BestFeature", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        var features = new System.Collections.Generic.List<int> { 0, 1 };
        var resultObj = method!.Invoke(null, new object[] { X, y, features });
        Assert.That(resultObj, Is.Not.Null);
        Assert.That((int)resultObj!, Is.EqualTo(0));
    }
}
