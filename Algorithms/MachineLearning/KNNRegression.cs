using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.MachineLearning
{
    /// <summary>
    /// K nearest neighbors for regression.
    /// </summary>
    public class KnnRegression : KNearestNeighbors<double>
    {
        public KnnRegression(double[][] data, double[] classes, Func<double[], double[], double> distanceFunction)
            : base(data, classes, distanceFunction)
        {
        }

        /// <summary>
        /// Get the mean value of the the k nearest neighbors classes.
        /// </summary>
        /// <param name="votes">Values of the k nearest neighbors.</param>
        /// <returns>The average (mean) value of the k nearest neighbors.</returns>
        protected override double GetResult(List<double> votes)
        {
            return votes.Sum() / votes.Count();
        }
    }
}
