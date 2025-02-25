using ShipRescue.Enums;
using ShipRescue.Utilities;

namespace ShipRescue.Models;

public class Ship
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
        if (DistanceTo(Other) > RadioRange)
        {
            return false;
        }

        return CommunicationRules.CanContact(Type, Other.Type);
    }
}