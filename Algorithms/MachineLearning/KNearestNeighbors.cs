using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.MachineLearning
{
    /// <summary>
    /// K-Nearest Neighbors implementation.
    /// https://en.wikipedia.org/wiki/K-nearest_neighbors_algorithm.
    /// </summary>
    public class KNearestNeighbors
    {
        /// <summary>
        /// Variables values for each point.
        /// </summary>
        private readonly double[][] data;

        /// <summary>
        /// Classification for each point.
        /// </summary>
        private readonly string[] classes;

        public KNearestNeighbors(double[][] data, string[] classes)
        {
            this.data = data;
            this.classes = classes;
        }

        /// <summary>
        /// Predict the classification for the given point using its k nearest neighbors.
        /// </summary>
        /// <param name="point">
        /// Data point to predict its classification. Should have the same dimensions than the points in the dataset.
        /// </param>
        /// <param name="k">Number of neeighbors that will vote for the classification.</param>
        /// <returns>The predicted classification for the given point.</returns>
        /// <exception cref="ArgumentException">
        /// Exception thrown when the point given have different dimensions than the points of the dataset.
        /// </exception>
        public string Predict(double[] point, int k)
        {
            // Tuples of index and distance.
            Tuple<int, double>[] distances = new Tuple<int, double>[classes.Length];

            for (int i = 0; i < classes.Length; i++)
            {
                distances[i] = new Tuple<int, double>(i, Maths.EuclideanDistance.Distance(point, this.data[i]));
            }

            // Sort by distance ascending
            Array.Sort(distances, (x1, x2) => x1.Item2.CompareTo(x2.Item2));

            // Get the votes of the K nearest neighbours
            Dictionary<string, int> votes = new Dictionary<string, int>();
            for (int i = 0; i < k; i++)
            {
                var vote = classes[distances[i].Item1];
                if (votes.ContainsKey(vote))
                {
                    votes[vote]++;
                }
                else
                {
                    votes.Add(vote, 1);
                }
            }

            // Return the classification that has more votes.
            return votes.OrderByDescending(x => x.Value).First().Key;
        }
    }
}
