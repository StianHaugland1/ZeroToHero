using ShipRescue;
using ShipRescue.Helpers;
using ShipRescue.Models;

namespace ShipRescueTests;

public class ShipParserTests
{
    private const string Ships = """
                            AAA_Y: 0,-5,90
                            BCA_C: 10,20,95
                            SAC_F: 5,80,65
                            ARH_B: 100,45,60
                            XXX_S: 150,70,180
                            """;


    private readonly Dictionary<string, Ship> _shipsDict = new Dictionary<string, Ship>
    {
        { "AAA", new Ship { Id = "AAA", Position = new Position(0, -5), RadioRange = 90, Type = Ship.ShipType.Yacht } },
        {
            "BCA",
            new Ship
            {
                Id = "BCA", Position = new Position(10, 20), RadioRange = 95, Type = Ship.ShipType.ContainerShip
            }
        },
        {
            "SAC",
            new Ship { Id = "SAC", Position = new Position(5, 80), RadioRange = 65, Type = Ship.ShipType.FishingBoat }
        },
        {
            "ARH", new Ship { Id = "ARH", Position = new Position(100, 45), RadioRange = 60, Type = Ship.ShipType.Buoy }
        },
        {
            "XXX",
            new Ship { Id = "XXX", Position = new Position(150, 70), RadioRange = 180, Type = Ship.ShipType.Shore }
        }
    };

    [Fact]
    public void Validate_ShipCount_IsCorrect()
    {
        var shipList = ShipParser.Parse(Ships);

        Assert.Equal(5, shipList.Count);
    }

    [Fact]
    public void Validate_ShipIds_AreParsedCorrectly()
    {
        var shipList = ShipParser.Parse(Ships);

        foreach (var ship in shipList)
        {
            Assert.Equal(_shipsDict[ship.Id].Id, ship.Id);
        }
    }

    [Fact]
    public void Validate_ShipPositions_AreParsedCorrectly()
    {
        var shipList = ShipParser.Parse(Ships);

        foreach (var ship in shipList)
        {
            Assert.Equal(_shipsDict[ship.Id].Position.X, ship.Position.X);
            Assert.Equal(_shipsDict[ship.Id].Position.Y, ship.Position.Y);
        }
    }

    [Fact]
    public void Validate_ShipRadioRanges_AreParsedCorrectly()
    {
        var shipList = ShipParser.Parse(Ships);

        foreach (var ship in shipList)
        {
            Assert.Equal(_shipsDict[ship.Id].RadioRange, ship.RadioRange);
        }
    }

    [Fact]
    public void Validate_ShipTypes_AreParsedCorrectly()
    {
        var shipList = ShipParser.Parse(Ships);

        foreach (var ship in shipList)
        {
            Assert.Equal(_shipsDict[ship.Id].Type, ship.Type);
        }
    }
}