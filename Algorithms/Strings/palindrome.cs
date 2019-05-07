using System.Linq;

namespace Example
{
    public static partial class StringViewModel
    {
        public static bool IsStringPalindrome(string word) => word.ToLower() == new string(word.Reverse().ToArray()).ToLower();
    }
}