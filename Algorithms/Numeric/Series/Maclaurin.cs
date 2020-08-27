using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Numeric.Series
{
    /// <summary>
    /// Maclaurin series calculates nonlinear functions approximation
    /// starting from point x = 0 in a form of infinite power series:
    /// f(x) = f(0) + f'(0) * x + ... + (f'n(0) * (x ^ n)) / n! + ...,
    /// where n is natural number.
    /// </summary>
    public static class Maclaurin
    {
        /// <summary>
        /// Calculates approximation of e^x function:
        /// e^x = 1 + x + x^2 / 2! + ... + x^n / n! + ...,
        /// where n is number of terms (natural number),
        /// and x is given point (rational number).
        /// </summary>
        /// <param name="x">Given point.</param>
        /// <param name="n">The number of terms in polynomial.</param>
        /// <returns>Approximated value of the function in the given point.</returns>
        public static double Exp(double x, int n) =>
            Enumerable.Range(0, n)
                .Sum(i => Math.Pow(x, i) / Factorial.Calculate(i));

        /// <summary>
        /// Calculates approximation of sin(x) function:
        /// sin(x) = x - x^3 / 3! + ... + (-1)^n * x^(2*n + 1) / (2*n + 1)! + ...,
        /// where n is number of terms (natural number),
        /// and x is given point (rational number).
        /// </summary>
        /// <param name="x">Given point.</param>
        /// <param name="n">The number of terms in polynomial.</param>
        /// <returns>Approximated value of the function in the given point.</returns>
        public static double Sin(double x, int n) =>
            Enumerable.Range(0, n)
                .Sum(i => (Math.Pow(-1, i) / Factorial.Calculate(2 * i + 1)) * Math.Pow(x, 2 * i + 1));

        /// <summary>
        /// Calculates approximation of cos(x) function:
        /// cos(x) = 1 - x^2 / 2! + ... + (-1)^n * x^(2*n) / (2*n)! + ...,
        /// where n is number of terms (natural number),
        /// and x is given point (rational number).
        /// </summary>
        /// <param name="x">Given point.</param>
        /// <param name="n">The number of terms in polynomial.</param>
        /// <returns>Approximated value of the function in the given point.</returns>
        public static double Cos(double x, int n) =>
            Enumerable.Range(0, n)
                .Sum(i => (Math.Pow(-1, i) / Factorial.Calculate(2 * i)) * Math.Pow(x, 2 * i));

        /// <summary>
        /// Calculates approximation of e^x function:
        /// e^x = 1 + x + x^2 / 2! + ... + x^n / n! + ...,
        /// and x is given point (rational number).
        /// </summary>
        /// <param name="x">Given point.</param>
        /// <param name="error">Last term error value.</param>
        /// <returns>Approximated value of the function in the given point.</returns>
        /// <exception cref="ArgumentException">Error value is not on interval (0.0; 1.0).</exception>
        /// <exception cref="ArgumentException">Series will not converge, when <b>|x|</b> is less than <b>eps</b>.</exception>
        public static double Exp(double x, double error = 0.00001)
        {
            if (error <= 0.0 || error >= 1.0)
            {
                throw new ArgumentException("Error value is not on interval (0.0; 1.0).");
            }

            if (Math.Abs(x) < error)
            {
                throw new ArgumentException("Series will not converge: |x| < eps.");
            }

            var n = 1;
            var termCoefficient = 1.0;
            var result = 0.0;

            while (Math.Abs(termCoefficient) > error)
            {
                result += termCoefficient;
                termCoefficient = Math.Pow(x, n) / Factorial.Calculate(n);
                n++;
            }

            return result;
        }
    }
}
