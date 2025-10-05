using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.MachineLearning;

/// <summary>
/// Simple Decision Tree for binary classification using the ID3 algorithm.
/// Supports categorical features (int values).
/// </summary>
public class DecisionTree
{
    private Node? root;

    /// <summary>
    /// Trains the decision tree using the ID3 algorithm.
    /// </summary>
    /// <param name="x">2D array of features (samples x features), categorical (int).</param>
    /// <param name="y">Array of labels (0 or 1).</param>
    public void Fit(int[][] x, int[] y)
    {
        if (x.Length == 0 || x[0].Length == 0)
        {
            throw new ArgumentException("Input features cannot be empty.");
        }

        if (x.Length != y.Length)
        {
            throw new ArgumentException("Number of samples and labels must match.");
        }

        root = BuildTree(x, y, Enumerable.Range(0, x[0].Length).ToList());
    }

    /// <summary>
    /// Predicts the class label (0 or 1) for a single sample.
    /// </summary>
    public int Predict(int[] x)
    {
        if (root is null)
        {
            throw new InvalidOperationException("Model not trained.");
        }

        if (x.Length != FeatureCount)
        {
            throw new ArgumentException("Feature count mismatch.");
        }

        return Traverse(root, x);
    }

    /// <summary>
    /// Gets the number of features used in training.
    /// </summary>
    public int FeatureCount => root?.FeatureCount ?? 0;

    private static Node BuildTree(int[][] x, int[] y, List<int> features)
    {
        if (y.All(l => l == y[0]))
        {
            return new Node { Label = y[0], FeatureCount = x[0].Length };
        }

        if (features.Count == 0)
        {
            return new Node { Label = MostCommon(y), FeatureCount = x[0].Length };
        }

        int bestFeature = BestFeature(x, y, features);
        var node = new Node { Feature = bestFeature, FeatureCount = x[0].Length };
        var values = x.Select(row => row[bestFeature]).Distinct();
        node.Children = new();
        foreach (var v in values)
        {
            var idx = x.Select((row, i) => (row, i)).Where(t => t.row[bestFeature] == v).Select(t => t.i).ToArray();
            if (idx.Length == 0)
            {
                continue;
            }

            var subX = idx.Select(i => x[i]).ToArray();
            var subY = idx.Select(i => y[i]).ToArray();
            var subFeatures = features.Where(f => f != bestFeature).ToList();
            node.Children[v] = BuildTree(subX, subY, subFeatures);
        }

        return node;
    }

    private static int Traverse(Node node, int[] x)
    {
        if (node.Label is not null)
        {
            return node.Label.Value;
        }

        int v = x[node.Feature!.Value];
        if (node.Children != null && node.Children.TryGetValue(v, out var child))
        {
            return Traverse(child, x);
        }

        // fallback to 0 if unseen value or Children is null
        return 0;
    }

    private static int MostCommon(int[] y) => y.GroupBy(l => l).OrderByDescending(g => g.Count()).First().Key;

    private static int BestFeature(int[][] x, int[] y, List<int> features)
    {
        double baseEntropy = Entropy(y);
        double bestGain = double.MinValue;
        int bestFeature = features[0];
        foreach (var f in features)
        {
            var values = x.Select(row => row[f]).Distinct();
            double splitEntropy = 0;
            foreach (var v in values)
            {
                var idx = x.Select((row, i) => (row, i)).Where(t => t.row[f] == v).Select(t => t.i).ToArray();
                if (idx.Length == 0)
                {
                    continue;
                }

                var subY = idx.Select(i => y[i]).ToArray();
                splitEntropy += (double)subY.Length / y.Length * Entropy(subY);
            }

            double gain = baseEntropy - splitEntropy;
            if (gain > bestGain)
            {
                bestGain = gain;
                bestFeature = f;
            }
        }

        return bestFeature;
    }

    private static double Entropy(int[] y)
    {
        int n = y.Length;
        if (n == 0)
        {
            return 0;
        }

        double p0 = y.Count(l => l == 0) / (double)n;
        double p1 = y.Count(l => l == 1) / (double)n;
        double e = 0;
        if (p0 > 0)
        {
            e -= p0 * Math.Log2(p0);
        }

        if (p1 > 0)
        {
            e -= p1 * Math.Log2(p1);
        }

        return e;
    }

    private class Node
    {
        public int? Feature { get; set; }

        public int? Label { get; set; }

        public int FeatureCount { get; set; }

        public Dictionary<int, Node>? Children { get; set; }
    }
}
