// A C# program that implements Z
// algorithm for pattern searching
using System;

namespace Algorithms.Strings
{
    ///     <summary>
    ///     The idea:   ZArraySearch(Content, Pattern) will return an array the index 
    ///     of Content in which the Pattern appears.
    ///     Time: O(m + n)
    ///     Space:  O(n)
    ///     where   
    ///     m - pattern length
    ///     n - length of text
    ///     Source:https://www.tutorialspoint.com/z-algorithm-linear-time-pattern-searching-algorithm-in-cplusplus#:~:text=Z%20algorithm%20is%20used%20to,O(m%2Bn).
    ///     </summary>

    public static class ZAlgorithm
    {
        public static void ZArraySearch(string text, string pattern)
        {
            // Create concatenated string "Pattern$Text"
            string concat = pattern + "$" + text;

            int[] Z = new int[concat.Length];

            int n = concat.Length;

            int L = 0, R = 0;

            for (int i = 1; i < n; ++i)
            {

                // if i>R nothing matches so we will
                // calculate. Z[i] using naive way.
                if (i > R)
                {
                    L = R = i;

                    // R-L = 0 in starting, so it will start
                    // checking from 0'th index. For example,
                    // for "ababab" and i = 1, the value of R
                    // remains 0 and Z[i] becomes 0. For string
                    // "aaaaaa" and i = 1, Z[i] and R become 5
                    while (R < n && concat[R - L] == concat[R])
                    {
                        R++;
                    }

                    Z[i] = R - L;
                    R--;

                }
                else
                {

                    // k = i-L so k corresponds to number
                    // which matches in [L,R] interval.
                    int k = i - L;

                    // if Z[k] is less than remaining interval
                    // then Z[i] will be equal to Z[k].
                    // For example, str = "ababab", i = 3,
                    // R = 5 and L = 2
                    if (Z[k] < R - i + 1)
                    {
                        Z[i] = Z[k];
                    }

                    // For example str = "aaaaaa" and
                    // i = 2, R is 5, L is 0
                    else
                    {


                        // else start from R and
                        // check manually
                        L = i;
                        while (R < n && concat[R - L] == concat[R])
                        {
                            R++;
                        }

                        Z[i] = R - L;
                        R--;
                    }
                }
            }

            // now looping through Z array 
            //identify the presence of pattern in content
            for (int i = 0; i < concat.Length; ++i)
            {

                // if Z[i] (matched region) is equal
                // to pattern length we got the pattern

                if (Z[i] == pattern.Length)
                {
                    Console.WriteLine("\n Pattern found at index " +
                                    (i - pattern.Length - 1));
                }

            }
        }


        // testing code
        public static void Main(string[] args)
        {
            string text = "Do test is test";
            string pattern = "test";

            ZArraySearch(text, pattern);
        }
    }
}

