namespace DataStructures.Graph;

/// <summary>
///     Implementation of graph vertex.
/// </summary>
/// <typeparam name="T">Generic Type.</typeparam>
public class Vertex<T>
{
    /// <summary>
    ///     Gets vertex data.
    /// </summary>
    public T Data { get; }

    /// <summary>
    ///     Gets an index of the vertex in graph adjacency matrix.
    /// </summary>
    public int Index { get; internal set; }

    /// <summary>
    ///     Gets reference to the graph this vertex belongs to.
    /// </summary>
    public DirectedWeightedGraph<T>? Graph { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vertex{T}"/> class.
    /// </summary>
    /// <param name="data">Vertex data. Generic type.</param>
    /// <param name="index">Index of the vertex in graph adjacency matrix.</param>
    /// <param name="graph">Graph this vertex belongs to.</param>
    public Vertex(T data, int index, DirectedWeightedGraph<T>? graph)
    {
        Data = data;
        Index = index;
        Graph = graph;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vertex{T}"/> class.
    /// </summary>
    /// <param name="data">Vertex data. Generic type.</param>
    /// <param name="index">Index of the vertex in graph adjacency matrix.</param>
    public Vertex(T data, int index)
    {
        Data = data;
        Index = index;
    }

    /// <summary>
    ///     Sets graph reference to the null. This method called when vertex removed from the graph.
    /// </summary>
    public void SetGraphNull() => Graph = null;

    /// <summary>
    ///     Override of base ToString method. Prints vertex data and index in graph adjacency matrix.
    /// </summary>
    /// <returns>String which contains vertex data and index in graph adjacency matrix..</returns>
    public override string ToString() => $"Vertex Data: {Data}, Index: {Index}";
}
