using ShipRescue;
using ShipRescue.Helpers;

public class ShipRadio
{
  public List<Ship> _ships { get; private set; }

  public ShipRadio(string shipsString)
  {
    _ships = ShipParser.Parse(shipsString).ToList();
  }

  public void EstablishChannel()
  {
    PathFindingService pathFindingService = new PathFindingService(new Graph());
    pathFindingService.BuildGraph(_ships);
    var l = pathFindingService.FindShortestPath(_ships[4], _ships[0]);
    // var m = pathFindingService.GetAdjucencyList(_ships[4]);
    Console.WriteLine("Channel established");
  }

}