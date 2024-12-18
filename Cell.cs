enum FireResult
{
    Missed,
    Hit,
    Killed,
    Duplicate
}

enum CellStatus
{
    Hidden,
    ShipHit,
    Open
}

interface ICell
{
    public FireResult Fire(Coordinate coord);
}

class Cell
{
    private readonly Coordinate _coord;
    private CellStatus _status;
    private IShip? _ship;

    public Cell(Coordinate coord)
    {
        _coord = coord;
        _status = CellStatus.Hidden;
    }

    public Cell(Coordinate coord, IShip ship) : this(coord)
    {
        _ship = ship;
    }

    public FireResult Fire()
    {
        if (_status == CellStatus.Open) return FireResult.Duplicate;

        if (_ship != null)
        {
            _status = CellStatus.ShipHit;
            _ship.HitShip(_coord);
            if (_ship.IsKilled()) return FireResult.Killed;
            return FireResult.Hit;
        }

        _status = CellStatus.Open;
        return FireResult.Missed;
    }

    public string GetShipKilledMessage()
    {
        if (_ship != null && _ship.IsKilled())
        {
            return _ship.GetDeathMessage();
        }

        throw new Exception(Localization.ShipNotKilledMessage);
    }

    public override string ToString()
    {
        if (_status == CellStatus.ShipHit) return "█";
        if (_status == CellStatus.Open) return "X";

#if DEBUG
        if (_ship != null) return "S"; // remove in production
#endif

        return "░";
    }
}

