using System;
using System.Collections.Generic;
using System.Text;

namespace Example
{
    public static class StringViewModel
    {
        public static bool IsStringPalindrome(string word)
        {

            string input = word.ToLower();
            char[] array = input.ToCharArray();
            Array.Reverse(array);
            var reversed = new String(array);

            if (reversed == input) return true;
            else return false;
        }
    }
}