using System;
using System.Collections.Generic;
using System.Linq;

namespace snakev0
{
    class FirstSnake:Snake
    {
        public FirstSnake(int arenawidth, int arenaheight)
        {
            this.headx = Console.WindowWidth / 2;
            this.heady = arenaheight / 2 + 4;
        }

        public override void DrawSnake()
        {
            Console.SetCursorPosition(headx, heady);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("██");
            Console.ResetColor();
        }          
    }
}
