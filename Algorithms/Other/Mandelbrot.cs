using System;
using System.Drawing;

namespace Algorithms.Other
{
    /// <summary>
    /// The Mandelbrot set is the set of complex numbers "c" for which the series
    /// "z_(n+1) = z_n * z_n + c" does not diverge, i.e. remains bounded. Thus, a
    /// complex number "c" is a member of the Mandelbrot set if, when starting with
    /// "z_0 = 0" and applying the iteration repeatedly, the absolute value of
    /// "z_n" remains bounded for all "n > 0". Complex numbers can be written as
    /// "a + b*i": "a" is the real component, usually drawn on the x-axis, and "b*i"
    /// is the imaginary component, usually drawn on the y-axis. Most visualizations
    /// of the Mandelbrot set use a color-coding to indicate after how many steps in
    /// the series the numbers outside the set cross the divergence threshold.
    /// Images of the Mandelbrot set exhibit an elaborate and infinitely
    /// complicated boundary that reveals progressively ever-finer recursive detail
    /// at increasing magnifications, making the boundary of the Mandelbrot set a
    /// fractal curve.
    /// (description adapted from https://en.wikipedia.org/wiki/Mandelbrot_set)
    /// (see also https://en.wikipedia.org/wiki/Plotting_algorithms_for_the_Mandelbrot_set).
    /// </summary>
    public static class Mandelbrot
    {
        /// <summary>
        /// Method to generate the bitmap of the Mandelbrot set. Two types of coordinates
        /// are used: bitmap-coordinates that refer to the pixels and figure-coordinates
        /// that refer to the complex numbers inside and outside the Mandelbrot set. The
        /// figure-coordinates in the arguments of this method determine which section
        /// of the Mandelbrot set is viewed. The main area of the Mandelbrot set is
        /// roughly between "-1.5 &lt; x &lt; 0.5" and "-1 &lt; y &lt; 1" in the figure-coordinates.
        /// To save the bitmap the command 'GetBitmap().Save("Mandelbrot.png")' can be used.
        /// </summary>
        /// <param name="bitmapWidth">The width of the rendered bitmap.</param>
        /// <param name="bitmapHeight">The height of the rendered bitmap.</param>
        /// <param name="figureCenterX">The x-coordinate of the center of the figure.</param>
        /// <param name="figureCenterY">The y-coordinate of the center of the figure.</param>
        /// <param name="figureWidth">The width of the figure.</param>
        /// <param name="maxStep">Maximum number of steps to check for divergent behavior.</param>
        /// <param name="useDistanceColorCoding">Render in color or black and white.</param>
        /// <returns>The bitmap of the rendered Mandelbrot set.</returns>
        public static Bitmap GetBitmap(
            int bitmapWidth = 800,
            int bitmapHeight = 600,
            double figureCenterX = -0.6,
            double figureCenterY = 0,
            double figureWidth = 3.2,
            int maxStep = 50,
            bool useDistanceColorCoding = true)
        {
            if (bitmapWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bitmapWidth), $"{nameof(bitmapWidth)} should be greater than zero");
            }

            if (bitmapHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bitmapHeight), $"{nameof(bitmapHeight)} should be greater than zero");
            }

            if (maxStep <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxStep), $"{nameof(maxStep)} should be greater than zero");
            }

            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            var figureHeight = figureWidth / bitmapWidth * bitmapHeight;

            // loop through the bitmap-coordinates
            for (var bitmapX = 0; bitmapX < bitmapWidth; bitmapX++)
            {
                for (var bitmapY = 0; bitmapY < bitmapHeight; bitmapY++)
                {
                    // determine the figure-coordinates based on the bitmap-coordinates
                    var figureX = figureCenterX + ((double)bitmapX / bitmapWidth - 0.5) * figureWidth;
                    var figureY = figureCenterY + ((double)bitmapY / bitmapHeight - 0.5) * figureHeight;

                    var distance = GetDistance(figureX, figureY, maxStep);

                    // color the corresponding pixel based on the selected coloring-function
                    bitmap.SetPixel(
                        bitmapX,
                        bitmapY,
                        useDistanceColorCoding ? ColorCodedColorMap(distance) : BlackAndWhiteColorMap(distance));
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Black and white color-coding that ignores the relative distance. The Mandelbrot
        /// set is black, everything else is white.
        /// </summary>
        /// <param name="distance">Distance until divergence threshold.</param>
        /// <returns>The color corresponding to the distance.</returns>
        private static Color BlackAndWhiteColorMap(double distance)
        {
            return distance >= 1
                ? Color.FromArgb(255, 0, 0, 0)
                : Color.FromArgb(255, 255, 255, 255);
        }

        /// <summary>
        /// Color-coding taking the relative distance into account. The Mandelbrot set
        /// is black.
        /// </summary>
        /// <param name="distance">Distance until divergence threshold.</param>
        /// <returns>The color corresponding to the distance.</returns>
        private static Color ColorCodedColorMap(double distance)
        {
            if (distance >= 1)
            {
                return Color.FromArgb(255, 0, 0, 0);
            }

            // simplified transformation of HSV to RGB
            // distance determines hue
            var hue = 360 * distance;
            double saturation = 1;
            double val = 255;
            var hi = (int)Math.Floor(hue / 60) % 6;
            var f = hue / 60 - Math.Floor(hue / 60);

            var v = (int)val;
            var p = 0;
            var q = (int)(val * (1 - f * saturation));
            var t = (int)(val * (1 - (1 - f) * saturation));

            switch (hi)
            {
                case 0: return Color.FromArgb(255, v, t, p);
                case 1: return Color.FromArgb(255, q, v, p);
                case 2: return Color.FromArgb(255, p, v, t);
                case 3: return Color.FromArgb(255, p, q, v);
                case 4: return Color.FromArgb(255, t, p, v);
                default: return Color.FromArgb(255, v, p, q);
            }
        }

        /// <summary>
        /// Return the relative distance (ratio of steps taken to maxStep) after which the complex number
        /// constituted by this x-y-pair diverges. Members of the Mandelbrot set do not
        /// diverge so their distance is 1.
        /// </summary>
        /// <param name="figureX">The x-coordinate within the figure.</param>
        /// <param name="figureY">The y-coordinate within the figure.</param>
        /// <param name="maxStep">Maximum number of steps to check for divergent behavior.</param>
        /// <returns>The relative distance as the ratio of steps taken to maxStep.</returns>
        private static double GetDistance(double figureX, double figureY, int maxStep)
        {
            var a = figureX;
            var b = figureY;
            var currentStep = 0;
            for (var step = 0; step < maxStep; step++)
            {
                currentStep = step;
                var aNew = a * a - b * b + figureX;
                b = 2 * a * b + figureY;
                a = aNew;

                // divergence happens for all complex number with an absolute value
                // greater than 4 (= divergence threshold)
                if (a * a + b * b > 4)
                {
                    break;
                }
            }

            return (double)currentStep / (maxStep - 1);
        }
    }
}
