using Algorithms.MachineLearning;

namespace Algorithms.Tests.MachineLearning;

/// <summary>
/// Unit tests for the LinearRegression class.
/// </summary>
public class LinearRegressionTests
{
    [Test]
    public void Fit_ThrowsException_WhenInputIsNull()
    {
        var lr = new LinearRegression();
        Assert.Throws<ArgumentException>(() => lr.Fit(null!, new List<double> { 1 }));
        Assert.Throws<ArgumentException>(() => lr.Fit(new List<double> { 1 }, null!));
    }

    [Test]
    public void Fit_ThrowsException_WhenInputIsEmpty()
    {
        var lr = new LinearRegression();
        Assert.Throws<ArgumentException>(() => lr.Fit(new List<double>(), new List<double>()));
    }

    [Test]
    public void Fit_ThrowsException_WhenInputLengthsDiffer()
    {
        var lr = new LinearRegression();
        Assert.Throws<ArgumentException>(() => lr.Fit(new List<double> { 1 }, new List<double> { 2, 3 }));
    }

    [Test]
    public void Fit_ThrowsException_WhenXVarianceIsZero()
    {
        var lr = new LinearRegression();
        Assert.Throws<ArgumentException>(() => lr.Fit(new List<double> { 1, 1, 1 }, new List<double> { 2, 3, 4 }));
    }

    [Test]
    public void Predict_ThrowsException_IfNotFitted()
    {
        var lr = new LinearRegression();
        Assert.Throws<InvalidOperationException>(() => lr.Predict(1.0));
        Assert.Throws<InvalidOperationException>(() => lr.Predict(new List<double> { 1.0 }));
    }

    [Test]
    public void FitAndPredict_WorksForSimpleData()
    {
        // y = 2x + 1
        var x = new List<double> { 1, 2, 3, 4 };
        var y = new List<double> { 3, 5, 7, 9 };
        var lr = new LinearRegression();
        lr.Fit(x, y);
        Assert.That(lr.IsFitted, Is.True);
        Assert.That(lr.Intercept, Is.EqualTo(1.0).Within(1e-6));
        Assert.That(lr.Slope, Is.EqualTo(2.0).Within(1e-6));
        Assert.That(lr.Predict(5), Is.EqualTo(11.0).Within(1e-6));
    }

    [Test]
    public void FitAndPredict_WorksForNegativeSlope()
    {
        // y = -3x + 4
        var x = new List<double> { 0, 1, 2 };
        var y = new List<double> { 4, 1, -2 };
        var lr = new LinearRegression();
        lr.Fit(x, y);
        Assert.That(lr.Intercept, Is.EqualTo(4.0).Within(1e-6));
        Assert.That(lr.Slope, Is.EqualTo(-3.0).Within(1e-6));
        Assert.That(lr.Predict(3), Is.EqualTo(-5.0).Within(1e-6));
    }

    [Test]
    public void Predict_List_WorksCorrectly()
    {
        var x = new List<double> { 1, 2, 3 };
        var y = new List<double> { 2, 4, 6 };
        var lr = new LinearRegression();
        lr.Fit(x, y); // y = 2x
        var predictions = lr.Predict(new List<double> { 4, 5 });
        Assert.That(predictions[0], Is.EqualTo(8.0).Within(1e-6));
        Assert.That(predictions[1], Is.EqualTo(10.0).Within(1e-6));
    }
}
