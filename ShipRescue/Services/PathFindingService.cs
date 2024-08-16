using ShipRescue;

public class PathFindingService
{
  public Graph _graph { get; private set; }

  public PathFindingService(Graph graph)
  {
    _graph = graph;
  }

  public void BuildGraph(List<Ship> ships)
  {
    for (int i = 0; i < ships.Count; i++)
    {
      for (int j = i + 1; j < ships.Count; j++)
      {
        var shipA = ships[i];
        var shipB = ships[j];

        double distance = shipA.DistanceTo(shipB);

        if (shipA.CanContactShip(shipB)){
          _graph.AddEdge(shipA, shipB, distance);
        }
        if (shipB.CanContactShip(shipA)) {
          _graph.AddEdge(shipB, shipA, distance);
        }
      }
    }
  }

  public List<(Ship, double)>? GetAdjucencyList(Ship ship)
  {
    return _graph.AdjacencyList[ship];
  }

  public List<Ship> FindShortestPath(Ship startShip, Ship targetShip)
   {
            var distances = new Dictionary<Ship, double>();
            var previousShips = new Dictionary<Ship, Ship?>();
            var priorityQueue = new SortedSet<(double, Ship)>();

            foreach (var ship in _graph.AdjacencyList.Keys)
            {
                distances[ship] = double.PositiveInfinity;
                previousShips[ship] = null;
            }

            distances[startShip] = 0;
            priorityQueue.Add((0, startShip));

            while (priorityQueue.Count > 0)
            {
                var (currentDistance, currentShip) = priorityQueue.Min;
                priorityQueue.Remove(priorityQueue.Min);

                if (currentShip == targetShip)
                {
                    break;
                }

                foreach (var (neighbor, weight) in _graph.AdjacencyList[currentShip])
                {
                    double distance = currentDistance + weight;
                    if (distance < distances[neighbor])
                    {
                        priorityQueue.Remove((distances[neighbor], neighbor));
                        distances[neighbor] = distance;
                        previousShips[neighbor] = currentShip;
                        priorityQueue.Add((distance, neighbor));
                    }
                }
            }

            var path = new List<Ship>();
            Ship? current = targetShip;
            while (current != null)
            {
                path.Add(current);
                current = previousShips[current];
            }

            path.Reverse();
            return path;
        }
 
}