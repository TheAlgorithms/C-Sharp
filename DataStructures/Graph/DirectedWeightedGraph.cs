﻿using System;
using System.Collections.Generic;

namespace DataStructures.Graph
{
    /// <summary>
    ///     Implementation of the directed weighted graph via adjacency matrix.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public class DirectedWeightedGraph<T>
    {
        /// <summary>
        ///     Capacity of the graph, indicates the maximum amount of vertices.
        /// </summary>
        private readonly int capacity;

        /// <summary>
        ///     Adjacency matrix which reflects the edges between vertices and their weight.
        ///     Zero value indicates no edge between two vertices.
        /// </summary>
        private readonly double[,] adjacencyMatrix;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectedWeightedGraph{T}"/> class.
        /// </summary>
        /// <param name="capacity">Capacity of the graph, indicates the maximum amount of vertices.</param>
        public DirectedWeightedGraph(int capacity)
        {
            ThrowIfNegativeCapacity(capacity);

            this.capacity = capacity;
            Vertices = new Vertex<T>[capacity];
            adjacencyMatrix = new double[capacity, capacity];
            Count = 0;
        }

        /// <summary>
        ///     Gets list of vertices of the graph.
        /// </summary>
        public Vertex<T>?[] Vertices { get; private set; }

        /// <summary>
        ///     Gets current amount of vertices in the graph.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        ///     Adds new vertex to the graph.
        /// </summary>
        /// <param name="data">Data of the vertex.</param>
        /// <returns>Reference to created vertex.</returns>
        public Vertex<T> AddVertex(T data)
        {
            ThrowIfOverflow();
            var vertex = new Vertex<T>(data, Count, this);
            Vertices[Count] = vertex;
            Count++;
            return vertex;
        }

        /// <summary>
        ///     Creates an edge between two vertices of the graph.
        /// </summary>
        /// <param name="startVertex">Vertex, edge starts at.</param>
        /// <param name="endVertex">Vertex, edge ends at.</param>
        /// <param name="weight">Double weight of an edge.</param>
        public void AddEdge(Vertex<T> startVertex, Vertex<T> endVertex, double weight)
        {
            ThrowIfVertexNotInGraph(startVertex);
            ThrowIfVertexNotInGraph(endVertex);

            ThrowIfWeightZero(weight);

            var currentEdgeWeight = adjacencyMatrix[startVertex.Index, endVertex.Index];

            ThrowIfEdgeExists(currentEdgeWeight);

            adjacencyMatrix[startVertex.Index, endVertex.Index] = weight;
        }

        /// <summary>
        ///     Removes vertex from the graph.
        /// </summary>
        /// <param name="vertex">Vertex to be removed.</param>
        public void RemoveVertex(Vertex<T> vertex)
        {
            ThrowIfVertexNotInGraph(vertex);

            Vertices[vertex.Index] = null;
            vertex.SetGraphNull();

            for (var i = 0; i < Count; i++)
            {
                adjacencyMatrix[i, vertex.Index] = 0;
                adjacencyMatrix[vertex.Index, i] = 0;
            }

            Count--;
        }

        /// <summary>
        ///     Removes edge between two vertices.
        /// </summary>
        /// <param name="startVertex">Vertex, edge starts at.</param>
        /// <param name="endVertex">Vertex, edge ends at.</param>
        public void RemoveEdge(Vertex<T> startVertex, Vertex<T> endVertex)
        {
            ThrowIfVertexNotInGraph(startVertex);
            ThrowIfVertexNotInGraph(endVertex);
            adjacencyMatrix[startVertex.Index, endVertex.Index] = 0;
        }

        /// <summary>
        ///     Gets a neighbors of particular vertex.
        /// </summary>
        /// <param name="vertex">Vertex, method gets list of neighbors for.</param>
        /// <returns>Collection of the neighbors of particular vertex.</returns>
        public IEnumerable<Vertex<T>?> GetNeighbors(Vertex<T> vertex)
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

        /// <summary>
        ///     Returns true, if there is an edge between two vertices.
        /// </summary>
        /// <param name="startVertex">Vertex, edge starts at.</param>
        /// <param name="endVertex">Vertex, edge ends at.</param>
        /// <returns>True if edge exists, otherwise false.</returns>
        public bool AreAdjacent(Vertex<T> startVertex, Vertex<T> endVertex)
        {
            ThrowIfVertexNotInGraph(startVertex);
            ThrowIfVertexNotInGraph(endVertex);

            return adjacencyMatrix[startVertex.Index, endVertex.Index] != 0;
        }

        private static void ThrowIfNegativeCapacity(int capacity)
        {
            if (capacity < 0)
            {
                throw new InvalidOperationException("Graph capacity should always be a non-negative integer.");
            }
        }

        private static void ThrowIfWeightZero(double weight)
        {
            if (weight.Equals(0.0d))
            {
                throw new InvalidOperationException("Edge weight cannot be zero.");
            }
        }

        private static void ThrowIfEdgeExists(double currentEdgeWeight)
        {
            if (!currentEdgeWeight.Equals(0.0d))
            {
                throw new InvalidOperationException($"Vertex already exists: {currentEdgeWeight}");
            }
        }

        private void ThrowIfOverflow()
        {
            if (Count == capacity)
            {
                throw new InvalidOperationException("Graph overflow.");
            }
        }

        private void ThrowIfVertexNotInGraph(Vertex<T> vertex)
        {
            if (vertex.Graph != this)
            {
                throw new InvalidOperationException($"Vertex does not belong to graph: {vertex}.");
            }
        }
    }
}
