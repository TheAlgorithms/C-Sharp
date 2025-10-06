using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.MachineLearning;

/// <summary>
/// K-Nearest Neighbors (KNN) classifier implementation.
/// This algorithm classifies data points based on the majority label of their k nearest neighbors.
/// </summary>
/// <typeparam name="TLabel">
/// The type of the label used for classification. This can be any type that represents the class or category of a sample.
/// </typeparam>
public class KNearestNeighbors<TLabel>
{
    private readonly List<(double[] Features, TLabel Label)> trainingData = new();
    private readonly int k;

    /// <summary>
    /// Initializes a new instance of the <see cref="KNearestNeighbors{TLabel}"/> classifier.
    /// </summary>
    /// <param name="k">Number of neighbors to consider for classification.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if k is less than 1.</exception>
    public KNearestNeighbors(int k)
    {
        if (k < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(k), "k must be at least 1.");
        }

        this.k = k;
    }

    /// <summary>
    /// Calculates the Euclidean distance between two feature vectors.
    /// </summary>
    /// <param name="a">First feature vector.</param>
    /// <param name="b">Second feature vector.</param>
    /// <returns>Euclidean distance.</returns>
    /// <exception cref="ArgumentException">Thrown if vectors are of different lengths.</exception>
    public static double EuclideanDistance(double[] a, double[] b)
    {
        if (a.Length != b.Length)
        {
            throw new ArgumentException("Feature vectors must be of the same length.");
        }

        double sum = 0;
        for (int i = 0; i < a.Length; i++)
        {
            double diff = a[i] - b[i];
            sum += diff * diff;
        }

        return Math.Sqrt(sum);
    }

    /// <summary>
    /// Adds a training sample to the classifier.
    /// </summary>
    /// <param name="features">Feature vector of the sample.</param>
    /// <param name="label">Label of the sample.</param>
    public void AddSample(double[] features, TLabel label)
    {
        if (features == null)
        {
            throw new ArgumentNullException(nameof(features));
        }

        trainingData.Add((features, label));
    }

    /// <summary>
    /// Predicts the label for a given feature vector using the KNN algorithm.
    /// </summary>
    /// <param name="features">Feature vector to classify.</param>
    /// <returns>Predicted label.</returns>
    /// <exception cref="InvalidOperationException">Thrown if there is no training data.</exception>
    public TLabel Predict(double[] features)
    {
        if (trainingData.Count == 0)
        {
            throw new InvalidOperationException("No training data available.");
        }

        if (features == null)
        {
            throw new ArgumentNullException(nameof(features));
        }

        // Compute distances to all training samples
        var distances = trainingData
            .Select(td => (Label: td.Label, Distance: EuclideanDistance(features, td.Features)))
            .OrderBy(x => x.Distance)
            .Take(k)
            .ToList();

        // Majority vote
        var labelCounts = distances
            .GroupBy(x => x.Label)
            .Select(g => new { Label = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Label?.GetHashCode() ?? 0)
            .ToList();

        return labelCounts.First().Label;
    }
}
