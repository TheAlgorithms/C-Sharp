using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.MachineLearning;

/// <summary>
/// Implements simple linear regression for one independent variable (univariate).
/// Linear regression is a supervised learning algorithm used to model the relationship
/// between a scalar dependent variable (Y) and an independent variable (X).
/// The model fits a line: Y = a + bX, where 'a' is the intercept and 'b' is the slope.
/// </summary>
public class LinearRegression
{
    // Intercept (a) and slope (b) of the fitted line
    public double Intercept { get; private set; }

    public double Slope { get; private set; }

    public bool IsFitted { get; private set; }

    /// <summary>
    /// Fits the linear regression model to the provided data.
    /// </summary>
    /// <param name="x">List of independent variable values.</param>
    /// <param name="y">List of dependent variable values.</param>
    /// <exception cref="ArgumentException">Thrown if input lists are null, empty, or of different lengths.</exception>
    public void Fit(IList<double> x, IList<double> y)
    {
        if (x == null || y == null)
        {
            throw new ArgumentException("Input data cannot be null.");
        }

        if (x.Count == 0 || y.Count == 0)
        {
            throw new ArgumentException("Input data cannot be empty.");
        }

        if (x.Count != y.Count)
        {
            throw new ArgumentException("Input lists must have the same length.");
        }

        // Calculate means
        double xMean = x.Average();
        double yMean = y.Average();

        // Calculate slope (b) and intercept (a)
        double numerator = 0.0;
        double denominator = 0.0;
        for (int i = 0; i < x.Count; i++)
        {
            numerator += (x[i] - xMean) * (y[i] - yMean);
            denominator += (x[i] - xMean) * (x[i] - xMean);
        }

        const double epsilon = 1e-12;
        if (Math.Abs(denominator) < epsilon)
        {
            throw new ArgumentException("Variance of X must not be zero.");
        }

        Slope = numerator / denominator;
        Intercept = yMean - Slope * xMean;
        IsFitted = true;
    }

    /// <summary>
    /// Predicts the output value for a given input using the fitted model.
    /// </summary>
    /// <param name="x">Input value.</param>
    /// <returns>Predicted output value.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the model is not fitted.</exception>
    public double Predict(double x)
    {
        if (!IsFitted)
        {
            throw new InvalidOperationException("Model must be fitted before prediction.");
        }

        return Intercept + Slope * x;
    }

    /// <summary>
    /// Predicts output values for a list of inputs using the fitted model.
    /// </summary>
    /// <param name="xValues">List of input values.</param>
    /// <returns>List of predicted output values.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the model is not fitted.</exception>
    public IList<double> Predict(IList<double> xValues)
    {
        if (!IsFitted)
        {
            throw new InvalidOperationException("Model must be fitted before prediction.");
        }

        return xValues.Select(Predict).ToList();
    }
}
