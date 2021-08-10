using System;
using System.Collections.Generic;

namespace DataStructures.Graph
{
    public class DirectedWeightedGraph<T> : IGraph<T>
    {
        private const int DefaultCapacity = 10;
        private readonly int capacity;
        private readonly double[,] adjacencyMatrix;

        public DirectedWeightedGraph(int capacity)
        {
            this.capacity = capacity;
            Vertices = new List<Vertex<T>>();
            adjacencyMatrix = new double[capacity, capacity];
            Count = 0;
        }

        public DirectedWeightedGraph()
        {
            capacity = DefaultCapacity;
            Vertices = new List<Vertex<T>>();
            adjacencyMatrix = new double[capacity, capacity];
            Count = 0;
        }

        public List<Vertex<T>> Vertices { get; }

        public int Count { get; private set; }

        public Vertex<T> AddVertex(T data)
        {
            ThrowIfOverflow();
            var vertex = new Vertex<T>(data, Count);
            Vertices.Add(vertex);
            vertex.SetGraph(this);
            Count++;
            return vertex;
        }

        public void AddEdge(Vertex<T> startVertex, Vertex<T> endVertex, double weight)
        {
            ThrowIfVertexNotInGraph(startVertex);
            ThrowIfVertexNotInGraph(endVertex);

            ThrowIfWeightZero(weight);

            var currentEdgeWeight = adjacencyMatrix[startVertex.Index, endVertex.Index];

            ThrowIfEdgeExists(currentEdgeWeight);

            adjacencyMatrix[startVertex.Index, endVertex.Index] = weight;
        }

        public void RemoveVertex(Vertex<T> vertex)
        {
            ThrowIfVertexNotInGraph(vertex);

            ThrowIfGraphIsEmpty();

            Vertices.Remove(vertex);
            vertex.SetGraph(null);

            for (var i = 0; i < Count; i++)
            {
                adjacencyMatrix[i, vertex.Index] = 0;
                adjacencyMatrix[vertex.Index, i] = 0;
            }

            Count--;
        }

        public void RemoveEdge(Vertex<T> vertex1, Vertex<T> vertex2)
        {
            ThrowIfVertexNotInGraph(vertex1);
            ThrowIfVertexNotInGraph(vertex2);
            adjacencyMatrix[vertex1.Index, vertex2.Index] = 0;
        }

        public IEnumerable<Vertex<T>> GetNeighbors(Vertex<T> vertex)
        {
            ThrowIfVertexNotInGraph(vertex);

            for (var i = 0; i < Count; i++)
            {
                if (adjacencyMatrix[vertex.Index, i] != 0)
                {
                    yield return Vertices[i];
                }
            }
        }

        public bool AreAdjacent(Vertex<T> vertex1, Vertex<T> vertex2)
        {
            ThrowIfVertexNotInGraph(vertex1);
            ThrowIfVertexNotInGraph(vertex2);

            return adjacencyMatrix[vertex1.Index, vertex2.Index] != 0;
        }

        private static void ThrowIfWeightZero(double weight)
        {
            if (weight == 0)
            {
                throw new InvalidOperationException("Edge weight cannot be zero.");
            }
        }

        private static void ThrowIfEdgeExists(double currentEdgeWeight)
        {
            if (currentEdgeWeight != 0)
            {
                throw new InvalidOperationException($"Vertex already exists: {currentEdgeWeight}");
            }
        }

        private void ThrowIfOverflow()
        {
            if (Count == capacity)
            {
                throw new OverflowException("Graph overflow.");
            }
        }

        private void ThrowIfVertexNotInGraph(Vertex<T> vertex)
        {
            if (vertex.Graph != this)
            {
                throw new InvalidOperationException($"Vertex does not belong to graph: {vertex}.");
            }
        }

        private void ThrowIfGraphIsEmpty()
        {
            if (Count == 0)
            {
                throw new IndexOutOfRangeException("Graph is empty.");
            }
        }
    }
}
