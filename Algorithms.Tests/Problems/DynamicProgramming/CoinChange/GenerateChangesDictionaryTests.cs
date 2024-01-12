using System.Linq;
using Algorithms.Problems.DynamicProgramming.CoinChange;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Problems.DynamicProgramming.CoinChange;

[TestFixture]
public class GenerateChangesDictionaryTests
{
    [Test]
    public void GenerateChangesDictionaryTest_Success()
    {
        const int coin = 6;
        var coins = new[] { 1, 3, 4 };
        var changeDictionary = DynamicCoinChangeSolver.GenerateChangesDictionary(coin, coins);

        changeDictionary[1].SequenceEqual(new[] { 0 }).Should().BeTrue();
        changeDictionary[2].SequenceEqual(new[] { 1 }).Should().BeTrue();
        changeDictionary[3].SequenceEqual(new[] { 0, 2 }).Should().BeTrue();
        changeDictionary[4].SequenceEqual(new[] { 0, 1, 3 }).Should().BeTrue();
        changeDictionary[5].SequenceEqual(new[] { 1, 2, 4 }).Should().BeTrue();
        changeDictionary[6].SequenceEqual(new[] { 2, 3, 5 }).Should().BeTrue();
    }
}
