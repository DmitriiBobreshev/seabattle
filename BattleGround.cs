using System.Text;

class BattleGround
{
    private Cell[,] _board = new Cell[10, 10];
    private int _shipCnt;
    public int ShipCnt => _shipCnt;
    public BattleGround(List<IShip> ships)
    {
        _shipCnt = ships.Count;
        FillBoard(ships);
    }

    private void FillBoard(List<IShip> ships)
    {
        int rows = _board.GetLength(0);
        int columns = _board.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                _board[j, i] = InitCell(ships, j, i);
            }
        }
    }

    private Cell InitCell(List<IShip> ships, int col, int row)
    {
        Coord coord = new Coord(col, row);
        IShip? ship = ships.Find(x => x.coords.Contains(coord));
        Cell cell;
        if (ship != null)
        {
            cell = new Cell(coord, ship);
        }
        else
        {
            cell = new Cell(coord);
        }

        return cell;
    }

    public (string, FireResult) FireCell(Coord coord)
    {
        try
        {
            Cell cell = _board[coord.y, coord.x];
            FireResult res = cell.Fire();

            switch (res)
            {
                case FireResult.Missed: return (Localization.Missed, FireResult.Missed);
                case FireResult.Hit: return (Localization.Hit, FireResult.Hit);
                case FireResult.Killed:
                    _shipCnt--;
                    return (cell.GetShipKilledMessage(), FireResult.Killed);
                default: return (Localization.Dupliacte, FireResult.Duplicate);
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new ArgumentOutOfRangeException(Localization.NotValidCell);
        }
    }


    public IEnumerable<StringBuilder> DisplayBattleGround()
    {
        StringBuilder sb = new StringBuilder();
        yield return DisplayRowHeader();

        int rows = _board.GetLength(0);
        int columns = _board.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            sb.Clear();
            sb.Append(i.ToString() + "|");
            for (int j = 0; j < columns; j++)
            {
                sb.Append(_board[j, i].ToString());
            }
            yield return sb;
        }
    }

    private StringBuilder DisplayRowHeader()
    {
        StringBuilder sb = new StringBuilder("  ");

        int rows = _board.GetLength(0);
        for (int i = 0; i < rows; i++)
        {
            char rowChar = BattleGroundUtils.IntToChar(i);
            sb.Append(rowChar);
        }
        return sb;
    }
}