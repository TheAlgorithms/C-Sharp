using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Algorithms.Tests.MachineLearning.DataSets;
using Algorithms.MachineLearning;
using Algorithms.Maths;
using FluentAssertions;

namespace Algorithms.Tests.MachineLearning
{
    internal class KNNRegressionTest
    {

        [Test]
        [TestCase(new[] { 13.71,20.83,90.2,577.9,0.1189,0.1645,0.09366,0.05985,0.2196,0.07451,0.5835,1.377,
            3.856,50.96,0.008805,0.03029,0.02488,0.01448,0.01486,0.005412,17.06,28.14,110.6,
            897,0.1654,0.3682,0.2678,0.1556,0.3196,0.1151}, 1, 123457)]
        [TestCase(new[] { 11.94, 18.24, 75.71, 437.6, 0.08261, 0.04751, 0.01972, 0.01349, 0.1868, 0.0611,
            0.2273, 0.6329, 1.52, 17.47, 0.00721, 0.00838, 0.01311, 0.008, 0.01996, 0.002635, 13.1, 21.33,
            83.67, 527.2, 0.1144, 0.08906, 0.09203, 0.06296, 0.2785, 0.07408}, 0, 987654)]
        [TestCase(new[] { 9.683, 19.34, 61.05, 285.7, 0.08491, 0.0503, 0.02337, 0.009615, 0.158, 0.06235,
            0.2957, 1.363, 2.054, 18.24, 0.00744, 0.01123, 0.02337, 0.009615, 0.02203, 0.004154, 10.93,
            25.59, 69.1, 364.2, 0.1199, 0.09546, 0.0935, 0.03846, 0.2552, 0.0792 }, 0, 54627)]
        public void KNNRegressionPredictTest(
            double[] dataPoint,
            double expected, int seed)
        {
            // Get data and randomize the dataset. 
            BreastCancerWisconsinDataSet dataSet = new BreastCancerWisconsinDataSet();
            dataSet.Randomize(seed);

            // Initialize the model
            var knn = new KNNRegression(dataSet.Data, dataSet.Class, EuclideanDistance.Distance);

            // Predict some values.
            // This is not intended to test the kNN model (eg. cross-validation), but the method.
            // So these are some fixed predictions using the full dataset.
            // We are checking that we can aproximate the expected value, there is no need to be accurate.
            knn.Predict(dataPoint, 7).Should().BeApproximately(expected, 0.2);
        }

        [Test]
        public void KNNRegressionPredictThrowsBadArgumentException()
        {
            // Get data and randomize the dataset. 
            BreastCancerWisconsinDataSet dataSet = new BreastCancerWisconsinDataSet();

            var knn = new KNNRegression(dataSet.Data, dataSet.Class, EuclideanDistance.Distance);

            Action act1 = () => knn.Predict(new[] { 3.8, 1.5, 0.3 }, 3);
            act1.Should().Throw<ArgumentException>();

            Action act2 = () => knn.Predict(new[] { 3.8, 2, 4, 1.5, 0.3 }, 3);
            act2.Should().Throw<ArgumentException>();
        }
    }
}
