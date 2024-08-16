using ShipRescue;

public class Graph
{
  public Dictionary<Ship, List<(Ship, double)>> AdjacencyList { get; private set; }

  public Graph()
  {
    AdjacencyList = new Dictionary<Ship, List<(Ship, double)>>();
  }

  public void AddEdge(Ship from, Ship to, double weight)
  {
    if (!AdjacencyList.ContainsKey(from))
    {
      AdjacencyList[from] = new List<(Ship, double)>();
    }
    AdjacencyList[from].Add((to, weight));
  }
}
