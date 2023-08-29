using System;

namespace snakev0
{
    class Arena
    {
        protected int height, width;
        public Arena(int height, int width)
        {
            this.height = height;
            this.width = width;
        }

        public int Height
        {
            get
            {
                return height;
            }
        }
        public int Width
        {
            get
            {
                return width;
            }
        }



        public void DrawArena()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - width / 2, 0);
            for (var i = 0; i < height+1; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - width / 2, i);
                Console.WriteLine($"██{("").PadRight(width-2,' ')}██");
            }
            Console.SetCursorPosition(Console.WindowWidth / 2 - width / 2, 0);
            Console.Write(("").PadRight(width, '█'));
            Console.SetCursorPosition(Console.WindowWidth / 2 - width / 2, height);
            Console.Write(("").PadRight(width, '█'));            
        }
    }
}
