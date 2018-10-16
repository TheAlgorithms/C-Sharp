using System;
using System.Collections.Generic;
using System.Text;

namespace Example
{
    public static class StringViewModel
    {
        public static bool IsStringPalindrome(string word)
        {
            return word.ToLower() == new string(word.Reverse()).ToLower();
        }
    }
}