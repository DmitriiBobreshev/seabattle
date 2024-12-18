interface IShip
{
    public List<Coord> coords
    {
        get;
    }

    bool IsKilled();
    void HitShip(Coord coord);
    string GetDeathMessage();
}

enum ShipCapacity
{
    OneCellShip = 1,
    TwoCellShip = 2,
    ThreeCellShip = 3,
    FourCellShip = 4
}

class NotValidShip : IShip
{
    private List<Coord> _coords = new List<Coord>();

    public List<Coord> coords
    {
        get { return _coords; }
    }

    public bool IsKilled()
    {
        throw new NotImplementedException();
    }
    public void HitShip(Coord coord)
    {
        throw new NotImplementedException();
    }

    public string GetDeathMessage()
    {
        throw new NotImplementedException();
    }
}


class Ship : IShip
{
    private List<Coord> _coords;
    public List<Coord> coords
    {
        get
        {
            return _coords;
        }
    }

    List<Coord> floatedCoords = new List<Coord>();
    public Ship(List<Coord> coords)
    {
        _coords = coords;
    }

    public void HitShip(Coord coord)
    {
        if (_coords.Contains(coord))
        {
            floatedCoords.Add(coord);
        }
    }

    public bool IsKilled()
    {
        return _coords.Count == floatedCoords.Count;
    }

    public virtual string GetDeathMessage()
    {
        throw new NotImplementedException();
    }
}

class OneCellShip : Ship
{
    public OneCellShip(List<Coord> coords) : base(coords)
    {
        // TODO how to not call base constructor before validation?
        if (coords.Count != (int)ShipCapacity.OneCellShip) throw new ArgumentOutOfRangeException(Localization.OneCellShipOutOfRange);
    }

    public override string GetDeathMessage()
    {
        return Localization.OneCellShipKilled;
    }
}

class TwoCellShip : Ship
{
    public TwoCellShip(List<Coord> coords) : base(coords)
    {
        // TODO how to not call base constructor before validation?
        if (coords.Count != (int)ShipCapacity.TwoCellShip) throw new ArgumentOutOfRangeException(Localization.TwoCellShipOutOfRange);
    }

    public override string GetDeathMessage()
    {
        return Localization.TwoCellShipKilled;
    }
}

class ThreeCellShip : Ship
{
    public ThreeCellShip(List<Coord> coords) : base(coords)
    {
        // TODO how to not call base constructor before validation?
        if (coords.Count != (int)ShipCapacity.ThreeCellShip) throw new ArgumentOutOfRangeException(Localization.ThreeCellShipOutOfRange);
    }

    public override string GetDeathMessage()
    {
        return Localization.ThreeCellShipKilled;
    }
}

class FourCellShip : Ship
{
    public FourCellShip(List<Coord> coords) : base(coords)
    {
        // TODO how to not call base constructor before validation?
        if (coords.Count != (int)ShipCapacity.FourCellShip) throw new ArgumentOutOfRangeException(Localization.FourCellShipOutOfRange);
    }

    public override string GetDeathMessage()
    {
        return Localization.FourCellShipKilled;
    }
}