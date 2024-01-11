using Algorithms.Problems.DynamicProgramming.CoinChange;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Problems.DynamicProgramming.CoinChange;

public class GetMinimalNextCoinTests
{
    [Test]
    public void GetMinimalNextCoinTest_Success()
    {
        const int coin = 6;
        var coins = new[] { 1, 3, 4 };
        var exchangeDict = DynamicCoinChangeSolver.GenerateChangesDictionary(coin, coins);
        var nextCoin = DynamicCoinChangeSolver.GetMinimalNextCoin(6, exchangeDict);

        nextCoin.Should().Be(3);
    }
}
