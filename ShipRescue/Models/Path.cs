using ShipRescue.Models;

public class RadioPath
{
    public List<Ship> Path { get; init; }
    public double Distance { get; init; }

    public RadioPath(List<Ship> path, double distance)
    {
        Path = path;
        Distance = distance;
    }
}