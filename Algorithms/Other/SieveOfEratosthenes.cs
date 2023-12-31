using System;
using System.Collections.Generic;

namespace Algorithms.Other;

/// <summary>
///     Implements the Sieve of Eratosthenes.
/// </summary>
public class SieveOfEratosthenes
{
    private readonly bool[] primes;

    /// <summary>
    /// Initializes a new instance of the <see cref="SieveOfEratosthenes"/> class.
    /// Uses the Sieve of Eratosthenes to precalculate the primes from 0 up to maximumNumberToCheck.
    /// Requires enough memory to allocate maximumNumberToCheck bytes.
    /// https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes .
    /// </summary>
    /// <param name="maximumNumberToCheck">long which specifies the largest number you wish to know if it is prime.</param>
    public SieveOfEratosthenes(long maximumNumberToCheck)
    {
        primes = new bool[maximumNumberToCheck + 1];

        // initialize primes array
        Array.Fill(this.primes, true, 2, primes.Length - 2);

        for(long i = 2; i * i <= maximumNumberToCheck; i++)
        {
            if (!primes[i])
            {
                continue;
            }

            for(long composite = i * i; composite <= maximumNumberToCheck; composite += i)
            {
                primes[composite] = false;
            }
        }
    }

    /// <summary>
    /// Gets the maximumNumberToCheck the class was instantiated with.
    /// </summary>
    public long MaximumNumber => primes.Length - 1;

    /// <summary>
    /// Returns a boolean indicating whether the number is prime.
    /// </summary>
    /// <param name="numberToCheck">The number you desire to know if it is prime or not.</param>
    /// <returns>A boolean indicating whether the number is prime or not.</returns>
    public bool IsPrime(long numberToCheck) => primes[numberToCheck];

    /// <summary>
    /// Returns an IEnumerable of long primes in asending order.
    /// </summary>
    /// <returns>Primes in ascending order.</returns>
    public IEnumerable<long> GetPrimes()
    {
        for(long i = 2; i < primes.Length; i++)
        {
            if (primes[i])
            {
                yield return i;
            }
        }
    }
}
