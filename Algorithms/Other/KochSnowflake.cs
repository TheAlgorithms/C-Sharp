using System;
using System.Collections.Generic;
using System.Numerics;
using SkiaSharp;

namespace Algorithms.Other;

/// <summary>
///     The Koch snowflake is a fractal curve and one of the earliest fractals to
///     have been described. The Koch snowflake can be built up iteratively, in a
///     sequence of stages. The first stage is an equilateral triangle, and each
///     successive stage is formed by adding outward bends to each side of the
///     previous stage, making smaller equilateral triangles.
///     This can be achieved through the following steps for each line:
///     1. divide the line segment into three segments of equal length.
///     2. draw an equilateral triangle that has the middle segment from step 1
///     as its base and points outward.
///     3. remove the line segment that is the base of the triangle from step 2.
///     (description adapted from https://en.wikipedia.org/wiki/Koch_snowflake )
///     (for a more detailed explanation and an implementation in the
///     Processing language, see  https://natureofcode.com/book/chapter-8-fractals/
///     #84-the-koch-curve-and-the-arraylist-technique ).
/// </summary>
public static class KochSnowflake
{
    /// <summary>
    ///     Go through the number of iterations determined by the argument "steps".
    ///     Be careful with high values (above 5) since the time to calculate increases
    ///     exponentially.
    /// </summary>
    /// <param name="initialVectors">
    ///     The vectors composing the shape to which
    ///     the algorithm is applied.
    /// </param>
    /// <param name="steps">The number of iterations.</param>
    /// <returns>The transformed vectors after the iteration-steps.</returns>
    public static List<Vector2> Iterate(List<Vector2> initialVectors, int steps = 5)
    {
        List<Vector2> vectors = initialVectors;
        for (var i = 0; i < steps; i++)
        {
            vectors = IterationStep(vectors);
        }

        return vectors;
    }

    /// <summary>
    ///     Method to render the Koch snowflake to a bitmap. To save the
    ///     bitmap the command 'GetKochSnowflake().Save("KochSnowflake.png")' can be used.
    /// </summary>
    /// <param name="bitmapWidth">The width of the rendered bitmap.</param>
    /// <param name="steps">The number of iterations.</param>
    /// <returns>The bitmap of the rendered Koch snowflake.</returns>
    public static SKBitmap GetKochSnowflake(
        int bitmapWidth = 600,
        int steps = 5)
    {
        if (bitmapWidth <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(bitmapWidth),
                $"{nameof(bitmapWidth)} should be greater than zero");
        }

        var offsetX = bitmapWidth / 10f;
        var offsetY = bitmapWidth / 3.7f;
        var vector1 = new Vector2(offsetX, offsetY);
        var vector2 = new Vector2(bitmapWidth / 2, (float)Math.Sin(Math.PI / 3) * bitmapWidth * 0.8f + offsetY);
        var vector3 = new Vector2(bitmapWidth - offsetX, offsetY);
        List<Vector2> initialVectors = new() { vector1, vector2, vector3, vector1 };
        List<Vector2> vectors = Iterate(initialVectors, steps);
        return GetBitmap(vectors, bitmapWidth, bitmapWidth);
    }

    /// <summary>
    ///     Loops through each pair of adjacent vectors. Each line between two adjacent
    ///     vectors is divided into 4 segments by adding 3 additional vectors in-between
    ///     the original two vectors. The vector in the middle is constructed through a
    ///     60 degree rotation so it is bent outwards.
    /// </summary>
    /// <param name="vectors">
    ///     The vectors composing the shape to which
    ///     the algorithm is applied.
    /// </param>
    /// <returns>The transformed vectors after the iteration-step.</returns>
    private static List<Vector2> IterationStep(List<Vector2> vectors)
    {
        List<Vector2> newVectors = new();
        for (var i = 0; i < vectors.Count - 1; i++)
        {
            var startVector = vectors[i];
            var endVector = vectors[i + 1];
            newVectors.Add(startVector);
            var differenceVector = endVector - startVector;
            newVectors.Add(startVector + differenceVector / 3);
            newVectors.Add(startVector + differenceVector / 3 + Rotate(differenceVector / 3, 60));
            newVectors.Add(startVector + differenceVector * 2 / 3);
        }

        newVectors.Add(vectors[^1]);
        return newVectors;
    }

    /// <summary>
    ///     Standard rotation of a 2D vector with a rotation matrix
    ///     (see https://en.wikipedia.org/wiki/Rotation_matrix ).
    /// </summary>
    /// <param name="vector">The vector to be rotated.</param>
    /// <param name="angleInDegrees">The angle by which to rotate the vector.</param>
    /// <returns>The rotated vector.</returns>
    private static Vector2 Rotate(Vector2 vector, float angleInDegrees)
    {
        var radians = angleInDegrees * (float)Math.PI / 180;
        var ca = (float)Math.Cos(radians);
        var sa = (float)Math.Sin(radians);
        return new Vector2(ca * vector.X - sa * vector.Y, sa * vector.X + ca * vector.Y);
    }

    /// <summary>
    ///     Utility-method to render the Koch snowflake to a bitmap.
    /// </summary>
    /// <param name="vectors">The vectors defining the edges to be rendered.</param>
    /// <param name="bitmapWidth">The width of the rendered bitmap.</param>
    /// <param name="bitmapHeight">The height of the rendered bitmap.</param>
    /// <returns>The bitmap of the rendered edges.</returns>
    private static SKBitmap GetBitmap(
        List<Vector2> vectors,
        int bitmapWidth,
        int bitmapHeight)
    {
        SKBitmap bitmap = new(bitmapWidth, bitmapHeight);
        var canvas = new SKCanvas(bitmap);

        // Set the background white
        var rect = SKRect.Create(0, 0, bitmapWidth, bitmapHeight);

        var paint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.White,
        };

        canvas.DrawRect(rect, paint);

        paint.Color = SKColors.Black;

        // Draw the edges
        for (var i = 0; i < vectors.Count - 1; i++)
        {
            var x1 = vectors[i].X;
            var y1 = vectors[i].Y;
            var x2 = vectors[i + 1].X;
            var y2 = vectors[i + 1].Y;

            canvas.DrawLine(new SKPoint(x1, y1), new SKPoint(x2, y2), paint);
        }

        return bitmap;
    }
}
