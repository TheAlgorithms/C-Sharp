using System.Linq;
using Algorithms.Problems.DynamicCoinChange;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Problems.DynamicCoinChange
{
    [TestFixture]
    public class MakeCoinChangeDynamicTests
    {
        [Test]
        public void MakeCoinChangeDynamicTest_Success()
        {
            DynamicCoinChangeHelper
                .MakeCoinChangeDynamic(6, new[] { 1, 3, 4 })
                .SequenceEqual(new[] { 3, 3 })
                .Should().BeTrue();

            DynamicCoinChangeHelper
                .MakeCoinChangeDynamic(8, new[] { 1, 3, 4 })
                .SequenceEqual(new[] { 4, 4 })
                .Should().BeTrue();

            DynamicCoinChangeHelper
                .MakeCoinChangeDynamic(25, new[] { 1, 3, 4, 12, 13, 14 })
                .SequenceEqual(new[] { 13, 12 })
                .Should().BeTrue();

            DynamicCoinChangeHelper
                .MakeCoinChangeDynamic(26, new[] { 1, 3, 4, 12, 13, 14 })
                .SequenceEqual(new[] { 14, 12 })
                .Should().BeTrue();
        }
    }
}
