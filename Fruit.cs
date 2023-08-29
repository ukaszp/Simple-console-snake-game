using System;

namespace snakev0
{
    class Fruit
    {
        public int x, y;
        private int arenaWidth, arenaHeight;
        public Fruit(int arenaWidth, int arenaHeight)
        {
            this.arenaWidth = arenaWidth;
            this.arenaHeight = arenaHeight;
        }

        public void DrawFruit(Snake snake, Snake secondsnake)
        {
            bool collision;
            do
            {
                collision = false;
                Random rnd = new Random();
                x = rnd.Next((Console.WindowWidth / 2 - arenaWidth / 2) + 2, (Console.WindowWidth / 2 + arenaWidth / 2) - 2);
                y = rnd.Next(2, arenaHeight - 2);
                if (snake.segments.Count > 0)
                {
                    foreach (SnakeSegments seg in snake.segments)
                    {
                        if ((x == seg.x || x == seg.x + 1) && y == seg.y)
                        {
                            collision = true;
                        }
                    }
                    foreach (SnakeSegments seg in secondsnake.segments)
                    {
                        if ((x == seg.x || x == seg.x + 1) && y == seg.y)
                        {
                            collision = true;
                        }
                    }
                }
                else if ((x == snake.Headx || x == snake.Headx + 1) && y == snake.Heady)
                {
                    collision = true;
                }
                else if ((x == secondsnake.Headx || x == secondsnake.Headx + 1) && y == secondsnake.Heady)
                {
                    collision = true;
                }
            }
            while (collision == true);
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("o");
            Console.ResetColor();
        }
        public void DrawFruit(Snake snake)
        {
            bool collision;
            do
            {
                collision = false;
                Random rnd = new Random();
                x = rnd.Next((Console.WindowWidth / 2 - arenaWidth / 2) + 2, (Console.WindowWidth / 2 + arenaWidth / 2) - 2);
                y = rnd.Next(2, arenaHeight - 2);
                if (snake.segments.Count > 0)
                {
                    foreach (SnakeSegments seg in snake.segments)
                    {
                        if (((x == seg.x || x == seg.x + 1) && y == seg.y))
                        {
                            collision = true;
                        }
                    }
                }
                else if((x == snake.Headx || x == snake.Headx + 1) && y == snake.Heady)
                {
                    collision = true;
                }
            }
            while (collision == true);
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("o");
            Console.ResetColor();
        }
        public void RemoveFruit()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }
    }
}
