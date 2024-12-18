using System.Text;

class Player
{
    List<IShip> _ships;
    BattleGround _battlefround;
    public string playerName { get; init; }
    public Player(string name = "UnknownPlayer")
    {
        _ships = BattleGroundUtils.GenerateShips();
        _battlefround = new BattleGround(_ships);
        playerName = name;
    }
    public IEnumerable<StringBuilder> DisplayBattleGround()
    {
        yield return new StringBuilder("  " + playerName);

        foreach (StringBuilder sb in _battlefround.DisplayBattleGround())
        {
            yield return sb;
        }
    }

    public (string, FireResult) FireCell(Coord coord)
    {
        return _battlefround.FireCell(coord);
    }

    public bool IsGameOver()
    {
        return _battlefround.ShipCnt == 0;
    }
}