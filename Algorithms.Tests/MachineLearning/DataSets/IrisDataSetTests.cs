using System;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.MachineLearning.DataSets
{
    internal class IrisDataSetTests
    {
        [Test]
        public void IrisDataSetTest()
        {
            IrisDataSet dataset = new IrisDataSet();
            dataset.Should().NotBeNull();
            dataset.Data.Should().HaveCount(150);
            dataset.Class.Should().HaveCount(150);
            dataset.Data[149].Should().NotBeNull();
        }

        [Test]
        public void IrisDataSetThrowsException()
        {
            string badUrl = "https://archive.ics.uci.edu/ml/machine-learning-databases/iris/";
            Action action = () => {
                new IrisDataSet(badUrl);
            };
            action.Should().Throw<Exception>();
        }
    }
}
