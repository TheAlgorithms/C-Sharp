using System;

namespace Algorithms.Strings
{
    /// <summary>
    /// Implements simple algorithms on strings.
    /// </summary>
    public static class GeneralStringAlgorithms
    {
        /// <summary>
        /// Finds character that creates longest consecutive substring with single character.
        /// </summary>
        /// <param name="input">String to find in.</param>
        /// <returns>Tuple containing char and number of times it appeared in a row.</returns>
        public static Tuple<char, int> FindLongestConsecutiveCharacters(string input)
        {
            var maxChar = input[0];

            var max = 1;
            var current = 1;

            for (var i = 1; i < input.Length; i++)
            {
                if (input[i] == input[i - 1])
                {
                    current++;
                    if (current > max)
                    {
                        max = current;
                        maxChar = input[i];
                    }
                }
                else
                {
                    current = 1;
                }
            }

            return new Tuple<char, int>(maxChar, max);
        }

        /// <summary>
        /// splits text to number of parts as evenly as possible.
        /// </summary>
        /// <param name="text">a string of text to split.</param>
        /// <param name="parts">the amount of parts to split the text to.</param>
        /// <returns>an array with the parts of the string.</returns>
        public static string[] SplitText(string text, int parts)
        {
            // calculate the remaining part of the division
            int rem = text.Length % parts;
            int charsPerPart = text.Length / parts;

            string[] ret = new string[parts];
            for (int i = 0; i < ret.Length; i++)
            {
                int times = charsPerPart;

                // if there is still a remainder add one from it to the current amount of chars for the part
                if (rem > 0)
                {
                    rem--;
                    times++;
                }

                // add the characters to a new string and remove from original
                string part = text.Substring(0, times);
                text = text.Remove(0, times);

                // add the new string to the array
                ret[i] = part;
            }

            return ret;
        }
    }
}
