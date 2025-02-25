using ShipRescue.Enums;
using ShipRescue.Models;
using ShipRescue.Utilities;

namespace ShipRescue.Services;

public class ShipRadio
{
    private readonly List<Ship> _ships;

    public ShipRadio(string shipsString)
    {
        _ships = ShipParser.Parse(shipsString).ToList();
    }

    private Dictionary<Ship, RadioPath> FindPathsToShores(PathFinder pathFinder, Ship startShip, List<Ship> shores)
    {
        var pathsToShore = new Dictionary<Ship, RadioPath>();
        foreach (var shore in shores)
        {
            var radioPath = pathFinder.FindShortestPath(startShip, shore);
            if (radioPath != null)
            {
                pathsToShore[shore] = radioPath;
            }
        }

        return pathsToShore;
    }

    private (List<Ship> shortestPathToShore, List<Ship> shortestPathFromShoreToShip, double shortestDistance)
        FindShortestPathToShoreAndBack(PathFinder pathFinder, Ship startShip, Dictionary<Ship, RadioPath> pathsToShore)
    {
        var shortestDistance = double.PositiveInfinity;
        var shortestPathToShore = new List<Ship>();
        var shortestPathFromShoreToShip = new List<Ship>();

        foreach (var path in pathsToShore)
        {
            var radioPathFromShoreToShip = pathFinder.FindShortestPath(path.Key, startShip);
            if (radioPathFromShoreToShip == null)
            {
                continue;
            }

            var totalDistance = path.Value.Distance + radioPathFromShoreToShip.Distance;
            if (totalDistance < shortestDistance)
            {
                shortestDistance = totalDistance;
                shortestPathToShore = path.Value.Path;
                shortestPathFromShoreToShip = radioPathFromShoreToShip.Path;
            }
        }

        return (shortestPathToShore, shortestPathFromShoreToShip, shortestDistance);
    }

    public Signal EstablishChannel()
    {
        var pathFinder = new PathFinder(_ships);
        var shores = _ships.Where(s => s.Type == ShipType.Shore).ToList();

        var pathsToShore = FindPathsToShores(pathFinder, _ships[0], shores);
        var (shortestPathToShore, shortestPathFromShoreToShip, shortestDistance) =
            FindShortestPathToShoreAndBack(pathFinder, _ships[0], pathsToShore);

        return new Signal(shortestPathToShore, shortestPathFromShoreToShip, shortestDistance);
    }
}