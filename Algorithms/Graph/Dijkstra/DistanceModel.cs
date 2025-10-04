using DataStructures.Graph;

namespace Algorithms.Graph.Dijkstra;

/// <summary>
/// Entity which represents the Dijkstra shortest distance.
/// Contains: Vertex, Previous Vertex and minimal distance from start vertex.
/// </summary>
/// <typeparam name="T">Generic parameter.</typeparam>
public class DistanceModel<T>(Vertex<T>? vertex, Vertex<T>? previousVertex, double distance)
{
    public Vertex<T>? Vertex { get; } = vertex;

    public Vertex<T>? PreviousVertex { get; set; } = previousVertex;

    public double Distance { get; set; } = distance;

    public override string ToString() =>
        $"Vertex: {Vertex} - Distance: {Distance} - Previous: {PreviousVertex}";
}
