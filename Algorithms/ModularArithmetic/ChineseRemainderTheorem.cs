using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.ModularArithmetic;

/// <summary>
/// Chinese Remainder Theorem: https://en.wikipedia.org/wiki/Chinese_remainder_theorem.
/// </summary>
public static class ChineseRemainderTheorem
{
    /// <summary>
    ///     The Chinese Remainder Theorem is used to compute x for given set of pairs of integers (a_i, n_i) with:
    ///     <list type="bullet">
    ///         <item>x = a_0 mod n_0</item>
    ///         <item>x = a_1 mod n_1</item>
    ///         <item>...</item>
    ///         <item>x = a_k mod n_k</item>
    ///     </list>
    ///     for 0 &lt;= i &lt; k, for some positive integer k. Additional requirements are:
    ///     <list type="bullet">
    ///         <item>n_i > 1 for 0 &lt;= i &lt; k</item>
    ///         <item>n_i and n_j are coprime, for all 0 &lt;= i &lt; j &lt; k</item>
    ///         <item>0 &lt;= a_i &lt; n_i, for all 0 &lt;= i &lt; k</item>
    ///         <item>0 &lt;= x &lt; n_0 * n_1 * ... * n_(k-1)</item>
    ///     </list>
    /// </summary>
    /// <param name="listOfAs">An ordered list of a_0, a_1, ..., a_k.</param>
    /// <param name="listOfNs">An ordered list of n_0, n_1, ..., n_k.</param>
    /// <returns>The value x.</returns>
    /// <exception cref="ArgumentException">If any of the requirements is not fulfilled.</exception>
    public static long Compute(List<long> listOfAs, List<long> listOfNs)
    {
        // Check the requirements for this algorithm:
        CheckRequirements(listOfAs, listOfNs);

        // For performance-reasons compute the product of all n_i as prodN, because we're going to need access to (prodN / n_i) for all i:
        var prodN = 1L;
        foreach (var n in listOfNs)
        {
            prodN *= n;
        }

        var result = 0L;
        for (var i = 0; i < listOfNs.Count; i++)
        {
            var a_i = listOfAs[i];
            var n_i = listOfNs[i];
            var modulus_i = prodN / n_i;

            var bezout_modulus_i = ExtendedEuclideanAlgorithm.Compute(n_i, modulus_i).bezoutB;
            result += a_i * bezout_modulus_i * modulus_i;
        }

        // Make sure, result is in [0, prodN).
        result %= prodN;
        if (result < 0)
        {
            result += prodN;
        }

        return result;
    }

    /// <summary>
    ///     The Chinese Remainder Theorem is used to compute x for given set of pairs of integers (a_i, n_i) with:
    ///     <list type="bullet">
    ///         <item>x = a_0 mod n_0</item>
    ///         <item>x = a_1 mod n_1</item>
    ///         <item>...</item>
    ///         <item>x = a_k mod n_k</item>
    ///     </list>
    ///     for 0 &lt;= i &lt; k, for some positive integer k. Additional requirements are:
    ///     <list type="bullet">
    ///         <item>n_i > 1 for 0 &lt;= i &lt; k</item>
    ///         <item>n_i and n_j are coprime, for all 0 &lt;= i &lt; j &lt; k</item>
    ///         <item>0 &lt;= a_i &lt; n_i, for all 0 &lt;= i &lt; k</item>
    ///         <item>0 &lt;= x &lt; n_0 * n_1 * ... * n_(k-1)</item>
    ///     </list>
    /// </summary>
    /// <param name="listOfAs">An ordered list of a_0, a_1, ..., a_k.</param>
    /// <param name="listOfNs">An ordered list of n_0, n_1, ..., n_k.</param>
    /// <returns>The value x.</returns>
    /// <exception cref="ArgumentException">If any of the requirements is not fulfilled.</exception>
    public static BigInteger Compute(List<BigInteger> listOfAs, List<BigInteger> listOfNs)
    {
        // Check the requirements for this algorithm:
        CheckRequirements(listOfAs, listOfNs);

        // For performance-reasons compute the product of all n_i as prodN, because we're going to need access to (prodN / n_i) for all i:
        var prodN = BigInteger.One;
        foreach (var n in listOfNs)
        {
            prodN *= n;
        }

        var result = BigInteger.Zero;
        for (var i = 0; i < listOfNs.Count; i++)
        {
            var a_i = listOfAs[i];
            var n_i = listOfNs[i];
            var modulus_i = prodN / n_i;

            var bezout_modulus_i = ExtendedEuclideanAlgorithm.Compute(n_i, modulus_i).bezoutB;
            result += a_i * bezout_modulus_i * modulus_i;
        }

        // Make sure, result is in [0, prodN).
        result %= prodN;
        if (result < 0)
        {
            result += prodN;
        }

        return result;
    }

