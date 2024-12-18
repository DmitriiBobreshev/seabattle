interface IShip
{
    public List<Coordinate> Coords
    {
        get;
    }

    bool IsKilled();
    void HitShip(Coordinate coord);
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
    private List<Coordinate> _coords = new List<Coordinate>();

    public List<Coordinate> Coords
    {
        get { return _coords; }
    }

    public bool IsKilled()
    {
        throw new NotImplementedException();
    }
    public void HitShip(Coordinate coord)
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
    private List<Coordinate> _coords;
    public List<Coordinate> Coords
    {
        get
        {
            return _coords;
        }
    }

    List<Coordinate> floatedCoords = new List<Coordinate>();
    public Ship(List<Coordinate> coords)
    {
        _coords = coords;
    }

    public void HitShip(Coordinate coord)
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
    public OneCellShip(List<Coordinate> coords) : base(coords)
    {
        if (coords.Count != (int)ShipCapacity.OneCellShip)
        {
            throw new ArgumentOutOfRangeException(Localization.OneCellShipOutOfRange);
        }
    }

    public override string GetDeathMessage()
    {
        return Localization.OneCellShipKilled;
    }
}

class TwoCellShip : Ship
{
    public TwoCellShip(List<Coordinate> coords) : base(coords)
    {
        if (coords.Count != (int)ShipCapacity.TwoCellShip)
        {
            throw new ArgumentOutOfRangeException(Localization.TwoCellShipOutOfRange);
        }
    }

    public override string GetDeathMessage()
    {
        return Localization.TwoCellShipKilled;
    }
}

class ThreeCellShip : Ship
{
    public ThreeCellShip(List<Coordinate> coords) : base(coords)
    {
        if (coords.Count != (int)ShipCapacity.ThreeCellShip)
        {
            throw new ArgumentOutOfRangeException(Localization.ThreeCellShipOutOfRange);
        }
    }

    public override string GetDeathMessage()
    {
        return Localization.ThreeCellShipKilled;
    }
}

class FourCellShip : Ship
{
    public FourCellShip(List<Coordinate> coords) : base(coords)
    {
        if (coords.Count != (int)ShipCapacity.FourCellShip)
        {
            throw new ArgumentOutOfRangeException(Localization.FourCellShipOutOfRange);
        }
    }

    public override string GetDeathMessage()
    {
        return Localization.FourCellShipKilled;
    }
}