using System;
using System.Linq;

using NUnit.Framework;

namespace Algorithms.Problems.StableMarriage
{
    /// <summary>
    /// The stable marriage problem (also stable matching problem or SMP)
    /// is the problem of finding a stable matching between two equally sized sets of elements given an ordering of preferences for each element.    
    /// </summary>
    public static class GaleShapleyTests
    {
        /// <summary>
        /// Checks that all parties are engaged and stable.
        /// </summary>
        [Test]
        public static void MatchingIsSuccessful()
        {
            var random = new Random(7);
            var proposers = Enumerable.Range(1, 10).Select(x => new Proposer()).ToArray();
            var accepters = Enumerable.Range(1, 10).Select(x => new Accepter()).ToArray();

            foreach (var proposer in proposers)
            {
                proposer.PreferenceOrder = accepters.OrderBy(x => random.Next()).ToList();
            }
            foreach (var accepter in accepters)
            {
                accepter.PreferenceOrder = proposers.OrderBy(x => random.Next()).ToList();
            }

            GaleShapley.Match(proposers, accepters);

            Assert.IsTrue(accepters.All(x => x.EngagedTo != null));
            Assert.IsTrue(proposers.All(x => x.EngagedTo != null));
            Assert.IsTrue(AreMatchesStable(proposers, accepters));
        }

        private static bool AreMatchesStable(Proposer[] proposers, Accepter[] accepters) =>
            proposers.All(p => p.EngagedTo != null && p.Score(p.EngagedTo) <= accepters.Where(a => a.PrefersOverCurrent(p)).Min(a => p.Score(a)));
    }
}
