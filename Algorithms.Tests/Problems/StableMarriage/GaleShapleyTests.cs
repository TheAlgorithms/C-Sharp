using System;
using System.Linq;

using NUnit.Framework;

namespace Algorithms.Problems.StableMarriage
{
    /// <summary>
    /// The stable marriage problem (also stable matching problem or SMP)
    /// is the problem of finding a stable matching between two equally sized sets of elements given an ordering of preferences for each element.
    /// Current example scores parties on single integer value (closer values are better).
    /// </summary>
    public static class GaleShapleyTests
    {
        /// <summary>
        /// Checks that all parties are engaged to their best possible partners.
        /// Pesimistic test case assumes reversed order of collections.
        /// </summary>
        [Test]
        public static void MatchingIsSuccessful()
        {
            var expectedPairs = Enumerable.Range(1, 10).Select(x => new { Proposer = new Proposer(x), Accepter = new Accepter(x) }).ToArray();

            var proposers = expectedPairs.Select(x => x.Proposer).OrderBy(x => x.Value).ToArray();
            var accepters = expectedPairs.Select(x => x.Accepter).OrderBy(x => x.Value).Reverse().ToArray();

            GaleShapley.Match(proposers, accepters);

            Assert.IsTrue(proposers.All(x => x.EngagedTo != null));
            Assert.IsTrue(accepters.All(x => x.EngagedTo != null));

            foreach (var pair in expectedPairs)
            {
                Assert.AreEqual(pair.Proposer.EngagedTo, pair.Accepter);
                Assert.AreEqual(pair.Accepter.EngagedTo, pair.Proposer);
            }            
        }
    }
}
