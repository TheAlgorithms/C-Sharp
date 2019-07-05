using Algorithms.Strings;
using NUnit.Framework;

namespace Algorithms.Tests.Strings
{
    public class PalindromeTests
    {
        // TODO: Split in 2 methods, use TestCase attribute
        [Test]
        public void InputStringIsPalindrome()
        {
            // Palindrome
            const string validWord = "Anna";
            const string validPhrase = "A Santa at Nasa";
            // Not palindrome
            const string invalidWord = "hallo";
            const string invalidPhare = "Once upon a time";

            // Act
            var resultA = Palindrome.IsStringPalindrome(validWord);
            var resultPhraseA = Palindrome.IsStringPalindrome(validPhrase);
            var resultB = Palindrome.IsStringPalindrome(invalidWord);
            var resultPhraseB = Palindrome.IsStringPalindrome(invalidPhare);

            // Assert
            Assert.True(resultA);
            Assert.True(resultPhraseA);
            Assert.False(resultB);
            Assert.False(resultPhraseB);
        }
    }
}
