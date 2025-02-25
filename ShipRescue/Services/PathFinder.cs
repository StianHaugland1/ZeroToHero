using ShipRescue.DataStructures;
using ShipRescue.Models;
using ShipRescue.Utilities;

namespace ShipRescue.Services;

public class PathFinder
{
    private readonly Graph<Ship> _graph;

    public PathFinder(List<Ship> ships)
    {
        _graph = GraphBuilder.BuildGraph(ships);
    }

    public RadioPath? FindShortestPath(Ship startShip, Ship targetShip)
    {
        var distances = new Dictionary<Ship, double>();
        var previousShips = new Dictionary<Ship, Ship?>();
        var priorityQueue = new SortedSet<(double, Ship)>(new TupleComparer());

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

        return GetRadioPath(targetShip, previousShips, distances);
    }

    private RadioPath? GetRadioPath(Ship targetShip, Dictionary<Ship, Ship?> previousShips,
        Dictionary<Ship, double> distances)
    {
        var path = new List<Ship>();
        Ship? current = targetShip;
        while (current != null)
        {
            path.Add(current);
            if (!previousShips.ContainsKey(current))
            {
                return null;
            }

            current = previousShips[current];
        }

        path.Reverse();
        double totalDistance = distances[targetShip];
        return new RadioPath(path, totalDistance);
    }
}