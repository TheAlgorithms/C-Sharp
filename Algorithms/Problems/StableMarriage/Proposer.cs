using System;

namespace Algorithms.Problems.StableMarriage
{
    public class Proposer
    {
        public Proposer(int value) => Value = value;

        public Accepter? EngagedTo { get; set; }

        public int Value { get; }

        public double Score(Accepter accepter) => Math.Abs(accepter.Value - Value);
    }
}
