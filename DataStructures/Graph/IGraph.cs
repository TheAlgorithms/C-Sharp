using System.Collections.Generic;

namespace DataStructures.Graph
{
    public interface IGraph<T>
    {
        public List<Vertex<T>> Vertices { get; }

        public int Count { get; }

        Vertex<T> AddVertex(T data);

        void AddEdge(Vertex<T> startVertex, Vertex<T> endVertex, double weight);

        void RemoveVertex(Vertex<T> vertex);

        void RemoveEdge(Vertex<T> vertex1, Vertex<T> vertex2);

        IEnumerable<Vertex<T>> GetNeighbors(Vertex<T> vertex);

        bool AreAdjacent(Vertex<T> vertex1, Vertex<T> vertex2);
    }
}
