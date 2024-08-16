using ShipRescue.Enums;
using ShipRescue.Models;

namespace ShipRescue.Utilities
{
    public static class ShipParser
    {
        public static ICollection<Ship> Parse(string shipsString)
        {
            var ships = new List<Ship>();
            var shipLines = shipsString.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            foreach (var shipLine in shipLines)
            {
                var ship = ParseShipLine(shipLine);
                ships.Add(ship);
            }

            return ships;
        }

        private static Ship ParseShipLine(string shipLine)
        {
            var shipParts = shipLine.Split(":", StringSplitOptions.RemoveEmptyEntries);
            var id = shipParts[0].Substring(0, 3);
            var type = ParseShipType(shipParts[0].Substring(4, 1));
            var position = ParsePosition(shipParts[1]);
            var radioRange = ParseRadioRange(shipParts[1]);

            return new Ship
            {
                Id = id,
                Position = position,
                RadioRange = radioRange,
                Type = type
            };
        }

        private static Position ParsePosition(string locationInfo)
        {
            var locationParts = locationInfo.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var x = double.Parse(locationParts[0]);
            var y = double.Parse(locationParts[1]);

            return new Position(x, y);
        }

        private static double ParseRadioRange(string locationInfo)
        {
            var locationParts = locationInfo.Split(",", StringSplitOptions.RemoveEmptyEntries);
            return double.Parse(locationParts[2]);
        }

        private static ShipType ParseShipType(string type)
        {
            return type switch
            {
                "Y" => ShipType.Yacht,
                "C" => ShipType.ContainerShip,
                "F" => ShipType.FishingBoat,
                "B" => ShipType.Buoy,
                "S" => ShipType.Shore,
                _ => throw new ArgumentException($"Invalid ship type: {type}")
            };
        }
    }
}