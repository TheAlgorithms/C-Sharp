using System;
using System.Linq;
using Algorithms.Problems.DynamicProgramming.CoinChange;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Problems.DynamicProgramming.CoinChange;

[TestFixture]
public class GenerateSingleCoinChangesTests
{
    [Test]
    public void GenerateSingleCoinChangesTests_Success()
    {
        DynamicCoinChangeSolver
            .GenerateSingleCoinChanges(6, new[] { 1, 2, 3 })
            .SequenceEqual(new[] { 3, 4, 5 })
            .Should().BeTrue();

        DynamicCoinChangeSolver
            .GenerateSingleCoinChanges(10, new[] { 1, 2, 3, 7, 12, 15, 14 })
            .SequenceEqual(new[] { 3, 7, 8, 9 })
            .Should().BeTrue();

        DynamicCoinChangeSolver
            .GenerateSingleCoinChanges(1, new[] { 1, 2, 3, 7, 12, 15, 14 })
            .SequenceEqual(new[] { 0 })
            .Should().BeTrue();

        DynamicCoinChangeSolver
            .GenerateSingleCoinChanges(2, new[] { 1, 2, 3, 7, 12, 15, 14 })
            .SequenceEqual(new[] { 0, 1 })
            .Should().BeTrue();
    }

    [Test]
    public void GenerateSingleCoinChangesTests_ShouldThrow_CoinCannotBeLesserOrEqualZero()
    {
        const int coin = 0;
        var arr = new[] { 1, 2, 3 };

        Func<int[]> act = () => DynamicCoinChangeSolver.GenerateSingleCoinChanges(coin, arr);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"The coin cannot be lesser or equal to zero {nameof(coin)}.");
    }

    [Test]
    public void GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotBeEmpty()
    {
        const int coin = 10;
        var coinsAsArray = Array.Empty<int>();

        Func<int[]> act = () => DynamicCoinChangeSolver.GenerateSingleCoinChanges(coin, coinsAsArray);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"Coins array cannot be empty {nameof(coinsAsArray)}.");
    }

    [Test]
    public void GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayMustContainOne()
    {
        const int coin = 10;
        var coinsAsArray = new[] { 2, 3, 4 };

        Func<int[]> act = () => DynamicCoinChangeSolver.GenerateSingleCoinChanges(coin, coinsAsArray);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"Coins array must contain coin 1 {nameof(coinsAsArray)}.");
    }

    [Test]
    public void GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotContainNegativeValues()
    {
        const int coin = 10;
        var coinsAsArray = new[] { 1, 2, -3, 4 };

        Func<int[]> act = () => DynamicCoinChangeSolver.GenerateSingleCoinChanges(coin, coinsAsArray);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"{nameof(coinsAsArray)} cannot contain numbers less than or equal to zero");
    }

    [Test]
    public void GenerateSingleCoinChangesTests_ShouldThrow_CoinsArrayCannotContainDuplicates()
    {
        const int coin = 10;
        var coinsAsArray = new[] { 1, 2, 3, 3, 4 };

        Func<int[]> act = () => DynamicCoinChangeSolver.GenerateSingleCoinChanges(coin, coinsAsArray);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"Coins array cannot contain duplicates {nameof(coinsAsArray)}.");
    }
}
