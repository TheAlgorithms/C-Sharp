namespace DataStructures.Graph
{
    public class Vertex<T>
    {
        public T Data { get; }

        public int Index { get; }

        public DirectedWeightedGraph<T>? Graph { get; private set; }

        public Vertex(T data, int index)
        {
            Data = data;
            Index = index;
        }

        public void SetGraph(DirectedWeightedGraph<T>? graph) => Graph = graph;

        public override string ToString() => $"Vertex Data: {Data}, Index: {Index}";
    }
}
