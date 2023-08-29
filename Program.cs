using System;

namespace snakev0
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            Menu menu = new Menu();
            menu.StartScreen();
            string[] elements = { "Start Game","Two players", "Score Board", "Quit" };
            menu.Setup(elements);
            while (!exit)
            {
                var selection = menu.Open();
                GameSingle gamesingle = new GameSingle();
                GameTwoPlayers gamefortwo = new GameTwoPlayers();
                ScoreBoard scoreboard = new ScoreBoard();
                switch (selection)
                {
                    case 0:
                        gamesingle.StartGame();
                        break;
                    case 1:
                        gamefortwo.StartGame();
                        break;
                    case 2:
                        scoreboard.PrintSB();
                        break;
                    case 3:
                        System.Environment.Exit(1);
                        break;
                }
                Console.Clear();
            }
            
            Console.ReadKey(true);
        }
    }
}
