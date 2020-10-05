using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Search;
using NUnit.Framework;
using NUnit.Framework.Internal;

using Utilities.Exceptions;
using Utilities.Extensions;

namespace Algorithms.Tests.Search
{
    public static class KmpSearcherTests
    {
        [Test]
        public static void FindIndexes_ItemsPresent_IndexsCorrect()
        {
            // Arrange
            var searcher = new KmpSearcher();
            var str = "ABABAcdeABA";
            var pat = "ABA";

            // Act
            var expectedItem = new[]{0,2,8};
            var actualItem = searcher.FindIndexes(str, pat);

            // Assert
            CollectionAssert.AreEqual(expectedItem, actualItem);
        }

        [Test]
        public static void FindIndexes_ItemsMissing_ItemNotFoundExceptionThrown()
        {
            // Arrange
            var searcher = new KmpSearcher();
            var str = "ABABA";
            var pat = "ABB";

            // Act & Assert
            _ = Assert.Throws<ItemNotFoundException>(() => searcher.FindIndexes(str, pat));
        }

    }
}
