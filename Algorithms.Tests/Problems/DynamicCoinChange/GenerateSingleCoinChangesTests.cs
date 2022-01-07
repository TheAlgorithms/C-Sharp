using System;
using Algorithms.Problems.DynamicCoinChange;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Problems.DynamicCoinChange
{
    [TestFixture]
    public class GenerateSingleCoinChangesTests
    {
        [Test]
        public void GenerateSingleCoinChangesTests_Success()
        {
        }

        [Test]
        public void GenerateSingleCoinChangesTests_ShouldThrow_CoinCannotBeLesserOrEqualZero()
        {
            const int coin = 0;
            var arr = new[] { 1, 2, 3 };

            Func<int[]> act = () => DynamicCoinChangeHelper.GenerateSingleCoinChanges(coin, arr);

            act.Should().Throw<IndexOutOfRangeException>()
                .WithMessage($"Coin cannot be lesser or equal to zero {nameof(coin)}.");
        }

        [Test]
        public void GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotBeNull()
        {
            const int coin = 10;
            int[] coinsAsArray = default!;

            Func<int[]> act = () => DynamicCoinChangeHelper.GenerateSingleCoinChanges(coin, coinsAsArray);

            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'enumerableCoins')");
        }

        [Test]
        public void GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotBeEmpty()
        {
            const int coin = 10;
            var coinsAsArray = Array.Empty<int>();

            Func<int[]> act = () => DynamicCoinChangeHelper.GenerateSingleCoinChanges(coin, coinsAsArray);

            act.Should().Throw<IndexOutOfRangeException>()
                .WithMessage($"Coins array cannot be empty {nameof(coinsAsArray)}.");
        }

        [Test]
        public void GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayMustContainOne()
        {
            const int coin = 10;
            var coinsAsArray = new[] { 2, 3, 4 };

            Func<int[]> act = () => DynamicCoinChangeHelper.GenerateSingleCoinChanges(coin, coinsAsArray);

            act.Should().Throw<IndexOutOfRangeException>()
                .WithMessage($"Coins array must contain coin 1 {nameof(coinsAsArray)}.");
        }

        [Test]
        public void GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotContainNegativeValues()
        {
            const int coin = 10;
            var coinsAsArray = new[] { 1, 2, -3, 4 };

            Func<int[]> act = () => DynamicCoinChangeHelper.GenerateSingleCoinChanges(coin, coinsAsArray);

            act.Should().Throw<IndexOutOfRangeException>()
                .WithMessage($"Coins array cannot contain negative numbers {nameof(coinsAsArray)}.");
        }

        [Test]
        public void GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotContainDuplicates()
        {
            const int coin = 10;
            var coinsAsArray = new[] { 1, 2, 3, 3, 4 };

            Func<int[]> act = () => DynamicCoinChangeHelper.GenerateSingleCoinChanges(coin, coinsAsArray);

            act.Should().Throw<IndexOutOfRangeException>()
                .WithMessage($"Coins array cannot contain duplicates {nameof(coinsAsArray)}.");
        }
    }
}
