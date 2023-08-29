using System;
using System.Linq;

namespace snakev0
{
    class SecondSnake : Snake
    {

        public SecondSnake(int arenawidth, int arenaheight)
        {
            this.headx = Console.WindowWidth / 2;
            this.heady = arenaheight / 2-4;
        }
        public override void DrawSnake()
        {
            Console.SetCursorPosition(headx, heady);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("██");
            Console.ResetColor();
        }

        public override void MoveSnake()
        {
            for (var i = 0; i < lenght; i++)
            {
                if (i == lenght - 1)
                {
                    segments.ElementAt(i).x = PrevStepX();
                    segments.ElementAt(i).y = PrevStepY();
                }
                else
                {
                    segments.ElementAt(i).x = segments.ElementAt(i + 1).x;
                    segments.ElementAt(i).y = segments.ElementAt(i + 1).y;
                }
            }
        }
    }
}
