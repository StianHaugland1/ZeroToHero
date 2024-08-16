using ShipRescue;
using ShipRescue.Helpers;

public class ShipRadio
{
  public List<Ship> _ships { get; private set; }

  public ShipRadio(string shipsString)
  {
    _ships = ShipParser.Parse(shipsString).ToList();
  }

  public Signal EstablishChannel()
  {
    PathFindingService pathFindingService = new PathFindingService(new Graph());
    pathFindingService.BuildGraph(_ships);
    var shores = _ships.Where(s => s.Type == Ship.ShipType.Shore).ToList();
    var pathsToShore = new Dictionary<Ship, List<Ship>>();
    foreach (var shore in shores)
    {
      var path = pathFindingService.FindShortestPath(_ships[0], shore);
      if (path != null)
      {
        pathsToShore[shore] = path;
      }
    }

    var shortestDistance = double.PositiveInfinity;
    var shortestPathToShore = new List<Ship>();
    var shortestPathFromShoreToShip = new List<Ship>();
    foreach (var path in pathsToShore)
    {
      var distance = CalculateDistance(path.Value);
      var pathFromShoreToShip = pathFindingService.FindShortestPath(path.Key, _ships[0]);
      if(pathFromShoreToShip == null){
        continue;
      }
      var totalDistance = distance + CalculateDistance(pathFromShoreToShip);
      if (totalDistance < shortestDistance){
        shortestDistance = totalDistance;
        shortestPathToShore = path.Value;
        shortestPathFromShoreToShip = pathFromShoreToShip!;
      }
    }
    return new Signal(shortestPathToShore, shortestPathFromShoreToShip, shortestDistance);
  }

  private double CalculateDistance(List<Ship> path)
  {
    double distance = 0;
    for (int i = 0; i < path.Count - 1; i++)
    {
      distance += path[i].DistanceTo(path[i + 1]);
    }
    return distance;
  }

}