using System;
using System.Collections.Generic;
using System.Text;

namespace Example
{
    public static class StringViewModel
    {

        public static Tuple<char, int> LongesConsecutiveCharacters(string input)
        {
            char max_char = input[0];
            char current_char = input[0];

            int max = 1;
            int current = 1;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == input[i - 1])
                {
                    current++;
                    if (current > max)
                    {
                        max = current;
                        max_char = input[i];
                    }
                }
                else
                {
                    current_char = input[i];
                    current = 1;
                }
            }

            return new Tuple<char, int>(max_char, max);
        }


    }
}