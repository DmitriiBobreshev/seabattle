using System.Runtime.InteropServices;
using System.Text;

class BattleGround
{
  private const char delitimer = '|';
  public string playerName { get; init; }
  private Cell[,] board = new Cell [10, 10];

  private int shipCnt = 0;
  public BattleGround(List<IShip> ships, string player = "UnknownPlayer")
  {
    playerName = player;
    shipCnt = ships.Count;
    FillBoard(ships);
  }

  private void FillBoard(List<IShip> ships)
  {
    int rows = board.GetUpperBound(0) + 1;
    int columns = board.Length / rows;
    for (int i = 0; i < rows; i++)
    {
      for (int j = 0; j < columns; j++)
      {
        board[j, i] = InitCell(ships, j, i);
      }
    }
  }

  private Cell InitCell(List<IShip> ships, int col, int row)
  {
    Coord coord = new Coord(col, row);
    IShip? ship = ships.Find(x => x.coords.Contains(coord));
    Cell cell;
    if (ship != null) {
      cell = new Cell(coord, ship);
    }
    else 
    {
      cell = new Cell(coord);
    }

    return cell;
  }

  public bool IsGameOver()
  {
    return shipCnt == 0;
  }

  public (string, FireResult) FireCell(Coord coord)
  {
    try 
    {
      Cell cell = board[coord.y, coord.x];
      FireResult res = cell.Fire();

      switch (res)
      {
        case FireResult.Missed: return (Localization.Missed, FireResult.Missed);
        case FireResult.Hit: return (Localization.Hit, FireResult.Hit);
        case FireResult.Killed: 
          shipCnt--;
          return (cell.GetShipKilledMessage(), FireResult.Killed);
        default: return (Localization.Dupliacte, FireResult.Duplicate);
      }
    } 
    catch (ArgumentOutOfRangeException)
    {
      throw new ArgumentOutOfRangeException(Localization.NotValidCell);
    }
  }

  public string GetPlayerName()
  {
    return playerName;
  }

  public IEnumerable<StringBuilder> DisplayBattleGround()
  {
    StringBuilder sb = new StringBuilder("  " + playerName);
    yield return sb;

    yield return DisplayRowHeader();

    int rows = board.GetLength(0);
    int columns = board.GetLength(1);
    for (int i = 0; i < rows; i++)
    {
      sb.Clear();
      sb.Append(i.ToString() + delitimer);
      for (int j = 0; j < columns; j++)
      {
        sb.Append(board[j, i].ToString() + delitimer);
      }
      yield return sb;
    }
  }

  private StringBuilder DisplayRowHeader()
  {
    StringBuilder sb = new StringBuilder(" " + delitimer);

    int rows = board.GetUpperBound(0) + 1;
    for (int i = 0; i < rows; i++)
    {
      char rowChar = BattleGroundUtils.IntToChar(i);
      sb.Append(rowChar);
      sb.Append(delitimer);
    }

    return sb;
  }
}