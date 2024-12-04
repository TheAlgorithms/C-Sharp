using FluentAssertions;
using NUnit.Framework;
using SkiaSharp;
using System;

namespace Algorithms.Tests.Other;

public static class Tests
{
    private const byte Alpha = 255;
    private static readonly SKColor Black = new(0, 0, 0, Alpha);
    private static readonly SKColor Green = new(0, 255, 0, Alpha);
    private static readonly SKColor Violet = new(255, 0, 255, Alpha);
    private static readonly SKColor White = new(255, 255, 255, Alpha);
    private static readonly SKColor Orange = new(255, 128, 0, Alpha);

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

    private static SKBitmap GenerateTestBitmap()
    {
        SKColor[,] layout =
        {
            {Violet, Violet, Green, Green, Black, Green, Green},
            {Violet, Green, Green, Black, Green, Green, Green},
            {Green, Green, Green, Black, Green, Green, Green},
            {Black, Black, Green, Black, White, White, Green},
            {Violet, Violet, Black, Violet, Violet, White, White},
            {Green, Green, Green, Violet, Violet, Violet, Violet},
            {Violet, Violet, Violet, Violet, Violet, Violet, Violet},
        };

        SKBitmap bitmap = new(7, 7);
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
        Action<SKBitmap, ValueTuple<int, int>, SKColor, SKColor> algorithm,
        ValueTuple<int, int> fillLocation,
        SKColor targetColor,
        SKColor replacementColor,
        ValueTuple<int, int> testLocation,
        SKColor expectedColor)
    {
        SKBitmap bitmap = GenerateTestBitmap();
        algorithm(bitmap, fillLocation, targetColor, replacementColor);
        SKColor actualColor = bitmap.GetPixel(testLocation.Item1, testLocation.Item2);
        actualColor.Should().Be(expectedColor);
    }
}
