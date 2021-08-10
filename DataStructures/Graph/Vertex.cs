namespace DataStructures.Graph
{
    public class Vertex<T>
    {
        public T Data { get; }

        public int Index { get; }

        public IGraph<T>? Graph { get; private set; }

        public Vertex(T data, int index)
        {
            Data = data;
            Index = index;
        }

        public void SetGraph(IGraph<T>? graph) => Graph = graph;

        public override string ToString() => $"Vertex Data: {Data}, Index: {Index}";
    }
}
