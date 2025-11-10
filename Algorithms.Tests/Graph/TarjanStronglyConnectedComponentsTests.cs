using Algorithms.Graph;
using NUnit.Framework;
using FluentAssertions;
using System.Linq;

namespace Algorithms.Tests.Graph;

public class TarjanStronglyConnectedComponentsTests
{
    [Test]
    public void FindSCCs_SimpleGraph_ReturnsCorrectSCCs()
    {
        var tarjan = new TarjanStronglyConnectedComponents(3);
        tarjan.AddEdge(0, 1);
        tarjan.AddEdge(1, 2);
        tarjan.AddEdge(2, 0);

        var sccs = tarjan.FindSCCs();

        sccs.Should().HaveCount(1);
        sccs[0].Should().BeEquivalentTo(new[] { 0, 1, 2 });
    }

    [Test]
    public void FindSCCs_TwoSeparateSCCs_ReturnsBothSCCs()
    {
        var tarjan = new TarjanStronglyConnectedComponents(4);
        tarjan.AddEdge(0, 1);
        tarjan.AddEdge(1, 0);
        tarjan.AddEdge(2, 3);
        tarjan.AddEdge(3, 2);

        var sccs = tarjan.FindSCCs();

        sccs.Should().HaveCount(2);
    }

    [Test]
    public void FindSCCs_DisconnectedVertices_EachVertexIsSCC()
    {
        var tarjan = new TarjanStronglyConnectedComponents(3);

        var sccs = tarjan.FindSCCs();

        sccs.Should().HaveCount(3);
        sccs.Should().OnlyContain(scc => scc.Count == 1);
    }

    [Test]
    public void FindSCCs_ComplexGraph_ReturnsCorrectSCCs()
    {
        var tarjan = new TarjanStronglyConnectedComponents(8);
        tarjan.AddEdge(0, 1);
        tarjan.AddEdge(1, 2);
        tarjan.AddEdge(2, 0);
        tarjan.AddEdge(2, 3);
        tarjan.AddEdge(3, 4);
        tarjan.AddEdge(4, 5);
        tarjan.AddEdge(5, 3);
        tarjan.AddEdge(5, 6);
        tarjan.AddEdge(6, 7);
        tarjan.AddEdge(7, 6);

        var sccs = tarjan.FindSCCs();

        sccs.Should().HaveCount(3);
    }

    [Test]
    public void GetSccCount_AfterFindingSCCs_ReturnsCorrectCount()
    {
        var tarjan = new TarjanStronglyConnectedComponents(5);
        tarjan.AddEdge(0, 1);
        tarjan.AddEdge(1, 0);
        tarjan.AddEdge(2, 3);
        tarjan.AddEdge(3, 4);
        tarjan.AddEdge(4, 2);

        tarjan.FindSCCs();
        var count = tarjan.GetSccCount();

        count.Should().Be(2);
    }

    [Test]
    public void InSameScc_VerticesInSameScc_ReturnsTrue()
    {
        var tarjan = new TarjanStronglyConnectedComponents(3);
        tarjan.AddEdge(0, 1);
        tarjan.AddEdge(1, 2);
        tarjan.AddEdge(2, 0);

        var result = tarjan.InSameScc(0, 2);

        result.Should().BeTrue();
    }

    [Test]
    public void InSameScc_VerticesInDifferentSccs_ReturnsFalse()
    {
        var tarjan = new TarjanStronglyConnectedComponents(4);
        tarjan.AddEdge(0, 1);
        tarjan.AddEdge(1, 0);
        tarjan.AddEdge(2, 3);

        var result = tarjan.InSameScc(0, 2);

        result.Should().BeFalse();
    }

    [Test]
    public void GetScc_ValidVertex_ReturnsSccContainingVertex()
    {
        var tarjan = new TarjanStronglyConnectedComponents(3);
        tarjan.AddEdge(0, 1);
        tarjan.AddEdge(1, 2);
        tarjan.AddEdge(2, 0);

        var scc = tarjan.GetScc(1);

        scc.Should().NotBeNull();
        scc.Should().Contain(1);
        scc.Should().HaveCount(3);
    }

    [Test]
    public void BuildCondensationGraph_ComplexGraph_ReturnsDAG()
    {
        var tarjan = new TarjanStronglyConnectedComponents(6);
        tarjan.AddEdge(0, 1);
        tarjan.AddEdge(1, 0);
        tarjan.AddEdge(1, 2);
        tarjan.AddEdge(2, 3);
        tarjan.AddEdge(3, 4);
        tarjan.AddEdge(4, 5);
        tarjan.AddEdge(5, 3);

        var condensation = tarjan.BuildCondensationGraph();

        condensation.Should().NotBeNull();
        condensation.Length.Should().Be(3); // {0,1}, {2}, {3,4,5}
    }

    [Test]
    public void AddEdge_InvalidVertex_ThrowsException()
    {
        var tarjan = new TarjanStronglyConnectedComponents(3);

        var act = () => tarjan.AddEdge(0, 5);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void FindSCCs_SingleVertex_ReturnsSingleSCC()
    {
        var tarjan = new TarjanStronglyConnectedComponents(1);

        var sccs = tarjan.FindSCCs();

        sccs.Should().HaveCount(1);
        sccs[0].Should().BeEquivalentTo(new[] { 0 });
    }

    [Test]
    public void FindSCCs_LinearChain_EachVertexIsSCC()
    {
        var tarjan = new TarjanStronglyConnectedComponents(4);
        tarjan.AddEdge(0, 1);
        tarjan.AddEdge(1, 2);
        tarjan.AddEdge(2, 3);

        var sccs = tarjan.FindSCCs();

        sccs.Should().HaveCount(4);
    }

    [Test]
    public void FindSCCs_SelfLoop_VertexIsSCC()
    {
        var tarjan = new TarjanStronglyConnectedComponents(2);
        tarjan.AddEdge(0, 0);
        tarjan.AddEdge(0, 1);

        var sccs = tarjan.FindSCCs();

        sccs.Should().Contain(scc => scc.Contains(0) && scc.Count == 1);
    }
}
