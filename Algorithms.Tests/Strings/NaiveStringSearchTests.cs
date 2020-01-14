using Algorithms.Strings;
using NUnit.Framework;
using System.Linq;

namespace Algorithms.Tests.Strings
{
   public static class NaiveStringSearchTests
   {
      [Test]
      public static void ThreeMatchesFound_PassExpected()
      {
         // Arrange
         var pattern = "ABB";
         var content = "ABBBAAABBAABBBBAB";

         // Act
         var expectedOccurrences = new [] {0, 6, 10};
         var actualOccurrences = NaiveStringSearch.NaiveSearch(content, pattern);
         bool sequencesAreEqual = expectedOccurrences.SequenceEqual(actualOccurrences);

         // Assert
         Assert.IsTrue(sequencesAreEqual);
      }

      [Test]
      public static void OneMatchFound_PassExpected()
      {
         // Arrange
         var pattern = "BAAB";
         var content = "ABBBAAABBAABBBBAB";

         // Act
         var expectedOccurrences = new [] {8};
         var actualOccurrences = NaiveStringSearch.NaiveSearch(content, pattern);
         bool sequencesAreEqual = expectedOccurrences.SequenceEqual(actualOccurrences);

         // Assert
         Assert.IsTrue(sequencesAreEqual);
      }

      [Test]
      public static void NoMatchFound_PassExpected()
      {
         // Arrange
         var pattern = "XYZ";
         var content = "ABBBAAABBAABBBBAB";

         // Act
         var expectedOccurrences = new System.Int32[0];
         var actualOccurrences = NaiveStringSearch.NaiveSearch(content, pattern);
         bool sequencesAreEqual = expectedOccurrences.SequenceEqual(actualOccurrences);

         // Assert
         Assert.IsTrue(sequencesAreEqual);
      }
   }
}
