using System;
using System.Collections.Generic;

namespace Algorithms.Numeric;

/// <summary>
///     In numerical analysis, the Runge–Kutta methods are a family of implicit and explicit iterative methods,
///     used in temporal discretization for the approximate solutions of simultaneous nonlinear equations.
///     The most widely known member of the Runge–Kutta family is generally referred to as
///     "RK4", the "classic Runge–Kutta method" or simply as "the Runge–Kutta method".
///     </summary>
public static class RungeKuttaMethod
{
    /// <summary>
    ///     Loops through all the steps until xEnd is reached, adds a point for each step and then
    ///     returns all the points.
    /// </summary>
    /// <param name="xStart">Initial conditions x-value.</param>
    /// <param name="xEnd">Last x-value.</param>
    /// <param name="stepSize">Step-size on the x-axis.</param>
    /// <param name="yStart">Initial conditions y-value.</param>
    /// <param name="function">The right hand side of the differential equation.</param>
    /// <returns>The solution of the Cauchy problem.</returns>
    public static List<double[]> ClassicRungeKuttaMethod(
        double xStart,
        double xEnd,
        double stepSize,
        double yStart,
        Func<double, double, double> function)
    {
        if (xStart >= xEnd)
        {
            throw new ArgumentOutOfRangeException(
                nameof(xEnd),
                $"{nameof(xEnd)} should be greater than {nameof(xStart)}");
        }

        if (stepSize <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(stepSize),
                $"{nameof(stepSize)} should be greater than zero");
        }

        List<double[]> points = new();
        double[] firstPoint = { xStart, yStart };
        points.Add(firstPoint);

        var yCurrent = yStart;
        var xCurrent = xStart;

        while (xCurrent < xEnd)
        {
            var k1 = function(xCurrent, yCurrent);
            var k2 = function(xCurrent + 0.5 * stepSize, yCurrent + 0.5 * stepSize * k1);
            var k3 = function(xCurrent + 0.5 * stepSize, yCurrent + 0.5 * stepSize * k2);
            var k4 = function(xCurrent + stepSize, yCurrent + stepSize * k3);

            yCurrent += (1.0 / 6.0) * stepSize * (k1 + 2 * k2 + 2 * k3 + k4);
            xCurrent += stepSize;

            double[] newPoint = { xCurrent, yCurrent };
            points.Add(newPoint);
        }

        return points;
    }
}
