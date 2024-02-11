using System;
using Algorithms.Numeric.Decomposition;
using FluentAssertions;
using NUnit.Framework;
using Utilities.Extensions;
using M = Utilities.Extensions.MatrixExtensions;
using V = Utilities.Extensions.VectorExtensions;

namespace Algorithms.Tests.Numeric.Decomposition;

public class SvdTests
{
    [Test]
    public void RandomUnitVector()
    {
        var epsilon = 0.0001;
        // unit vector should have length 1
        ThinSvd.RandomUnitVector(10).Magnitude().Should().BeApproximately(1, epsilon);
        // unit vector with single element should be [-1] or [+1]
        Math.Abs(ThinSvd.RandomUnitVector(1)[0]).Should().BeApproximately(1, epsilon);
        // two randomly generated unit vectors should not be equal 
        ThinSvd.RandomUnitVector(10).Should().NotBeEquivalentTo(ThinSvd.RandomUnitVector(10));
    }

    [Test]
    public void Svd_Decompose()
    {
        CheckSvd(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
        CheckSvd(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
        CheckSvd(new double[,] { { 1, 0, 0, 0, 2 }, { 0, 3, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 2, 0, 0, 0 } });
    }

    [Test]
    public void Svd_Random([Random(3, 10, 5)] int m, [Random(3, 10, 5)] int n)
    {
        double[,] matrix = GenerateRandomMatrix(m, n);
        CheckSvd(matrix);
    }

    private void AssertMatrixEqual(double[,] matrix1, double[,] matrix2, double epsilon)
    {
        matrix1.GetLength(0).Should().Be(matrix2.GetLength(0));
        matrix1.GetLength(1).Should().Be(matrix2.GetLength(1));
        for (var i = 0; i < matrix1.GetLength(0); i++)
        {
            for (var j = 0; j < matrix1.GetLength(1); j++)
            {
                Assert.That(matrix1[i, j], Is.EqualTo(matrix2[i, j]).Within(epsilon), $"At index ({i}, {j})");
            }
        }
    }

    private double[,] GenerateRandomMatrix(int m, int n)
    {
        double[,] result = new double[m, n];
        Random random = new();
        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                result[i, j] = random.NextDouble() - 0.5;
            }
        }

        return result;
    }

    private void CheckSvd(double[,] testMatrix)
    {
        var epsilon = 1E-6;
        double[,] u;
        double[,] v;
        double[] s;
        (u, s, v) = ThinSvd.Decompose(testMatrix, 1e-6 * epsilon, 1000);

        for (var i = 1; i < s.Length; i++)
        {
            // singular values should be arranged from greatest to smallest
            // but there are rounding errors
            (s[i] - s[i - 1]).Should().BeLessThan(1);
        }

        for (var i = 0; i < u.GetLength(1); i++)
        {
            double[] extracted = new double[u.GetLength(0)];
            // extract a column of u
            for (var j = 0; j < extracted.Length; j++)
            {
                extracted[j] = u[j, i];
            }

            if (s[i] > epsilon)
            {
                // if the singular value is non-zero, then the basis vector in u should be a unit vector
                extracted.Magnitude().Should().BeApproximately(1, epsilon);
            }
            else
            {
                // if the singular value is zero, then the basis vector in u should be zeroed out
                extracted.Magnitude().Should().BeApproximately(0, epsilon);
            }
        }

        for (var i = 0; i < v.GetLength(1); i++)
        {
            double[] extracted = new double[v.GetLength(0)];
            // extract column of v
            for (var j = 0; j < extracted.Length; j++)
            {
                extracted[j] = v[j, i];
            }

            if (s[i] > epsilon)
            {
                // if the singular value is non-zero, then the basis vector in v should be a unit vector
                Assert.That(extracted.Magnitude(), Is.EqualTo(1).Within(epsilon));
            }
            else
            {
                // if the singular value is zero, then the basis vector in v should be zeroed out
                Assert.That(extracted.Magnitude(), Is.EqualTo(0).Within(epsilon));
            }
        }

        // convert singular values to a diagonal matrix
        double[,] expanded = new double[s.Length, s.Length];
        for (var i = 0; i < s.Length; i++)
        {
            expanded[i, i] = s[i];
        }


        // matrix = U * S * V^t, definition of Singular Vector Decomposition
        AssertMatrixEqual(testMatrix, u.Multiply(expanded).Multiply(v.Transpose()), epsilon);
        AssertMatrixEqual(testMatrix, u.Multiply(expanded.Multiply(v.Transpose())), epsilon);
    }
}
