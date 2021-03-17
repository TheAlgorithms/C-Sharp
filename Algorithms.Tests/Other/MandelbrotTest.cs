using System;
using System.Drawing;
using NUnit.Framework;

namespace Algorithms.Tests.Other
{
    public static class MandelbrotTest
    {
        [Test]
        public static void BitmapWidthIsZeroOrNegative_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Algorithms.Other.Mandelbrot.GetBitmap(bitmapWidth:-200));
        }
        
        [Test]
        public static void BitmapHeightIsZeroOrNegative_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Algorithms.Other.Mandelbrot.GetBitmap(bitmapHeight:0));
        }
        
        [Test]
        public static void MaxStepIsZeroOrNegative_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Algorithms.Other.Mandelbrot.GetBitmap(maxStep:-1));
        }
        
        [Test]
        public static void TestBlackAndWhite()
        {
            Bitmap bitmap = Algorithms.Other.Mandelbrot.GetBitmap(useDistanceColorCoding:false);
            // Pixel outside the Mandelbrot set should be white.
            Assert.AreEqual(bitmap.GetPixel(0,0), Color.FromArgb(255, 255, 255, 255));
            
            // Pixel inside the Mandelbrot set should be black.
            Assert.AreEqual(bitmap.GetPixel(400,300), Color.FromArgb(255, 0, 0, 0));
        }
        
        [Test]
        public static void TestColorCoded()
        {
            Bitmap bitmap = Algorithms.Other.Mandelbrot.GetBitmap(useDistanceColorCoding:true);
            // Pixel distant to the Mandelbrot set should be red.
            Assert.AreEqual(bitmap.GetPixel(0,0), Color.FromArgb(255, 255, 0, 0));
            
            // Pixel inside the Mandelbrot set should be black.
            Assert.AreEqual(bitmap.GetPixel(400,300), Color.FromArgb(255, 0, 0, 0));
        }
    }
}