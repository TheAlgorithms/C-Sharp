using Algorithms.Problems.DynamicCoinChange;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Problems.DynamicCoinChange
{
    public class GetMinimalNextCoinTests
    {
        [Test]
        public void GetMinimalNextCoinTest_Success()
        {
            const int coin = 6;
            var coins = new[] { 1, 3, 4 };
            var exchangeDict = DynamicCoinChangeHelper.GenerateChangesDictionary(coin, coins);
            var nextCoin = DynamicCoinChangeHelper.GetMinimalNextCoin(6, exchangeDict);

            nextCoin.Should().Be(3);
        }
    }
}
