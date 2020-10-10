using System;

namespace Algorithms.Problems.StableMarriage
{
    public class Accepter
    {
        public Accepter(int value) => Value = value;

        public Proposer? EngagedTo { get; set; }

        public int Value { get; }

        public bool PrefersOverCurrent(Proposer newProposer) => EngagedTo == null || Math.Abs(newProposer.Value - Value) < Math.Abs(EngagedTo.Value - Value);
    }
}
