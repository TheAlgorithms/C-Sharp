using DataStructures.Graph;

namespace Algorithms.Graph.Dijkstra;

/// <summary>
/// Entity which represents the Dijkstra shortest distance.
/// Contains: Vertex, Previous Vertex and minimal distance from start vertex.
/// </summary>
/// <typeparam name="T">Generic parameter.</typeparam>
public class DistanceModel<T>
{
    public Vertex<T>? Vertex { get; }

    public Vertex<T>? PreviousVertex { get; set; }

    public Vertex<T>? StartVertex { get; set; }

    public double Distance { get; set; }

    public DistanceModel(Vertex<T>? vertex, Vertex<T>? previousVertex, double distance)
    {
        Vertex = vertex;
        PreviousVertex = previousVertex;
        Distance = distance;
    }

    public DistanceModel(Vertex<T>? vertex, Vertex<T>? previousVertex, Vertex<T>? startVertex, double distance)
    {
        Vertex = vertex;
        PreviousVertex = previousVertex;
        Distance = distance;
        StartVertex = startVertex;
    }

    public override string ToString() =>
        $"From Previous Vertex: {PreviousVertex} to Vertex {Vertex} is Distance: {Distance}";

    public string PrintDistance() =>
        $"Start from Vertex: {StartVertex} to Vertex {Vertex} is Distance: {Distance}";
}
