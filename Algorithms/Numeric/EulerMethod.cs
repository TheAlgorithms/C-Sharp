using System;
using System.Collections.Generic;

namespace Algorithms.Numeric
{
    /// <summary>
    /// In mathematics and computational science, the Euler method (also called forward Euler method)
    /// is a first-order numerical procedure for solving ordinary differential equations (ODEs)
    /// with a given initial value. It is the most basic explicit method for numerical integration
    /// of ordinary differential equations. The method proceeds in a series of steps. At each step
    /// the y-value is calculated by evaluating the differential equation at the previous step,
    /// multiplying the result with the step-size and adding it to the last y-value:
    /// y_n+1 = y_n + stepSize * f(x_n, y_n).
    /// (description adapted from https://en.wikipedia.org/wiki/Euler_method )
    /// (see also: https://www.geeksforgeeks.org/euler-method-solving-differential-equation/ ).
    /// </summary>
    public static class EulerMethod
    {
        /// <summary>
        /// Calculates the next y-value based on the current value of x, y and the stepSize.
        /// </summary>
        /// <param name="xCurrent">Current x-value.</param>
        /// <param name="stepSize">Step-size on the x-axis.</param>
        /// <param name="yCurrent">Current y-value.</param>
        /// <param name="differentialEquation">The differential equation to be solved.</param>
        /// <returns>The next y-value.</returns>
        public static double EulerStep(
                double xCurrent,
                double stepSize,
                double yCurrent,
                Func<double, double, double> differentialEquation)
        {
            if (stepSize <= 0)
            {
              throw new ArgumentOutOfRangeException("stepSize", "stepSize should be greater than zero");
            }

            double yNext = yCurrent + stepSize * differentialEquation(xCurrent, yCurrent);
            return yNext;
        }

        /// <summary>
        /// Loops through all the steps until xEnd is reached, adds a point for each step and then
        /// returns all the points.
        /// </summary>
        /// <param name="xStart">First x-value.</param>
        /// <param name="xEnd">Last x-value.</param>
        /// <param name="stepSize">Step-size on the x-axis.</param>
        /// <param name="yStart">First y-value.</param>
        /// <param name="differentialEquation">The differential equation to be solved.</param>
        /// <returns>The points constituting the solution of the differential equation.</returns>
        public static List<double[]> EulerFull(
                double xStart,
                double xEnd,
                double stepSize,
                double yStart,
                Func<double, double, double> differentialEquation)
        {
            if (xStart >= xEnd)
            {
              throw new ArgumentOutOfRangeException("xEnd", "xEnd should be greater than xStart");
            }

            if (stepSize <= 0)
            {
              throw new ArgumentOutOfRangeException("stepSize", "stepSize should be greater than zero");
            }

            List<double[]> points = new List<double[]>();
            double[] firstPoint = { xStart, yStart };
            points.Add(firstPoint);
            double yCurrent = yStart;
            double xCurrent = xStart;

            while (xCurrent < xEnd)
            {
                yCurrent = EulerStep(xCurrent, stepSize, yCurrent, differentialEquation);
                xCurrent += stepSize;
                double[] point = { xCurrent, yCurrent };
                points.Add(point);
            }

            return points;
        }
    }
}
