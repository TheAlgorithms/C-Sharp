using Algorithms.Strings.Similarity;

namespace Algorithms.Tests.Strings.Similarity
{
    [TestFixture]
    public class OptimalStringAlignmentTests
    {
        [Test]
        public void Calculate_IdenticalStrings_ReturnsZero()
        {
            var result = OptimalStringAlignment.Calculate("example", "example");
            result.Should().Be(0.0);
        }

        [Test]
        public void Calculate_FirstStringEmpty_ReturnsLengthOfSecondString()
        {
            var result = OptimalStringAlignment.Calculate("", "example");
            result.Should().Be("example".Length);
        }

        [Test]
        public void Calculate_SecondStringEmpty_ReturnsLengthOfFirstString()
        {
            var result = OptimalStringAlignment.Calculate("example", "");
            result.Should().Be("example".Length);
        }

        [Test]
        public void Calculate_BothStringsEmpty_ReturnsZero()
        {
            var result = OptimalStringAlignment.Calculate("", "");
            result.Should().Be(0.0);
        }

        [Test]
        public void Calculate_OneInsertion_ReturnsOne()
        {
            var result = OptimalStringAlignment.Calculate("example", "examples");
            result.Should().Be(1.0);
        }

        [Test]
        public void Calculate_OneDeletion_ReturnsOne()
        {
            var result = OptimalStringAlignment.Calculate("examples", "example");
            result.Should().Be(1.0);
        }

        [Test]
        public void Calculate_OneSubstitution_ReturnsOne()
        {
            var result = OptimalStringAlignment.Calculate("example", "exbmple");
            result.Should().Be(1.0);
        }

        [Test]
        public void Calculate_OneTransposition_ReturnsOne()
        {
            var result = OptimalStringAlignment.Calculate("example", "exmaple");
            result.Should().Be(1.0);
        }

        [Test]
        public void Calculate_MultipleOperations_ReturnsCorrectDistance()
        {
            var result = OptimalStringAlignment.Calculate("kitten", "sitting");
            result.Should().Be(3.0);
        }
    }
}
