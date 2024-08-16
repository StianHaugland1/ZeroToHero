namespace ShipRescue.DataStructures;

public class Graph<T> where T : notnull
{
    public Dictionary<T, List<(T, double)>> AdjacencyList { get; private set; }

    public Graph()
    {
        AdjacencyList = new Dictionary<T, List<(T, double)>>();
    }

    public void AddEdge(T from, T to, double weight)
    {
        if (!AdjacencyList.ContainsKey(from))
        {
            AdjacencyList[from] = new List<(T, double)>();
        }

        AdjacencyList[from].Add((to, weight));
    }
}