using ShipRescue.Enums;

namespace ShipRescue.Utilities
{
    public static class CommunicationRules
    {
        private static readonly IReadOnlyDictionary<ShipType, ISet<ShipType>> Rules =
            new Dictionary<ShipType, ISet<ShipType>>
            {
                { ShipType.Yacht, new HashSet<ShipType> { ShipType.Yacht, ShipType.Buoy, ShipType.FishingBoat } },
                { ShipType.ContainerShip, new HashSet<ShipType> { ShipType.Buoy, ShipType.FishingBoat } },
                { ShipType.FishingBoat, new HashSet<ShipType> { ShipType.Yacht, ShipType.ContainerShip } },
                {
                    ShipType.Buoy,
                    new HashSet<ShipType>
                        { ShipType.ContainerShip, ShipType.FishingBoat, ShipType.Buoy, ShipType.Shore }
                },
                { ShipType.Shore, new HashSet<ShipType> { ShipType.Buoy, ShipType.Yacht } }
            };

        public static bool CanContact(ShipType from, ShipType to)
        {
            return Rules.TryGetValue(from, out var contacts) && contacts.Contains(to);
        }
    }
}