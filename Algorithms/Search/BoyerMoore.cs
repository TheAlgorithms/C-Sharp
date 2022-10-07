using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Search
{
    /// <summary>
    ///     A boyer-moore majority finder algorithm implementation.
    /// </summary>
    /// <typeparam name="T">Type of element stored inside array. 2.</typeparam>
    public class BoyerMoore<T> where T : IComparable
    {
        public static T FindMajority(IEnumerable<T> input)
        {
            var candidate = findMajorityCandidate(input, input.Count());

            if (verify(input, input.Count(), candidate))
            {
                return candidate;
            }

            return default(T);
        }

        //Find majority candidate
        private static T findMajorityCandidate(IEnumerable<T> input, int length)
        {
            int count = 1;
            T candidate = input.First();

            foreach (var element in input.Skip(1))
            {
                if (candidate.Equals(element))
                {
                    count++;
                }
                else
                {
                    count--;
                }

                if (count == 0)
                {
                    candidate = element;
                    count = 1;
                }
            }

            return candidate;
        }

        //verify that candidate is indeed the majority
        private static bool verify(IEnumerable<T> input, int size, T candidate)
        {
            return input.Count(x => x.Equals(candidate)) > size / 2;
        }
    }
}
