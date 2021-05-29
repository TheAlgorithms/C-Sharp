using System;
using NUnit.Framework;
using FluentAssertions;
using System.Drawing;

namespace FloodFillTest
{
    public static class Tests
    {
        private static Color black = Color.FromArgb(255, 0, 0, 0);
        private static Color green = Color.FromArgb(255, 0, 255, 0);
        private static Color violet = Color.FromArgb(255, 255, 0, 255);
        private static Color white = Color.FromArgb(255, 255, 255, 255);
        private static Color orange = Color.FromArgb(255, 255, 128, 0);

        [Test]
        public static void BreadthFirstSearch_ThrowsArgumentOutOfRangeException()
        {
            Action act = () => Algorithms.Other.FloodFill.BreadthFirstSearch(GenerateTestBitmap(), (10, 10), black, white);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public static void DepthFirstSearch_ThrowsArgumentOutOfRangeException()
        {
            Action act = () => Algorithms.Other.FloodFill.DepthFirstSearch(GenerateTestBitmap(), (-1, -1), black, white);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public static void BreadthFirstSearch_Test1()
        {
            TestAlgorithm(Algorithms.Other.FloodFill.BreadthFirstSearch, (1, 1), green, orange, (1, 1), orange);
        }

        [Test]
        public static void BreadthFirstSearch_Test2()
        {
            TestAlgorithm(Algorithms.Other.FloodFill.BreadthFirstSearch, (1, 1), green, orange, (0, 1), violet);
        }

        [Test]
        public static void BreadthFirstSearch_Test3()
        {
            TestAlgorithm(Algorithms.Other.FloodFill.BreadthFirstSearch, (1, 1), green, orange, (6, 4), white);
        }

        [Test]
        public static void DepthFirstSearch_Test1()
        {
            TestAlgorithm(Algorithms.Other.FloodFill.DepthFirstSearch, (1, 1), green, orange, (1, 1), orange);
        }

        [Test]
        public static void DepthFirstSearch_Test2()
        {
            TestAlgorithm(Algorithms.Other.FloodFill.DepthFirstSearch, (1, 1), green, orange, (0, 1), violet);
        }

        [Test]
        public static void DepthFirstSearch_Test3()
        {
            TestAlgorithm(Algorithms.Other.FloodFill.DepthFirstSearch, (1, 1), green, orange, (6, 4), white);
        }

        private static Bitmap GenerateTestBitmap()
        {
            Color[,] layout =
            {
                {violet, violet, green, green, black, green, green},
                {violet, green, green, black, green, green, green},
                {green, green, green, black, green, green, green},
                {black, black, green, black, white, white, green},
                {violet, violet, black, violet, violet, white, white},
                {green, green, green, violet, violet, violet, violet},
                {violet, violet, violet, violet, violet, violet, violet}
            };

            Bitmap bitmap = new Bitmap(7, 7);
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