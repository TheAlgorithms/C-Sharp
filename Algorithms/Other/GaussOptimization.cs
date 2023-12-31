using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Other;

/// <summary>
/// The Gaussian method (coordinate descent method) refers to zero-order methods in which only the value
/// of the function Q(X) at different points in the space of variables is used to organize the search
/// for the extremum. This reduces the overall computational cost of finding the extremum. Also in
/// the Gaussian method, the procedures for finding and moving the operating point are simplified as
/// much as possible.
/// </summary>
public class GaussOptimization
{
    /// <summary>
    /// Implementation of function extremum search by the Gauss optimization algorithm.
    /// </summary>
    /// <param name="func">Function for which extremum has to be found.</param>
    /// <param name="n">This parameter identifies how much step size will be decreased each iteration.</param>
    /// <param name="step">The initial shift step.</param>
    /// <param name="eps">This value is used to control the accuracy of the optimization. In case if the error is less than eps,
    /// optimization will be stopped.</param>
    /// <param name="x1">The first function parameter.</param>
    /// <param name="x2">The second function parameter.</param>
    /// <returns>A tuple of coordinates of function extremum.</returns>
    public (double, double) Optimize(
        Func<double, double, double> func,
        double n,
        double step,
        double eps,
        double x1,
        double x2)
    {
        // The initial value of the error
        double error = 1;

        while (Math.Abs(error) > eps)
        {
            // Calculation of the function with coordinates that are calculated with shift
            double bottom = func(x1, x2 - step);
            double top = func(x1, x2 + step);
            double left = func(x1 - step, x2);
            double right = func(x1 + step, x2);

            // Determination of the best option.
            var possibleFunctionValues = new List<double> { bottom, top, left, right };
            double maxValue = possibleFunctionValues.Max();
            double maxValueIndex = possibleFunctionValues.IndexOf(maxValue);

            // Error evaluation
            error = maxValue - func(x1, x2);

            // Coordinates update for the best option
            switch (maxValueIndex)
            {
                case 0:
                    x2 -= step;
                    break;
                case 1:
                    x2 += step;
                    break;
                case 2:
                    x1 -= step;
                    break;
                default:
                    x1 += step;
                    break;
            }

            // Step reduction
            step /= n;
        }

        return (x1, x2);
    }
}
