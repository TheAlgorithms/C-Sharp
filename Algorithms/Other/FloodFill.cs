using System;
using System.Drawing;
using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Other
{
    /// <summary>
    /// Flood fill, also called seed fill, is an algorithm that determines and alters the area connected to a given node in a multi-dimensional array with some matching attribute. It is used in the "bucket" fill tool of paint programs to fill connected, similarly-colored areas with a different color.
    /// (description adapted from https://en.wikipedia.org/wiki/Flood_fill).
    /// </summary>
    public static class FloodFill
    {
        private static List<ValueTuple<int, int>> neighbors = new List<ValueTuple<int, int>> { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

        /// <summary>
        /// Implements the flood fill algorithm through a breadth-first approach using a queue.
        /// </summary>
        /// <param name="bitmap">The bitmap to which the algorithm is applied.</param>
        /// <param name="location">The start location on the bitmap.</param>
        /// <param name="targetColor">The old color to be replaced.</param>
        /// <param name="replacementColor">The new color to replace the old one.</param>
        public static void BreadthFirstSearch(Bitmap bitmap, ValueTuple<int, int> location, Color targetColor, Color replacementColor)
        {
            if (location.Item1 < 0 || location.Item1 >= bitmap.Width || location.Item2 < 0 || location.Item2 >= bitmap.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(location), $"{nameof(location)} should point to a pixel within the bitmap");
            }

            List<ValueTuple<int, int>> queue = new List<ValueTuple<int, int>>();
            queue.Add(location);

            while (queue.Count > 0)
            {
                ValueTuple<int, int> currentLocation = queue[0];
                queue.RemoveAt(0);

                if (bitmap.GetPixel(currentLocation.Item1, currentLocation.Item2) == targetColor)
                {
                    bitmap.SetPixel(currentLocation.Item1, currentLocation.Item2, replacementColor);

                    for (int i = 0; i < neighbors.Count; i++)
                    {
                        int x = currentLocation.Item1 + neighbors[i].Item1;
                        int y = currentLocation.Item2 + neighbors[i].Item2;
                        if (x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height)
                        {
                            queue.Add((x, y));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Implements the flood fill algorithm through a depth-first approach through recursion.
        /// </summary>
        /// <param name="bitmap">The bitmap to which the algorithm is applied.</param>
        /// <param name="location">The start location on the bitmap.</param>
        /// <param name="targetColor">The old color to be replaced.</param>
        /// <param name="replacementColor">The new color to replace the old one.</param>
        public static void DepthFirstSearch(Bitmap bitmap, ValueTuple<int, int> location, Color targetColor, Color replacementColor)
        {
            if (location.Item1 < 0 || location.Item1 >= bitmap.Width || location.Item2 < 0 || location.Item2 >= bitmap.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(location), $"{nameof(location)} should point to a pixel within the bitmap");
            }

            DepthFirstFill(bitmap, location, targetColor, replacementColor);
        }

        private static void DepthFirstFill(Bitmap bitmap, ValueTuple<int, int> location, Color targetColor, Color replacementColor)
        {
            if (bitmap.GetPixel(location.Item1, location.Item2) == targetColor)
            {
                bitmap.SetPixel(location.Item1, location.Item2, replacementColor);

                for (int i = 0; i < neighbors.Count; i++)
                {
                    int x = location.Item1 + neighbors[i].Item1;
                    int y = location.Item2 + neighbors[i].Item2;
                    if (x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height)
                    {
                        DepthFirstFill(bitmap, (x, y), targetColor, replacementColor);
                    }
                }
            }
        }
    }
}
