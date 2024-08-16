using ShipRescue.Models;

namespace ShipRescue.Utilities;

public class TupleComparer : IComparer<(double, Ship)>
{
    public int Compare((double, Ship) x, (double, Ship) y)
    {
        int result = x.Item1.CompareTo(y.Item1);
        return result != 0 ? result : x.Item2.Id.CompareTo(y.Item2.Id);
    }
}