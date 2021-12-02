using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Other
{
    public static class Tests
    {
        private static readonly Color Black = Color.FromArgb(255, 0, 0, 0);
        private static readonly Color Green = Color.FromArgb(255, 0, 255, 0);
        private static readonly Color Violet = Color.FromArgb(255, 255, 0, 255);
        private static readonly Color White = Color.FromArgb(255, 255, 255, 255);
        private static readonly Color Orange = Color.FromArgb(255, 255, 128, 0);

        [Test]
        public static void BreadthFirstSearch_ThrowsArgumentOutOfRangeException()
        {
            Action act = () => Algorithms.Other.FloodFill.BreadthFirstSearch(GenerateTestBitmap(), (10, 10), Black, White);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public static void DepthFirstSearch_ThrowsArgumentOutOfRangeException()
        {
            Action act = () => Algorithms.Other.FloodFill.DepthFirstSearch(GenerateTestBitmap(), (-1, -1), Black, White);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public static void BreadthFirstSearch_Test1()
        {
            TestAlgorithm(Algorithms.Other.FloodFill.BreadthFirstSearch, (1, 1), Green, Orange, (1, 1), Orange);
        }

        [Test]
        public static void BreadthFirstSearch_Test2()
        {
            TestAlgorithm(Algorithms.Other.FloodFill.BreadthFirstSearch, (1, 1), Green, Orange, (0, 1), Violet);
        }

        [Test]
        public static void BreadthFirstSearch_Test3()
        {
            TestAlgorithm(Algorithms.Other.FloodFill.BreadthFirstSearch, (1, 1), Green, Orange, (6, 4), White);
        }

        [Test]
        public static void DepthFirstSearch_Test1()
        {
            TestAlgorithm(Algorithms.Other.FloodFill.DepthFirstSearch, (1, 1), Green, Orange, (1, 1), Orange);
        }

        [Test]
        public static void DepthFirstSearch_Test2()
        {
            TestAlgorithm(Algorithms.Other.FloodFill.DepthFirstSearch, (1, 1), Green, Orange, (0, 1), Violet);
        }

        [Test]
        public static void DepthFirstSearch_Test3()
        {
            TestAlgorithm(Algorithms.Other.FloodFill.DepthFirstSearch, (1, 1), Green, Orange, (6, 4), White);
        }

        private static Bitmap GenerateTestBitmap()
        {
            Color[,] layout =
            {
                {Violet, Violet, Green, Green, Black, Green, Green},
                {Violet, Green, Green, Black, Green, Green, Green},
                {Green, Green, Green, Black, Green, Green, Green},
                {Black, Black, Green, Black, White, White, Green},
                {Violet, Violet, Black, Violet, Violet, White, White},
                {Green, Green, Green, Violet, Violet, Violet, Violet},
                {Violet, Violet, Violet, Violet, Violet, Violet, Violet},
            };

            Bitmap bitmap = new(7, 7);
            for (int x = 0; x < layout.GetLength(0); x++)
            {
                for (int y = 0; y < layout.GetLength(1); y++)
                {
                    bitmap.SetPixel(x, y, layout[y, x]);
                }
            }

            return bitmap;
        }

        private static void TestAlgorithm(
            Action<Bitmap, ValueTuple<int, int>, Color, Color> algorithm,
            ValueTuple<int, int> fillLocation,
            Color targetColor,
            Color replacementColor,
            ValueTuple<int, int> testLocation,
            Color expectedColor)
        {
            Bitmap bitmap = GenerateTestBitmap();
            algorithm(bitmap, fillLocation, targetColor, replacementColor);
            Color actualColor = bitmap.GetPixel(testLocation.Item1, testLocation.Item2);
            actualColor.Should().Be(expectedColor);
        }
    }
}
