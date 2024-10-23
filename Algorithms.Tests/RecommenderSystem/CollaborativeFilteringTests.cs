using Algorithms.RecommenderSystem;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Algorithms.Tests.RecommenderSystem
{
    [TestFixture]
    public class CollaborativeFilteringTests
    {
        private Mock<ISimilarityCalculator>? mockSimilarityCalculator;
        private CollaborativeFiltering? recommender;
        private Dictionary<string, Dictionary<string, double>> testRatings = null!;

        [SetUp]
        public void Setup()
        {
            mockSimilarityCalculator = new Mock<ISimilarityCalculator>();
            recommender = new CollaborativeFiltering(mockSimilarityCalculator.Object);

            testRatings = new Dictionary<string, Dictionary<string, double>>
            {
                ["user1"] = new()
                {
                    ["item1"] = 5.0,
                    ["item2"] = 3.0,
                    ["item3"] = 4.0
                },
                ["user2"] = new()
                {
                    ["item1"] = 4.0,
                    ["item2"] = 2.0,
                    ["item3"] = 5.0
                },
                ["user3"] = new()
                {
                    ["item1"] = 3.0,
                    ["item2"] = 4.0,
                    ["item4"] = 3.0
                }
            };
        }

        [Test]
        [TestCase("item1", 4.0, 5.0)]
        [TestCase("item2", 2.0, 4.0)]
        public void CalculateSimilarity_WithValidInputs_ReturnsExpectedResults(
            string commonItem,
            double rating1,
            double rating2)
        {
            var user1Ratings = new Dictionary<string, double> { [commonItem] = rating1 };
            var user2Ratings = new Dictionary<string, double> { [commonItem] = rating2 };

            var similarity = recommender?.CalculateSimilarity(user1Ratings, user2Ratings);

            Assert.That(similarity, Is.InRange(-1.0, 1.0));
        }

        [Test]
        public void CalculateSimilarity_WithNoCommonItems_ReturnsZero()
        {
            var user1Ratings = new Dictionary<string, double> { ["item1"] = 5.0 };
            var user2Ratings = new Dictionary<string, double> { ["item2"] = 4.0 };

            var similarity = recommender?.CalculateSimilarity(user1Ratings, user2Ratings);

            Assert.That(similarity, Is.EqualTo(0));
        }

        [Test]
        public void PredictRating_WithNonexistentItem_ReturnsZero()
        {
            var predictedRating = recommender?.PredictRating("nonexistentItem", "user1", testRatings);

            Assert.That(predictedRating, Is.EqualTo(0));
        }

        [Test]
        public void PredictRating_WithOtherUserHavingRatedTargetItem_ShouldCalculateSimilarityAndWeightedSum()
        {
            var targetItem = "item1";
            var targetUser = "user1";

            mockSimilarityCalculator?
                .Setup(s => s.CalculateSimilarity(It.IsAny<Dictionary<string, double>>(), It.IsAny<Dictionary<string, double>>()))
                .Returns(0.8);

            var predictedRating = recommender?.PredictRating(targetItem, targetUser, testRatings);

            Assert.That(predictedRating, Is.Not.EqualTo(0.0d));
            Assert.That(predictedRating, Is.EqualTo(3.5d).Within(0.01));
        }
    }
}
