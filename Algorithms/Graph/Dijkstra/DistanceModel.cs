using DataStructures.Graph;

namespace Algorithms.Graph.Dijkstra
{
    public class DistanceModel<T>
    {
        public Vertex<T>? Vertex { get; set; }

        public Vertex<T>? PreviousVertex { get; set; }

        public int Distance { get; set; }

        public DistanceModel()
        {
        }

        public DistanceModel(Vertex<T> vertex, Vertex<T> previousVertex, int distance)
        {
            Vertex = vertex;
            PreviousVertex = previousVertex;
            Distance = distance;
        }

        public override string ToString() =>
            $"Vertex: {Vertex} - Distance: {Distance} - Previous: {PreviousVertex}";
    }
}
