using System;
using System.Linq;

namespace Algorithms.Problems.StableMarriage
{
    public static class GaleShapley
    {
        /// <summary>
        /// Finds a stable matching between two equal sets of elements (fills EngagedTo properties).
        /// time complexity: O(n^2), where n - array size.
        /// Guarantees:
        /// - Everyone is matched
        /// - Matches are stable (there is no better accepter, for any given proposer, which would accept a new match).
        /// Presented and proven by David Gale and Lloyd Shapley in 1962.
        /// </summary>
        public static void Match(Proposer[] proposers, Accepter[] accepters)
        {
            if (proposers.Length != accepters.Length)
            {
                throw new ArgumentException("Collections must have equal count");
            }

            foreach (var proposer in proposers)
            {
                proposer.EngagedTo = null;
            }

            foreach (var accepter in accepters)
            {
                accepter.EngagedTo = null;
            }

            var matchingSession = proposers.Select(p => new { Proposer = p, Accepters = accepters.OrderBy(a => p.Score(a)).ToList() }).ToArray();

            while (matchingSession.Any(m => m.Proposer.EngagedTo == null))
            {
                foreach (var session in matchingSession.Where(m => m.Proposer.EngagedTo == null))
                {
                    var accepter = session.Accepters.First();
                    var newProposer = session.Proposer;

                    if (accepter.EngagedTo == null)
                    {
                        Engage(newProposer, accepter);
                    }
                    else
                    {
                        if (accepter.PrefersOverCurrent(newProposer))
                        {
                            Free(accepter.EngagedTo);
                            Engage(newProposer, accepter);
                        }
                    }

                    session.Accepters.Remove(accepter);
                }
            }
        }

        private static void Free(Proposer proposer)
        {
            proposer.EngagedTo = null;
        }

        private static void Engage(Proposer proposer, Accepter accepter)
        {
            proposer.EngagedTo = accepter;
            accepter.EngagedTo = proposer;
        }
    }
}
