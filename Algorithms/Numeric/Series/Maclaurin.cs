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
            Enumerable.Range(0, n).Sum(i => ExpTerm(x, i));

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
            Enumerable.Range(0, n).Sum(i => SinTerm(x, i));

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
            Enumerable.Range(0, n).Sum(i => CosTerm(x, i));

        /// <summary>
        /// Calculates approximation of e^x function:
        /// e^x = 1 + x + x^2 / 2! + ... + x^n / n! + ...,
        /// and x is given point (rational number).
        /// </summary>
        /// <param name="x">Given point.</param>
        /// <param name="error">Last term error value.</param>
        /// <returns>Approximated value of the function in the given point.</returns>
        /// <exception cref="ArgumentException">Error value is not on interval (0.0; 1.0).</exception>
        public static double Exp(double x, double error = 0.00001) => ErrorTermWrapper(x, error, ExpTerm);

        /// <summary>
        /// Calculates approximation of sin(x) function:
        /// sin(x) = x - x^3 / 3! + ... + (-1)^n * x^(2*n + 1) / (2*n + 1)! + ...,
        /// and x is given point (rational number).
        /// </summary>
        /// <param name="x">Given point.</param>
        /// <param name="error">Last term error value.</param>
        /// <returns>Approximated value of the function in the given point.</returns>
        /// <exception cref="ArgumentException">Error value is not on interval (0.0; 1.0).</exception>
        public static double Sin(double x, double error = 0.00001) => ErrorTermWrapper(x, error, SinTerm);

        /// <summary>
        /// Calculates approximation of cos(x) function:
        /// cos(x) = 1 - x^2 / 2! + ... + (-1)^n * x^(2*n) / (2*n)! + ...,
        /// and x is given point (rational number).
        /// </summary>
        /// <param name="x">Given point.</param>
        /// <param name="error">Last term error value.</param>
        /// <returns>Approximated value of the function in the given point.</returns>
        /// <exception cref="ArgumentException">Error value is not on interval (0.0; 1.0).</exception>
        public static double Cos(double x, double error = 0.00001) => ErrorTermWrapper(x, error, CosTerm);

        /// <summary>
        /// Wrapper function for calculating approximation with estimated
        /// count of terms, where last term value is less than given error.
        /// </summary>
        /// <param name="x">Given point.</param>
        /// <param name="error">Last term error value.</param>
        /// <param name="term">Indexed term of approximation series.</param>
        /// <returns>Approximated value of the function in the given point.</returns>
        /// <exception cref="ArgumentException">Error value is not on interval (0.0; 1.0).</exception>
        private static double ErrorTermWrapper(double x, double error, Func<double, int, double> term)
        {
            if (error <= 0.0 || error >= 1.0)
            {
                throw new ArgumentException("Error value is not on interval (0.0; 1.0).");
            }

            var i = 0;
            var termCoefficient = 0.0;
            var result = 0.0;

            do
            {
                result += termCoefficient;
                termCoefficient = term(x, i);
                i++;
            }
            while (Math.Abs(termCoefficient) > error);

            return result;
        }

        /// <summary>
        /// Single term for e^x function approximation: x^i / i!.
        /// </summary>
        /// <param name="x">Given point.</param>
        /// <param name="i">Term index from 0 to n.</param>
        /// <returns>Single term value.</returns>
        private static double ExpTerm(double x, int i) => Math.Pow(x, i) / Factorial.Calculate(i);

        /// <summary>
        /// Single term for sin(x) function approximation: (-1)^i * x^(2*i + 1) / (2*i + 1)!.
        /// </summary>
        /// <param name="x">Given point.</param>
        /// <param name="i">Term index from 0 to n.</param>
        /// <returns>Single term value.</returns>
        private static double SinTerm(double x, int i) =>
            (Math.Pow(-1, i) / Factorial.Calculate(2 * i + 1)) * Math.Pow(x, 2 * i + 1);

        /// <summary>
        /// Single term for cos(x) function approximation: (-1)^i * x^(2*i) / (2*i)!.
        /// </summary>
        /// <param name="x">Given point.</param>
        /// <param name="i">Term index from 0 to n.</param>
        /// <returns>Single term value.</returns>
        private static double CosTerm(double x, int i) =>
            (Math.Pow(-1, i) / Factorial.Calculate(2 * i)) * Math.Pow(x, 2 * i);
    }
}
