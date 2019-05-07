using System;

namespace Example
{
    public static partial class StringViewModel
    {

        public static Tuple<char, int> LongesConsecutiveCharacters(string input)
        {
            var max_char = input[0];
            var current_char = input[0];

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