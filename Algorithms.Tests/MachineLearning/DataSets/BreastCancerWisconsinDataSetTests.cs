using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Tests.MachineLearning.DataSets
{
    internal class BreastCancerWisconsinDataSetTests
    {
        [Test]
        public void BreastCancerWisconsinDataSetTest()
        {
            BreastCancerWisconsinDataSet dataset = new BreastCancerWisconsinDataSet();
            dataset.Should().NotBeNull();
            dataset.Data.Should().HaveCount(569);
            dataset.Class.Should().HaveCount(569);
            dataset.Data[568].Should().NotBeNull();
        }

        [Test]
        [TestCase("https://nothing.here")]
        [TestCase("https://archive.ics.uci.edu/ml/machine-learning-databases/iris/")]
        public void BreastCancerWisconsinDataSetThrowsException(string badUrl)
        {
            Action action = () =>
            {
                new BreastCancerWisconsinDataSet(badUrl).Should().BeNull();
            };
            action.Should().Throw<Exception>();
        }
    }
}
