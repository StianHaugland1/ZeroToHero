using ShipRescue.DataStructures;
using ShipRescue.Models;

public class GraphBuilder
{
    public static Graph<Ship> BuildGraph(List<Ship> ships)
    {
        var graph = new Graph<Ship>();

        for (int i = 0; i < ships.Count; i++)
        {
            for (int j = i + 1; j < ships.Count; j++)
            {
                var shipA = ships[i];
                var shipB = ships[j];
                AddEdgeIfContactPossible(graph, shipA, shipB);
            }
        }

        return graph;
    }

    private static void AddEdgeIfContactPossible(Graph<Ship> graph, Ship shipA, Ship shipB)
    {
        double distance = shipA.DistanceTo(shipB);

        if (shipA.CanContactShip(shipB))
        {
            graph.AddEdge(shipA, shipB, distance);
        }

        if (shipB.CanContactShip(shipA))
        {
            graph.AddEdge(shipB, shipA, distance);
        }
    }
}