using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Algorithms.Other
{
    /// <summary>
    /// The Koch snowflake is a fractal curve and one of the earliest fractals to
    /// have been described. The Koch snowflake can be built up iteratively, in a
    /// sequence of stages. The first stage is an equilateral triangle, and each
    /// successive stage is formed by adding outward bends to each side of the
    /// previous stage, making smaller equilateral triangles.
    /// This can be achieved through the following steps for each line:
    ///     1. divide the line segment into three segments of equal length.
    ///     2. draw an equilateral triangle that has the middle segment from step 1
    ///     as its base and points outward.
    ///     3. remove the line segment that is the base of the triangle from step 2.
    /// (description adapted from https://en.wikipedia.org/wiki/Koch_snowflake )
    /// (for a more detailed explanation and an implementation in the
    /// Processing language, see  https://natureofcode.com/book/chapter-8-fractals/
    /// #84-the-koch-curve-and-the-arraylist-technique ).
    /// </summary>
    public static class KochSnowflake
    {
        /// <summary>
        /// Go through the number of iterations determined by the argument "steps".
        /// Be careful with high values (above 5) since the time to calculate increases
        /// exponentially.
        /// </summary>
        /// <param name="initialVectors">The vectors composing the shape to which
        /// the algorithm is applied.</param>
        /// <param name="steps">The number of iterations.</param>
        /// <returns>The transformed vectors after the iteration-steps.</returns>
        public static List<Vector2> Iterate(List<Vector2> initialVectors, int steps = 5)
        {
            List<Vector2> vectors = initialVectors;
            for (int i = 0; i < steps; i++)
            {
                vectors = IterationStep(vectors);
            }

            return vectors;
        }

        /// <summary>
        /// Utility-method to render the Koch snowflake to a bitmap.
        /// </summary>
        /// <param name="vectors">The vectors defining the edges to be rendered.</param>
        /// <param name="bitmapWidth">The width of the rendered bitmap.</param>
        /// <param name="bitmapHeight">The height of the rendered bitmap.</param>
        /// <param name="bitmapHeight">The height of the rendered bitmap.</param>
        /// <returns>The bitmap of the rendered edges.</returns>
        public static Bitmap GetBitmap(
            List<Vector2> vectors,
            int bitmapWidth = 600,
            int bitmapHeight = 600)
        {
            if (bitmapWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bitmapWidth), $"{nameof(bitmapWidth)} should be greater than zero");
            }

            if (bitmapHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bitmapHeight), $"{nameof(bitmapHeight)} should be greater than zero");
            }

            Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight);

            // Set the background white
            using (Graphics graph = Graphics.FromImage(bitmap))
            {
                Rectangle ImageSize = new Rectangle(0, 0, bitmapWidth,bitmapHeight);
                graph.FillRectangle(Brushes.White, ImageSize);
            }

            // Draw the edges
            for (int i = 0; i < vectors.Count-1; i++)
            {
                Pen blackPen = new Pen(Color.Black, 1);

                float x1 = vectors[i].X;
                float y1 = vectors[i].Y;
                float x2 = vectors[i+1].X;
                float y2 = vectors[i+1].Y;
                
                using (var graphics = Graphics.FromImage(bitmap))
                {
                   graphics.DrawLine(blackPen, x1, y1, x2, y2);
                }
            }
            
            return bitmap;
        }
        
        /// <summary>
        /// Example-method to render the Koch snowflake to a bitmap. To save the
        /// bitmap the command 'GetKochSnowflake().Save("KochSnowflake.png")' can be used.
        /// </summary>
        /// <returns>The bitmap of the rendered Koch snowflake.</returns>
        public static Bitmap GetKochSnowflake()
        {
            Vector2 vector1 = new Vector2(50, 150);
            Vector2 vector2 = new Vector2(300, (float)Math.Sin(Math.PI/3)*500 + 150);
            Vector2 vector3 = new Vector2(550, 150);
            List<Vector2> initialVectors = new List<Vector2>{ vector1, vector2, vector3, vector1 };
            List<Vector2> vectors = Iterate(initialVectors);
            return GetBitmap(vectors);
        }

        /// <summary>
        /// Loops through each pair of adjacent vectors. Each line between two adjacent
        /// vectors is divided into 4 segments by adding 3 additional vectors in-between
        /// the original two vectors. The vector in the middle is constructed through a
        /// 60 degree rotation so it is bent outwards.
        /// </summary>
        /// <param name="vectors">The vectors composing the shape to which
        /// the algorithm is applied.</param>
        /// <returns>The transformed vectors after the iteration-step.</returns>
        private static List<Vector2> IterationStep(List<Vector2> vectors)
        {
            List<Vector2> newVectors = new List<Vector2>();
            for (int i = 0; i < vectors.Count - 1; i++)
            {
                Vector2 startVector = vectors[i];
                Vector2 endVector = vectors[i + 1];
                newVectors.Add(startVector);
                Vector2 differenceVector = endVector - startVector;
                newVectors.Add(startVector + differenceVector / 3);
                newVectors.Add(startVector + differenceVector / 3 + Rotate(differenceVector / 3, 60));
                newVectors.Add(startVector + differenceVector * 2 / 3);
            }
            newVectors.Add(vectors[vectors.Count-1]);
            return newVectors;
        }

        /// <summary>
        /// Standard rotation of a 2D vector with a rotation matrix
        /// (see https://en.wikipedia.org/wiki/Rotation_matrix ).
        /// </summary>
        /// <param name="vector">The vector to be rotated.</param>
        /// <param name="angleInDegrees">The angle by which to rotate the vector.</param>
        /// <returns>The rotated vector.</returns>
        private static Vector2 Rotate(Vector2 vector, float angleInDegrees)
        {
            float radians = angleInDegrees * (float)Math.PI/180;
            float ca = (float)Math.Cos(radians);
            float sa = (float)Math.Sin(radians);
            return new Vector2(ca*vector.X - sa*vector.Y, sa*vector.X + ca*vector.Y);
        }
    }
}
