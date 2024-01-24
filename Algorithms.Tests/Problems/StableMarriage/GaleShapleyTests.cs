using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Problems.StableMarriage;
using NUnit.Framework;

namespace Algorithms.Tests.Problems.StableMarriage;

/// <summary>
///     The stable marriage problem (also stable matching problem or SMP)
///     is the problem of finding a stable matching between two equally sized sets of elements given an ordering of
///     preferences for each element.
/// </summary>
public static class GaleShapleyTests
{
    /// <summary>
    ///     Checks that all parties are engaged and stable.
    /// </summary>
    [Test]
    public static void MatchingIsSuccessful()
    {
        var random = new Random(7);
        var proposers = Enumerable.Range(1, 10).Select(_ => new Proposer()).ToArray();
        var acceptors = Enumerable.Range(1, 10).Select(_ => new Accepter()).ToArray();

        foreach (var proposer in proposers)
        {
            proposer.PreferenceOrder = new LinkedList<Accepter>(acceptors.OrderBy(_ => random.Next()));
        }

        foreach (var acceptor in acceptors)
        {
            acceptor.PreferenceOrder = proposers.OrderBy(_ => random.Next()).ToList();
        }

        GaleShapley.Match(proposers, acceptors);

        Assert.That(acceptors.ToList().TrueForAll(x => x.EngagedTo is not null));
        Assert.That(proposers.ToList().TrueForAll(x => x.EngagedTo is not null));
        Assert.That(AreMatchesStable(proposers, acceptors), Is.True);
    }

    private static bool AreMatchesStable(Proposer[] proposers, Accepter[] accepters) =>
        proposers.ToList().TrueForAll(p =>
            p.EngagedTo is not null
            && Score(p, p.EngagedTo) <= accepters
                .Where(a => a.PrefersOverCurrent(p))
                .Min(a => Score(p, a)));

    private static int Score(Proposer proposer, Accepter accepter) =>
        proposer.PreferenceOrder.ToList().IndexOf(accepter);
}
