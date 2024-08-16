using ShipRescue.Models;

namespace ShipRescue;

public class Signal {
    public List<Ship> PathFromShipToShore { get; init; }
    public List<Ship> PathFromShoreToShip { get; init; }
    public double Distance { get; init; }

    public Signal(List<Ship> pathFromShipToShore, List<Ship> pathFromShoreToShip, double distance)
    {
        PathFromShipToShore = pathFromShipToShore;
        PathFromShoreToShip = pathFromShoreToShip;
        Distance = distance;
    }

    public override string ToString()
    {
      var pathToShore = string.Join(", ", PathFromShipToShore.Select(ship => ship.Id));
      var pathToShip = string.Join(", ", PathFromShoreToShip.Select(ship => ship.Id));
      return $"{pathToShore}\n{pathToShip}\n{Distance:F2}";
    }
}
