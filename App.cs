using System.Text;

class App
{
    Player player1;
    Player player2;

    bool isPlayer1Turn = true;

    public App()
    {
        player1 = new Player("Player1");
        player2 = new Player("Player2");
    }

    public void Start()
    {
        string? line;

        while (true)
        {
            try
            {
                Player player = isPlayer1Turn ? player1 : player2;
                Player oponent = isPlayer1Turn ? player2 : player1;
                Console.Clear();
                ShowBattleground();

                Console.WriteLine(Localization.MainInfo);
                Console.Write(player.playerName + ": ");

                line = Console.ReadLine();
                if (line == String.Empty || line == null) return;

                Coordinate coord = GetCoordsFromLine(line);

                (string Text, FireResult Result) t = oponent.FireCell(coord);
                Console.WriteLine(t.Text);

                if (t.Result == FireResult.Missed || t.Result == FireResult.Duplicate)
                {
                    isPlayer1Turn = !isPlayer1Turn;
                }

                if (oponent.IsGameOver())
                {
                    Console.WriteLine(Localization.GameOver + player.playerName);
                    return;
                }

                Console.WriteLine(Localization.PressAnyKey);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(Localization.PressAnyKey);
                Console.ReadLine();
            }
        }
    }

    private Coordinate GetCoordsFromLine(string line)
    {
        if (line.Length != 2 && line.Length != 3)
        {
            throw new ArgumentOutOfRangeException(Localization.ReadLineException);
        }

        char letter = line[0];
        if (letter < 'A' || letter > 'J')
        {
            throw new ArgumentOutOfRangeException(Localization.NotValidCell);
        }

        int y;
        if (!int.TryParse(line.Substring(1), out y) || y < 1 || y > 10)
        {
            throw new ArgumentOutOfRangeException(Localization.NotValidCell);
        }

        return new Coordinate(letter, y - 1);
    }

    private void ShowBattleground()
    {
        int y = 0;
        int p2x = 50;
        Console.SetCursorPosition(0, y);
        foreach (StringBuilder sb in player1.DisplayBattleGround())
        {
            Console.SetCursorPosition(0, y);
            Console.WriteLine(sb);
            y += 1;
        }

        y = 0;
        Console.SetCursorPosition(p2x, y);
        foreach (StringBuilder sb in player2.DisplayBattleGround())
        {
            Console.SetCursorPosition(p2x, y);
            Console.WriteLine(sb);
            y += 1;
        }
    }
}