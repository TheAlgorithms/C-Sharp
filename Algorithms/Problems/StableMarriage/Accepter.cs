using System;
using System.Collections.Generic;

namespace Algorithms.Problems.StableMarriage
{
    public class Accepter
    {
        public Proposer? EngagedTo { get; set; }

        public List<Proposer> PreferenceOrder { get; set; } = new List<Proposer>();

        public bool PrefersOverCurrent(Proposer newProposer) => EngagedTo == null || PreferenceOrder.IndexOf(newProposer) < PreferenceOrder.IndexOf(EngagedTo);
    }
}
