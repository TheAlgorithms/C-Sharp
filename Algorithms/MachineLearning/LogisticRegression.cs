using System;
using System.Linq;

namespace Algorithms.MachineLearning;

/// <summary>
/// Logistic Regression for binary classification.
/// </summary>
public class LogisticRegression
{
    private double[] weights = [];
    private double bias;

    public int FeatureCount => weights.Length;

    /// <summary>
    /// Fit the model using gradient descent.
    /// </summary>
    /// <param name="x">2D array of features (samples x features).</param>
    /// <param name="y">Array of labels (0 or 1).</param>
    /// <param name="epochs">Number of iterations.</param>
    /// <param name="learningRate">Step size.</param>
    public void Fit(double[][] x, int[] y, int epochs = 1000, double learningRate = 0.01)
    {
        if (x.Length == 0 || x[0].Length == 0)
        {
            throw new ArgumentException("Input features cannot be empty.");
        }

        if (x.Length != y.Length)
        {
            throw new ArgumentException("Number of samples and labels must match.");
        }

        int nSamples = x.Length;
        int nFeatures = x[0].Length;
        weights = new double[nFeatures];
        bias = 0;

        for (int epoch = 0; epoch < epochs; epoch++)
        {
            double[] dw = new double[nFeatures];
            double db = 0;
            for (int i = 0; i < nSamples; i++)
            {
                double linear = Dot(x[i], weights) + bias;
                double pred = Sigmoid(linear);
                double error = pred - y[i];
                for (int j = 0; j < nFeatures; j++)
                {
                    dw[j] += error * x[i][j];
                }

                db += error;
            }

            for (int j = 0; j < nFeatures; j++)
            {
                weights[j] -= learningRate * dw[j] / nSamples;
            }

            bias -= learningRate * db / nSamples;
        }
    }

    /// <summary>
    /// Predict probability for a single sample.
    /// </summary>
    public double PredictProbability(double[] x)
    {
        if (x.Length != weights.Length)
        {
            throw new ArgumentException("Feature count mismatch.");
        }

        return Sigmoid(Dot(x, weights) + bias);
    }

    /// <summary>
    /// Predict class label (0 or 1) for a single sample.
    /// </summary>
    public int Predict(double[] x) => PredictProbability(x) >= 0.5 ? 1 : 0;

    private static double Sigmoid(double z) => 1.0 / (1.0 + Math.Exp(-z));

    private static double Dot(double[] a, double[] b) => a.Zip(b).Sum(pair => pair.First * pair.Second);
}
