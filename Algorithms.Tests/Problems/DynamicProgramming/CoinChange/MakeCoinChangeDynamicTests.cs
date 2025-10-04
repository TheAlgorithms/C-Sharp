using Algorithms.Problems.DynamicProgramming.CoinChange;

namespace Algorithms.Tests.Problems.DynamicProgramming.CoinChange;

[TestFixture]
public class MakeCoinChangeDynamicTests
{
    [Test]
    public void MakeCoinChangeDynamicTest_Success()
    {
        DynamicCoinChangeSolver
            .MakeCoinChangeDynamic(6, [1, 3, 4])
            .SequenceEqual(new[] { 3, 3 })
            .Should().BeTrue();

        DynamicCoinChangeSolver
            .MakeCoinChangeDynamic(8, [1, 3, 4])
            .SequenceEqual(new[] { 4, 4 })
            .Should().BeTrue();

        DynamicCoinChangeSolver
            .MakeCoinChangeDynamic(25, [1, 3, 4, 12, 13, 14])
            .SequenceEqual(new[] { 13, 12 })
            .Should().BeTrue();

        DynamicCoinChangeSolver
            .MakeCoinChangeDynamic(26, [1, 3, 4, 12, 13, 14])
            .SequenceEqual(new[] { 14, 12 })
            .Should().BeTrue();
    }
}
