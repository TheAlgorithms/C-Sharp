using System.Collections.Generic;

namespace Algorithms.Problems.StableMarriage;

public class Proposer
{
    public Accepter? EngagedTo { get; set; }

    public LinkedList<Accepter> PreferenceOrder { get; set; } = new();
}
