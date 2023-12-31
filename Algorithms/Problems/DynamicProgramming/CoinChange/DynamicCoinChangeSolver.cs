using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Problems.DynamicProgramming.CoinChange;

public static class DynamicCoinChangeSolver
{
    /// <summary>
    /// Generates an array of changes for current coin.
    /// For instance, having coin C = 6 and array A = [1,3,4] it returns an array R = [2,3,5].
    /// Because, 6 - 4 = 2, 6 - 3 = 3, 6 - 1 = 5.
    /// </summary>
    /// <param name="coin">The value of the coin to be exchanged.</param>
    /// <param name="coins">An array of available coins.</param>
    /// <returns>Array of changes of current coins by available coins.</returns>
    public static int[] GenerateSingleCoinChanges(int coin, int[] coins)
    {
        ValidateCoin(coin);
        ValidateCoinsArray(coins);

        var coinsArrayCopy = new int[coins.Length];

        Array.Copy(coins, coinsArrayCopy, coins.Length);
        Array.Sort(coinsArrayCopy);
        Array.Reverse(coinsArrayCopy);

        var list = new List<int>();

        foreach (var item in coinsArrayCopy)
        {
            if (item > coin)
            {
                continue;
            }

            var difference = coin - item;

            list.Add(difference);
        }

        var result = list.ToArray();

        return result;
    }

    /// <summary>
    /// Given a positive integer N, such as coin.
    /// Generates a change dictionary for all values [1,N].
    /// Used in so-called backward induction in search of the minimum exchange.
    /// </summary>
    /// <param name="coin">The value of coin.</param>
    /// <param name="coins">Array of available coins.</param>
    /// <returns>Change dictionary for all values [1,N], where N is the coin.</returns>
    public static Dictionary<int, int[]> GenerateChangesDictionary(int coin, int[] coins)
    {
        var dict = new Dictionary<int, int[]>();
        var currentCoin = 1;

        while (currentCoin <= coin)
        {
            var changeArray = GenerateSingleCoinChanges(currentCoin, coins);
            dict[currentCoin] = changeArray;
            currentCoin++;
        }

        return dict;
    }

    /// <summary>
    /// Gets a next coin value, such that changes array contains the minimal change overall possible changes.
    /// For example, having coin N = 6 and A = [1,3,4] coins array.
    /// The minimum next coin for 6 will be 3, because changes of 3 by A = [1,3,4] contains 0, the minimal change.
    /// </summary>
    /// <param name="coin">Coin to be exchanged.</param>
    /// <param name="exchanges">Dictionary of exchanges for [1, coin].</param>
    /// <returns>Index of the next coin with minimal exchange.</returns>
    public static int GetMinimalNextCoin(int coin, Dictionary<int, int[]> exchanges)
    {
        var nextCoin = int.MaxValue;
        var minChange = int.MaxValue;

        var coinChanges = exchanges[coin];

        foreach (var change in coinChanges)
        {
            if (change == 0)
            {
                return 0;
            }

            var currentChange = exchanges[change];
            var min = currentChange.Min();

            var minIsLesser = min < minChange;

            if (minIsLesser)
            {
                nextCoin = change;
                minChange = min;
            }
        }

        return nextCoin;
    }

    /// <summary>
    /// Performs a coin change such that an amount of coins is minimal.
    /// </summary>
    /// <param name="coin">The value of coin to be exchanged.</param>
    /// <param name="coins">An array of available coins.</param>
    /// <returns>Array of exchanges.</returns>
    public static int[] MakeCoinChangeDynamic(int coin, int[] coins)
    {
        var changesTable = GenerateChangesDictionary(coin, coins);
        var list = new List<int>();

        var currentCoin = coin;
        var nextCoin = int.MaxValue;

        while (nextCoin != 0)
        {
            nextCoin = GetMinimalNextCoin(currentCoin, changesTable);
            var difference = currentCoin - nextCoin;
            list.Add(difference);
            currentCoin = nextCoin;
        }

        var result = list.ToArray();

        return result;
    }

    private static void ValidateCoin(int coin)
    {
        if (coin <= 0)
        {
            throw new InvalidOperationException($"The coin cannot be lesser or equal to zero {nameof(coin)}.");
        }
    }

    private static void ValidateCoinsArray(int[] coinsArray)
    {
        var coinsAsArray = coinsArray.ToArray();

        if (coinsAsArray.Length == 0)
        {
            throw new InvalidOperationException($"Coins array cannot be empty {nameof(coinsAsArray)}.");
        }

        var coinsContainOne = coinsAsArray.Any(x => x == 1);

        if (!coinsContainOne)
        {
            throw new InvalidOperationException($"Coins array must contain coin 1 {nameof(coinsAsArray)}.");
        }

        var containsNonPositive = coinsAsArray.Any(x => x <= 0);

        if (containsNonPositive)
        {
            throw new InvalidOperationException(
                $"{nameof(coinsAsArray)} cannot contain numbers less than or equal to zero");
        }

        var containsDuplicates = coinsAsArray.GroupBy(x => x).Any(g => g.Count() > 1);

        if (containsDuplicates)
        {
            throw new InvalidOperationException($"Coins array cannot contain duplicates {nameof(coinsAsArray)}.");
        }
    }
}
