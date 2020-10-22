using NUnit.Framework;
using System;
using M = Utilities.Extensions.MatrixExtensions;

namespace Utilities.Tests
{
    public class MatrixTests
    {
        [Test]
        public void MatrixMultiply()
        {
            double[,] lhs = new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            double[,] rhs = new double[,] { { 7, 8, 9 }, { 10, 11, 12 } };
            double[,] expected = new double[,] { { 27, 30, 33 }, { 61, 68, 75 }, { 95, 106, 117 } };
            double[,] got = M.MultiplyGeneral(lhs, rhs);
            Assert.AreEqual(expected, got);
        }
    }
}