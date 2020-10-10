using System;
using System.Collections.Generic;

namespace Algorithms.Problems.StableMarriage
{
    public class Proposer
    {
        public Accepter? EngagedTo { get; set; }

        public List<Accepter> PreferenceOrder { get; set; } = new List<Accepter>();

        public int Score(Accepter accepter) => PreferenceOrder.IndexOf(accepter);
    }
}
