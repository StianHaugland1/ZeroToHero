using ShipRescue.Models;

namespace ShipRescue;

public class Ship : IShip
{
    public required string Id { get; init; }
    public required Position Position { get; init; }
    public double RadioRange { get; init; }
    public ShipType Type { get; init; }

    public double DistanceTo(Ship other)
    {
        return Position.DistanceTo(other.Position);
    }

    public bool CanContactShip(Ship Other)
    {
        if(DistanceTo(Other) > RadioRange)
        {
            return false;
        }
        
        var rules = Type switch
        {
            ShipType.Yacht => new HashSet<ShipType>{ ShipType.Yacht, ShipType.Buoy, ShipType.FishingBoat },
            ShipType.ContainerShip => new HashSet<ShipType>{ ShipType.Buoy, ShipType.FishingBoat },
            ShipType.FishingBoat => new HashSet<ShipType>{ ShipType.Yacht, ShipType.ContainerShip },
            ShipType.Buoy => new HashSet<ShipType>{ ShipType.ContainerShip, ShipType.FishingBoat, ShipType.Buoy, ShipType.Shore },
            ShipType.Shore => new HashSet<ShipType>{ ShipType.Buoy, ShipType.Yacht },
            _ => new HashSet<ShipType>{}
        };

        return rules.Contains(Other.Type);
    }

    public enum ShipType
    {
        Yacht, // Y
        ContainerShip, // C
        FishingBoat, // F
        Buoy, // B
        Shore // S
    }

}
