using System;
using System.Threading;

namespace snakev0
{
    class Menu
    {
        private string[] elements = new string[0];

        public void Setup(string[] menuElements)
        {
            if (menuElements != null)
            {
                elements = menuElements;
            }
        }
        public int Open()
        {
            Console.CursorVisible = false;
            DrawArt();
            var max = 0;
            Console.CursorVisible = false;
            var chosen = 0;
            ConsoleKeyInfo key;
            
            do
            {
                for (var i = 0; i < elements.Length; i++)
                {

                    for (var j = 0; j < elements.Length; j++)
                    {
                        
                        if (elements[j].Length > max)
                        {
                            max = elements[j].Length;
                        }
                    }
                   
                    if (i == chosen)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Beep(500, 50);
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    Console.SetCursorPosition((Console.WindowWidth / 2)-5, (Console.WindowHeight / 2) + i);
                    Console.WriteLine(elements[i].PadRight(max));

                }

                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow && chosen > 0)
                {
                    chosen--;
                }
                else if (key.Key == ConsoleKey.DownArrow && chosen < elements.Length - 1)
                {
                    chosen++;
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    chosen = -1;
                }

            } while (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Escape);

            Console.CursorVisible = true;
            Console.ResetColor();
            return chosen;
        }
        private void DrawArt()
        {
            Console.SetCursorPosition(1 , (Console.WindowHeight / 2) -7);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(@" 
                                                       .""-.
                                                      |° °  \
                                                      \.. /  |
                                                       v-v)  ;
                                                         _/  /_");
 
        }
        public void StartScreen()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(Console.WindowWidth/2, (Console.WindowHeight / 2) - 7);
            Console.WriteLine(@"
                                    ________  ________   ________  ___  __    _______      
                                   |\   ____\|\   ___  \|\   __  \|\  \|\  \ |\  ___ \     
                                   \ \  \___|\ \  \\ \  \ \  \|\  \ \  \/  /|\ \   __/|    
                                    \ \_____  \ \  \\ \  \ \   __  \ \   ___  \ \  \_|/__  
                                     \|____|\  \ \  \\ \  \ \  \ \  \ \  \\ \  \ \  \_|\ \ 
                                       ____\_\  \ \__\\ \__\ \__\ \__\ \__\\ \__\ \_______\
                                      |\_________\|__| \|__|\|__|\|__|\|__| \|__|\|_______|
                                      \|_________|            

                                                    Press any key to start...                                                                ");
            var counter = 0;
            int[,] notes = {
                { 196, 300, 100 }, {196,200,0},
                {294,200,0}, { 262, 400, 100},
                { 262, 100, 100}, {247, 200,100},
                {262, 300,100}, {247, 300, 100},
                {220,400,100}, {220,300,100},
                {220,100,100}, {247,100,100},
                {262,300,100}, {262, 100,100},
                {247,100,100}, {262, 100,100},
                {247, 100,100}, {220,200,200},
                {196,400,200} };

            do
            {
                if (counter == 19) counter = 0;

                Console.Beep(notes[counter, 0], notes[counter, 1]);
                Thread.Sleep(notes[counter, 2]);

                counter++;
            } while (!Console.KeyAvailable);
            Console.ReadKey();
            Console.Clear();
        }
    }
}
    

