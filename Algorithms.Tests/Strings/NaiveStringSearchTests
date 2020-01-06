using Algorithms.Strings;
using NUnit.Framework;

namespace Algorithms.Tests.Strings
{
   public static class NaiveStringSearchTests
   {
      [Test]
      public static void ThreeMatchesFound_PassExpected()
      {
         // Arrange
         var pattern = String("ABB");
         var content = String("ABBBAAABBAABBBBAB");

         // Act
         var expectedOccurrences = new int[] {0, 6, 10};
         var actualOccurrences = NaiveStringSearch.NaiveSearch(content, pattern);
         bool sequencesAreEqual = expectedOccurrences.SequenceEqual(actualOccurrences);

         // Assert
         Assert.IsTrue(sequencesAreEqual);
      }

      [Test]
      public static void OneMatchFound_PassExpected()
      {
         // Arrange
         var pattern = String("BAAB");
         var content = String("ABBBAAABBAABBBBAB");

         // Act
         var expectedOccurrences = new int[] {8};
         var actualOccurrences = NaiveStringSearch.NaiveSearch(content, pattern);
         bool sequencesAreEqual = expectedOccurrences.SequenceEqual(actualOccurrences);

         // Assert
         Assert.IsTrue(sequencesAreEqual);
      }

      [Test]
      public static void NoMatchFound_PassExpected()
      {
         // Arrange
         var pattern = String("XYZ");
         var content = String("ABBBAAABBAABBBBAB");

         // Act
         var expectedOccurrences = new System.Int32[0];
         var actualOccurrences = NaiveSearch(content, pattern);
         bool sequencesAreEqual = expectedOccurrences.SequenceEqual(actualOccurrences);

         // Assert
         Assert.IsTrue(sequencesAreEqual);
      }
   }
}
