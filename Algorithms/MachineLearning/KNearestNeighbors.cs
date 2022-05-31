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
    ///
    /// This algorithm uses feature similarity yo predict the value of any new sample or data point.
    /// That is, the value or classification of a new sample is obtained from the examples in the
    /// training set that most closely resemble the new one.
    /// </summary>
    /// <typeparam name="T">Type of classes. Should be double or float for regression.</typeparam>
    public abstract class KNearestNeighbors<T>
    {
        /// <summary>
        /// Features of the samples.
        /// </summary>
        /// <example>
        /// data[sampleIndex][featureIndex].
        /// </example>
        private readonly double[][] data;

        /// <summary>
        /// Classification or value for each point.
        /// </summary>
        private readonly T[] classes;

        /// <summary>
        /// Function to measure the distance between two points.
        /// </summary>
        private readonly Func<double[], double[], double> distanceFunction;

        public KNearestNeighbors(double[][] data, T[] classes, Func<double[], double[], double> distanceFunction)
        {
            this.data = data;
            this.classes = classes;
            this.distanceFunction = distanceFunction;
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
        public T Predict(double[] point, int k)
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
            List<T> votes = new List<T>();

            for (int i = 0; i < k; i++)
            {
                votes.Add(classes[distances[i].Item1]);
            }

            // Return the classification that has more votes.
            return GetResult(votes);
        }

        /// <summary>
        /// Get the value or class to assign to the new sample.
        /// </summary>
        /// <remarks>
        /// For regression kNN it will return the mean value of the k nearest neighbors.
        /// For classification kNN it will return the most voted class.
        /// </remarks>
        /// <param name="votes">Values or classes of the k nearest neighbors.</param>
        /// <returns>The value or class to set to the new data point.</returns>
        protected abstract T GetResult(List<T> votes);
    }
}
