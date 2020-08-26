using System;
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
            Enumerable.Range(0, n + 1)
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
            Enumerable.Range(0, n + 1)
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
            Enumerable.Range(0, n + 1)
                .Sum(i => (Math.Pow(-1, i) / Factorial.Calculate(2 * i)) * Math.Pow(x, 2 * i));

        /// <summary>
        /// Calculates approximation of ln(x) function:
        /// ln(x) = x - x^2 / 2 + ... + (-1)^n * x^(2*n) / (2*n)! + ...,
        /// where n is number of terms (natural number),
        /// and x is given point (rational number).
        /// </summary>
        /// <param name="x">Given point.</param>
        /// <param name="n">The number of terms in polynomial.</param>
        /// <returns>Approximated value of the function in the given point.</returns>
        public static double Ln(double x, int n) =>
            Enumerable.Range(1, n + 1)
                .Sum(i => (Math.Pow(-1, i + 1) / i) * Math.Pow(x - 1, i));
    }
}
