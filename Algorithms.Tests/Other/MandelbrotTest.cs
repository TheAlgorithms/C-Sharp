using System;
using Algorithms.Other;
using NUnit.Framework;
using SkiaSharp;

namespace Algorithms.Tests.Other;

public static class MandelbrotTest
{
    [Test]
    public static void BitmapWidthIsZeroOrNegative_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Mandelbrot.GetBitmap(-200));
    }

    [Test]
    public static void BitmapHeightIsZeroOrNegative_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Mandelbrot.GetBitmap(bitmapHeight: 0));
    }

    [Test]
    public static void MaxStepIsZeroOrNegative_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Mandelbrot.GetBitmap(maxStep: -1));
    }

    [Test]
    public static void TestBlackAndWhite()
    {
        SKBitmap bitmap = Mandelbrot.GetBitmap(useDistanceColorCoding: false);
        // Pixel outside the Mandelbrot set should be white.
        Assert.That(new SKColor(255, 255, 255, 255), Is.EqualTo(bitmap.GetPixel(0, 0)));

        // Pixel inside the Mandelbrot set should be black.
        Assert.That(new SKColor(0, 0, 0, 255), Is.EqualTo(bitmap.GetPixel(400, 300)));
    }

    [Test]
    public static void TestColorCoded()
    {
        SKBitmap bitmap = Mandelbrot.GetBitmap(useDistanceColorCoding: true);
        // Pixel distant to the Mandelbrot set should be red.
        Assert.That(new SKColor(255, 0, 0, 255), Is.EqualTo(bitmap.GetPixel(0, 0)));

        // Pixel inside the Mandelbrot set should be black.
        Assert.That(new SKColor(0, 0, 0, 255), Is.EqualTo(bitmap.GetPixel(400, 300)));
    }
}
