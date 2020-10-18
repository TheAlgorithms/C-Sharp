
using System.Collections.Generic;

namespace PrimeCalculations
{
    public static class PrimeCalc
    {
        //--------------------------------------------------------------------
        // IsPrime
        //--------------------------------------------------------------------

        /// <summary>
        /// Indicates whether the integer given is a prime number
        /// </summary>
        public static bool IsPrime(this int candidate)
        {
            if ((candidate & 1) == 0)
            {
                if (candidate == 2)
                    return true;
                else
                    return false;
            }

            for (int i = 3; (i * i) <= candidate; i += 2)
            {
                if ((candidate % i) == 0)
                    return false;
            }
            return candidate != 1;
        }

        //--------------------------------------------------------------------
        // GetPrimeNumber
        //--------------------------------------------------------------------

        /// <summary>
        /// Returns the Nth prime where N is given
        /// </summary>
        public static int GetPrimeNumber(this int number)
        {
            int counter = 0;
            int candidateNumber = 0;
            while (counter != number)
            {
                candidateNumber++;
                if (IsPrime(candidateNumber))
                {
                    counter++;
                }
            }
            return candidateNumber;
        }

        //--------------------------------------------------------------------
        // GetLargestPrime
        //--------------------------------------------------------------------

        /// <summary>
        /// Returns the largest prime integer from integer given
        /// </summary>
        public static int GetLargestPrime(this int number)
        {
            int candidateNumber = 0;
            int retVal = 0;
            while (candidateNumber != number)
            {
                candidateNumber++;
                if (IsPrime(candidateNumber))
                {
                    retVal = candidateNumber;
                }
            }
            return retVal;
        }

        //--------------------------------------------------------------------
        // GetListOfPrimes
        //--------------------------------------------------------------------

        /// <summary>
        /// Returns a list of N of primes where N is given
        /// </summary>
        public static List<int> GetListOfPrimes(this int number)
        {
            int counter = 0;
            int candidateNumber = 0;
            List<int> returnList = new List<int>();
            while (counter != number)
            {
                candidateNumber++;

                if (IsPrime(candidateNumber))
                {
                    returnList.Add(candidateNumber);
                    counter++;
                }
            }
            return returnList;
        }
    }
}
