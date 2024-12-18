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
    public FireResult Fire(Coord coord);
}

class Cell
{
    Coord coord;
    CellStatus status;
    IShip? ship;

    public Cell(Coord coord)
    {
        this.coord = coord;
        status = CellStatus.Hidden;
    }

    public Cell(Coord coord, IShip ship) : this(coord)
    {
        this.ship = ship;
    }

    public FireResult Fire()
    {
        if (status == CellStatus.Open) return FireResult.Duplicate;

        if (ship != null)
        {
            status = CellStatus.ShipHit;
            ship.HitShip(coord);
            if (ship.IsKilled()) return FireResult.Killed;
            return FireResult.Hit;
        }

        status = CellStatus.Open;
        return FireResult.Missed;
    }

    public string GetShipKilledMessage()
    {
        if (ship != null && ship.IsKilled())
        {
            return ship.GetDeathMessage();
        }

        throw new Exception(Localization.ShipNotKilledMessage);
    }

    public override string ToString()
    {
        if (status == CellStatus.ShipHit) return "X";
        if (status == CellStatus.Open) return "O";

#if DEBUG
        if (ship != null) return "S"; // remove in production
#endif

        return "H";
    }
}

