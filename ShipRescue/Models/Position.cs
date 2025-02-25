namespace ShipRescue.Models;

public class Position
{
    public double X { get; set; }
    public double Y { get; set; }

    public Position(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double DistanceTo(Position other)
    {
        return Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2));
    }
}