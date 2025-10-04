
using System.Reflection;

using Algorithms.Search.AStar;

namespace Algorithms.Tests.Search;

public static class AStarTests
{
    [Test]
    public static void ResetNodes_ResetsAllNodeProperties()
    {
        var node = new Node(new VecN(0, 0), true, 1.0)
        {
            CurrentCost = 5,
            EstimatedCost = 10,
            Parent = new Node(new VecN(1, 1), true, 1.0),
            State = NodeState.Closed
        };
        var nodes = new List<Node> { node };

        AStar.ResetNodes(nodes);

        node.CurrentCost.Should().Be(0);
        node.EstimatedCost.Should().Be(0);
        node.Parent.Should().BeNull();
        node.State.Should().Be(NodeState.Unconsidered);
    }

    [Test]
    public static void GeneratePath_ReturnsPathFromTargetToRoot()
    {
        var start = new Node(new VecN(0, 0), true, 1.0);
        var mid = new Node(new VecN(1, 0), true, 1.0) { Parent = start };
        var end = new Node(new VecN(2, 0), true, 1.0) { Parent = mid };

        var path = AStar.GeneratePath(end);

        path.Should().HaveCount(3);
        path[0].Should().BeSameAs(start);
        path[1].Should().BeSameAs(mid);
        path[2].Should().BeSameAs(end);
    }

    [Test]
    public static void Compute_ReturnsEmptyList_WhenNoPathExists()
    {
        var start = new Node(new VecN(0, 0), true, 1.0);
        var end = new Node(new VecN(1, 0), true, 1.0);
        start.ConnectedNodes = [];
        end.ConnectedNodes = [];

        var path = AStar.Compute(start, end);

        path.Should().BeEmpty();
    }

    [Test]
    public static void Compute_ReturnsPath_WhenPathExists()
    {
        var start = new Node(new VecN(0, 0), true, 1.0);
        var mid = new Node(new VecN(1, 0), true, 1.0);
        var end = new Node(new VecN(2, 0), true, 1.0);

        start.ConnectedNodes = [mid];
        mid.ConnectedNodes = [end];
        end.ConnectedNodes = [];

        var path = AStar.Compute(start, end);

        path.Should().NotBeEmpty();
        path[0].Should().Be(start);
        path[^1].Should().Be(end);
    }

    [Test]
    public static void VecN_Equality_WorksAsExpected()
    {
        var a = new VecN(1, 2);
        var b = new VecN(1, 2);
        var c = new VecN(2, 1);

        a.Equals(b).Should().BeTrue();
        a.Equals(c).Should().BeFalse();
    }

    [Test]
    public static void AddOrUpdateConnected_ThrowsPathfindingException_OnSelfReference()
    {
        var node = new Node(new VecN(0, 0), true, 1.0);
        node.ConnectedNodes = [node];
        node.State = NodeState.Open;

        var queue = new PriorityQueue<Node>();

        Action act = () => {
            // Directly call the private method using reflection, otherwise we can't test this case
            var method = typeof(AStar).GetMethod("AddOrUpdateConnected", BindingFlags.NonPublic | BindingFlags.Static);
            method!.Invoke(null, [node, node, queue]);
        };

        act.Should().Throw<TargetInvocationException>()
            .WithInnerException<PathfindingException>()
            .WithMessage("*same node twice*");
    }
}
