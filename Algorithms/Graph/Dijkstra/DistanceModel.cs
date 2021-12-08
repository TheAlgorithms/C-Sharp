using DataStructures.Graph;

namespace Algorithms.Graph.Dijkstra
{
    public class DistanceModel<T>
    {
        public Vertex<T>? Vertex { get; }

        public Vertex<T>? PreviousVertex { get; set; }

        public double Distance { get; set; }

        public DistanceModel(Vertex<T>? vertex, Vertex<T>? previousVertex, double distance)
        {
            Vertex = vertex;
            PreviousVertex = previousVertex;
            Distance = distance;
        }

        public override string ToString() =>
            $"Vertex: {Vertex} - Distance: {Distance} - Previous: {PreviousVertex}";
    }
}
