using NUnit.Framework;
using System;
using Algorithms.MachineLearning;
using FluentAssertions;
using Algorithms.Tests.MachineLearning.DataSets;
using Algorithms.Maths;

namespace Algorithms.Tests.MachineLearning
{
    /// <summary>
    /// KNN tests. 
    /// </summary>
    /// <remarks>
    /// SingleThreaded is added to avoid conflicts with random when executed on parallel. 
    /// </remarks>
    [SingleThreaded]
    public class KnnClassificationTests
    {
        [Test]
        public void KnnClassificationPredictTest([Random(0, 10000000, 10, Distinct = true)] int seed)
        {
            // Get data and randomize the dataset. 
            IrisDataSet dataSet = new IrisDataSet();
            dataSet.Randomize(seed);

            // Initialize the model
            var knn = new KnnClassification<string>(dataSet.Data, dataSet.Class, EuclideanDistance.Distance);

            // Predict some values.
            // This is not intended to test the kNN model (eg. cross-validation), but the method.
            // So these are some fixed predictions using the full dataset.
            knn.Predict(new [] { 5.1, 3.8, 1.5, 0.3 }, 1).Should().Be(IrisDataSet.ClassIrisSetosa);
            knn.Predict(new [] { 6.0, 2.7, 5.1, 1.6 }, 1).Should().Be(IrisDataSet.ClassIrisVersicolor);
            knn.Predict(new [] { 5.7, 2.5, 5.0, 2.0 }, 1).Should().Be(IrisDataSet.ClassIrisVirginica);
            knn.Predict(new [] { 5.9, 3.0, 4.2, 1.5 }, 1).Should().Be(IrisDataSet.ClassIrisVersicolor);
        }

        [Test]
        public void KnnClassificationPredictThrowsBadArgumentException()
        {
            IrisDataSet dataSet = new IrisDataSet();

            var knn = new KnnClassification<string>(dataSet.Data, dataSet.Class, EuclideanDistance.Distance);

            Action act1 = () => knn.Predict(new [] { 3.8, 1.5, 0.3 }, 3);
            act1.Should().Throw<ArgumentException>();

            Action act2 = () => knn.Predict(new [] { 3.8, 2, 4, 1.5, 0.3 }, 3);
            act2.Should().Throw<ArgumentException>();
        }


    }
}
