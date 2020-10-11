using Algorithms.Numeric.Decomposition;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Numeric.Decomposition
{
    public class SVDTests
    {
        public void AssertMatrixEqual(double[,] matrix1, double[,] matrix2, double epsilon)
        {
            Assert.AreEqual(matrix1.GetLength(0), matrix2.GetLength(0));
            Assert.AreEqual(matrix1.GetLength(1), matrix2.GetLength(1));
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    Assert.AreEqual(matrix1[i, j], matrix2[i, j], epsilon, $"At index ({i}, {j})");
                }
            }
        }

        [Test]
        public void MatrixMultiply()
        {
            double[,] lhs = new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            double[,] rhs = new double[,] { { 7, 8, 9 }, { 10, 11, 12 } };
            double[,] expected = new double[,] { { 27, 30, 33 }, { 61, 68, 75 }, { 95, 106, 117 } };
            double[,] got = SVD.MatrixMultiply(lhs, rhs);
            Assert.AreEqual(expected, got);
        }

        [Test]
        public void VectorMagnitude()
        {
            Assert.AreEqual(Math.Sqrt(3), SVD.Magnitude(new double[] { 1, -1, 0, 1 }));
        }

        [Test]
        public void RandomUnitVector()
        {
            double epsilon = 0.0001;
            Assert.AreEqual(1, SVD.Magnitude(SVD.RandomUnitVector(10)), epsilon);
            Assert.AreEqual(1, Math.Abs(SVD.RandomUnitVector(1)[0]), epsilon);
            Assert.AreNotEqual(SVD.RandomUnitVector(10), SVD.RandomUnitVector(10));
        }

        [Test]
        public void SVD_Decompose()
        {
            CheckSVD(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            CheckSVD(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            CheckSVD(new double[,] { { 1, 0, 0, 0, 2 }, { 0, 3, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 2, 0, 0, 0 } });
        }

        public double[,] GenerateRandomMatrix(int m, int n)
        {
            double[,] result = new double[m, n];
            Random random = new Random();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = random.NextDouble() * 100 - 50;
                }
            }
            return result;
        }

        [Test]
        public void SVD_Random([Random(3, 10, 5)] int m, [Random(3, 10, 5)] int n)
        {
            double[,] matrix = GenerateRandomMatrix(m, n);
            CheckSVD(matrix);
        }

        public void CheckSVD(double[,] testMatrix)
        {
            double epsilon = 1E-5;
            double[,] u, v;
            double[] s;
            (u, s, v) = SVD.Decompose(testMatrix, epsilon);

            for (int i = 0; i < u.GetLength(1); i++)
            {
                double[] extracted = new double[u.GetLength(0)];
                for (int j = 0; j < extracted.Length; j++)
                {
                    extracted[j] = u[j, i];
                }

                if (s[i] > epsilon)
                {
                    Assert.AreEqual(1, SVD.Magnitude(extracted), epsilon);
                }
                else
                {
                    Assert.Less(SVD.Magnitude(extracted), 1);
                }
            }

            for (int i = 0; i < v.GetLength(1); i++)
            {
                double[] extracted = new double[v.GetLength(0)];
                for (int j = 0; j < extracted.Length; j++)
                {
                    extracted[j] = v[j, i];
                }

                if (s[i] > epsilon)
                {
                    Assert.AreEqual(1, SVD.Magnitude(extracted), epsilon);
                }
                else
                {
                    Assert.Less(SVD.Magnitude(extracted), 1);
                }
            }

            double[,] expanded = new double[s.Length, s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                expanded[i, i] = s[i];
            }

            AssertMatrixEqual(testMatrix, SVD.MatrixMultiply(SVD.MatrixMultiply(u, expanded), SVD.MatrixTranspose(v)), epsilon);
            AssertMatrixEqual(testMatrix, SVD.MatrixMultiply(u, SVD.MatrixMultiply(expanded, SVD.MatrixTranspose(v))), epsilon);
        }
    }
}