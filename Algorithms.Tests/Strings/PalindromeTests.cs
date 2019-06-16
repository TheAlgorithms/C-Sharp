using Algorithms.Strings;
using NUnit.Framework;

namespace Algorithms.Tests.Strings
{
    public class PalindromeTests
    {
        [Test]
        [Parallelizable]
        public void InputStringIsPalindrome()
        {
            // Valid word
            const string validWord = "Anna";
            const string validPhrase = "A Santa at Nasa";
            // Invalid word
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