    /// <summary>
    /// Checks the requirements for the algorithm and throws an ArgumentException if they are not being met.
    /// </summary>
    /// <param name="listOfAs">An ordered list of a_0, a_1, ..., a_k.</param>
    /// <param name="listOfNs">An ordered list of n_0, n_1, ..., n_k.</param>
    /// <exception cref="ArgumentException">If any of the requirements is not fulfilled.</exception>
    private static void CheckRequirements(List<long> listOfAs, List<long> listOfNs)
    {
        if (listOfAs == null || listOfNs == null || listOfAs.Count != listOfNs.Count)
        {
            throw new ArgumentException("The parameters 'listOfAs' and 'listOfNs' must not be null and have to be of equal length!");
        }

        if (listOfNs.Any(x => x <= 1))
        {
            throw new ArgumentException($"The value {listOfNs.First(x => x <= 1)} for some n_i is smaller than or equal to 1.");
        }

        if (listOfAs.Any(x => x < 0))
        {
            throw new ArgumentException($"The value {listOfAs.First(x => x < 0)} for some a_i is smaller than 0.");
        }

        // Check if all pairs of (n_i, n_j) are coprime:
        for (var i = 0; i < listOfNs.Count; i++)
        {
            for (var j = i + 1; j < listOfNs.Count; j++)
            {
                long gcd;
                if ((gcd = ExtendedEuclideanAlgorithm.Compute(listOfNs[i], listOfNs[j]).gcd) != 1L)
                {
                    throw new ArgumentException($"The GCD of n_{i} = {listOfNs[i]} and n_{j} = {listOfNs[j]} equals {gcd} and thus these values aren't coprime.");
                }
            }
        }
    }

    /// <summary>
    /// Checks the requirements for the algorithm and throws an ArgumentException if they are not being met.
    /// </summary>
    /// <param name="listOfAs">An ordered list of a_0, a_1, ..., a_k.</param>
    /// <param name="listOfNs">An ordered list of n_0, n_1, ..., n_k.</param>
    /// <exception cref="ArgumentException">If any of the requirements is not fulfilled.</exception>
    private static void CheckRequirements(List<BigInteger> listOfAs, List<BigInteger> listOfNs)
    {
        if (listOfAs == null || listOfNs == null || listOfAs.Count != listOfNs.Count)
        {
            throw new ArgumentException("The parameters 'listOfAs' and 'listOfNs' must not be null and have to be of equal length!");
        }

        if (listOfNs.Any(x => x <= 1))
        {
            throw new ArgumentException($"The value {listOfNs.First(x => x <= 1)} for some n_i is smaller than or equal to 1.");
        }

        if (listOfAs.Any(x => x < 0))
        {
            throw new ArgumentException($"The value {listOfAs.First(x => x < 0)} for some a_i is smaller than 0.");
        }

        // Check if all pairs of (n_i, n_j) are coprime:
        for (var i = 0; i < listOfNs.Count; i++)
        {
            for (var j = i + 1; j < listOfNs.Count; j++)
            {
                BigInteger gcd;
                if ((gcd = ExtendedEuclideanAlgorithm.Compute(listOfNs[i], listOfNs[j]).gcd) != BigInteger.One)
                {
                    throw new ArgumentException($"The GCD of n_{i} = {listOfNs[i]} and n_{j} = {listOfNs[j]} equals {gcd} and thus these values aren't coprime.");
                }
            }
        }
    }
}
