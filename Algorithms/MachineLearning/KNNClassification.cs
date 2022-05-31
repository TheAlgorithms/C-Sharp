using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.MachineLearning
{
    /// <summary>
    /// K nearest neighbors for classification.
    /// </summary>
    /// <typeparam name="T">Type of the class of the data points.</typeparam>
    public class KNNClassification<T> : KNearestNeighbors<T>
    {
        public KNNClassification(double[][] data, T[] classes, Func<double[], double[], double> distanceFunction)
           : base(data, classes, distanceFunction)
        {
        }

        /// <summary>
        /// Get the mean value of the the k nearest neighbors classes.
        /// </summary>
        /// <param name="votes">Values of the k nearest neighbors.</param>
        /// <returns>The average (mean) value of the k nearest neighbors.</returns>
        protected override T GetResult(List<T> votes)
        {
            return votes.GroupBy(v => v).OrderByDescending(gp => gp.Count()).Take(1).Select(g => g.Key).First();
        }
    }
}
