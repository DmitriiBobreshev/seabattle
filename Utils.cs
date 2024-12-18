using System.ComponentModel.DataAnnotations;

static class BattleGroundUtils
{
    public static char IntToChar(int v)
    {
        char s = (char)(v + 'A');
        return s;
    }

    public static int CharToInt(char c)
    {
        return c - 'A';
    }


    // Dummy algorythm to generate ships 
    public static List<IShip> GenerateShips()
    {
        List<IShip> ships = new List<IShip>();
        HashSet<Coord> busyCoords = new HashSet<Coord>();
        int[] shipsCount = new int[4] { 4, 3, 2, 1 };

        for (int i = shipsCount.Length - 1; i >= 0; i--)
        {
            int remainigShips = shipsCount[i];
            int shipSize = i + 1;
            while (remainigShips > 0)
            {
                IShip ship;
                if (TryGenShip(ref busyCoords, shipSize, out ship))
                {
                    ships.Add(ship);
                    remainigShips--;
                }
                else
                {
                    return GenerateShips();
                }
            }
        }

        return ships;
    }

    private static bool TryGenShip(ref HashSet<Coord> busyCoords, int shipSize, out IShip ship)
    {
        int remainingAttempts = 100;
        List<Coord> notValidCoords = new List<Coord>();
        ship = new NotValidShip();
        Random r = new Random();
        Coord c;

        do
        {
            int x = r.Next(0, 9);
            int y = r.Next(0, 9);
            c = new Coord(x, y);

            if (busyCoords.Contains(c))
            {
                notValidCoords.Add(c);
            }
            else if (TryToGenShipFromCell(busyCoords, shipSize, c, out ship))
            {
                foreach (Coord shipCoord in ship.coords)
                {
                    busyCoords.Add(shipCoord);

                    // add all coords nearby
                    List<Coord> roundCoords = GenRoundCoords(shipCoord);
                    foreach (Coord roundCoord in roundCoords)
                    {
                        busyCoords.Add(roundCoord);
                    }
                }
                return true;
            }

            remainingAttempts--;
        } while (notValidCoords.Contains(c) && remainingAttempts > 0);

        return false;
    }

    private static List<Coord> GenRoundCoords(Coord startingCoord)
    {
        List<Coord> coords = new List<Coord>();
        int x = startingCoord.x;
        int y = startingCoord.y;

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i < 0 || j < 0) continue;
                if (i == x && j == y) continue;
                coords.Add(new Coord(i, j));
            }
        }

        return coords;
    }

    private static bool TryToGenShipFromCell(HashSet<Coord> busyCoords, int shipSize, Coord startingCoord, out IShip ship)
    {
        ship = new NotValidShip();
        if (shipSize == 1)
        {
            ship = new OneCellShip(new List<Coord> { startingCoord });
            return true;
        }

        for (int i = 1; i < 5; i++)
        {
            List<Coord> possibleShip = new List<Coord>();

            for (int j = 1; j < shipSize + 1; j++)
            {
                int x = startingCoord.x;
                int y = startingCoord.y;

                if (i == 0) x = x - j; // max to top 
                if (i == 1) x = x + j; // max to bottom 
                if (i == 2) y = y - j; // max to left 
                if (i == 3) y = y + j; // max to right

                if (x < 0 || x > 9) break; // not valid
                if (y < 0 || y > 9) break; // not valid

                Coord coord = new Coord(x, y);
                if (busyCoords.Contains(coord) || coord == startingCoord) break;

                possibleShip.Add(new Coord(x, y));
            }

            if (possibleShip.Count == shipSize)
            {
                if (shipSize == 2) ship = new TwoCellShip(possibleShip);
                if (shipSize == 3) ship = new ThreeCellShip(possibleShip);
                if (shipSize == 4) ship = new FourCellShip(possibleShip);

                return true;
            }
        }

        return false;
    }
}