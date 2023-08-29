using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace snakev0
{
    class GameSingle : Game
    {

        ConsoleKeyInfo keyInfo;
        ConsoleKey key;
        ConsoleKey skey;

        public override void StartGame()
        {
            TypeNick();
            Difficulties();
            snake.segments = new List<SnakeSegments>();
            snake.GameOverBool = false;
            Console.Clear();
            Console.CursorVisible = false;
            arena.DrawArena();
            snake.DrawSnake();
            fruit.DrawFruit(snake);
            while (!snake.GameOverBool)
            {
                snake.HitMs();
                HandleInput();
                snake.MoveSnake();
                MoveLogic();
                ScoreCounter();
                Logic();
                if (!growingspeedbool)
                {
                    Thread.Sleep(speed);
                }
                else
                {
                    Thread.Sleep(growingspeed);
                }
            }

        }
        protected override void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
                if (key == ConsoleKey.RightArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow)
                {
                    skey = key;
                }
            }
        }

        protected override void Logic()
        {

            if ((fruit.x == snake.Headx || fruit.x == snake.Headx + 1) && fruit.y == snake.Heady)
            {
                Console.Beep(400, 10);
                snake.Lenght++;
                snake.segments.Add(new SnakeSegments(snake.PrevStepX(), snake.PrevStepY()));
                fruit.DrawFruit(snake);
                if (growingspeed > 25)
                {
                    growingspeed -= 5;
                }
            }
            if ((snake.Headx > (Console.WindowWidth / 2 + arenaWidth / 2) - 2) || (snake.Headx < (Console.WindowWidth / 2 - arenaWidth / 2) + 2) || (snake.Heady < 1) || (snake.Heady > arena.Height - 1))
            {
                Console.SetCursorPosition(snake.Headx, snake.Heady);
                GameOver();

            }
            if (snake.HitMs())
            {
                GameOver();
            }

        }

        protected override void GameOver()
        {
            Console.CursorVisible = false;
            scoreboard = new ScoreBoard();
            var gameo = "Game Over";
            var nrecord = "!!!NEW RECORD!!!";
            var result = $"{snake.Nick}'s score: {snake.Lenght}";
            Console.Beep(300, 600);
            Console.Clear();
            Thread.Sleep(20);
            Console.SetCursorPosition(0, (Console.WindowHeight / 2) - 7);
            Console.Write(@" 
                                                       .""-.
                                                      |x x  \
                                                      \   /  |
                                                       '-')  ;
                                                         _/  /_");
            Console.SetCursorPosition(Console.WindowWidth / 2 - gameo.Length / 2, (Console.WindowHeight / 2));
            if (snake.Lenght > scoreboard.SortSB().Take(1).First().Value)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - nrecord.Length / 2 - 2, 3);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(nrecord);
            }
            scoreboard.SaveResult(snake.Nick, snake.Lenght);
            Console.SetCursorPosition(Console.WindowWidth / 2 - result.Length / 2, (Console.WindowHeight / 2) + 2);
            Console.Write(result);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 14, (Console.WindowHeight / 2) + 4);
            Console.WriteLine("Doubleclick any key to continue...");
            Console.ReadKey();
            Console.ReadKey();
            snake.GameOverBool = true;
        }
        protected override void ScoreCounter()
        {
            Console.SetCursorPosition((Console.WindowWidth / 2) - 3, arenaHeight + 1);
            Console.Write($"Scores: {snake.Lenght}");
        }
        protected override void TypeNick()
        {
            snake.Nick = "";
            Console.Clear();
            var text = "Type your Nick: ";
            var text1 = "Damn... You don't even know how Nick looks like.";
            var text2 = "Try again: ";
            var text3 = "This Nick already exists in scoreboard. Type another Nick.";
            Console.SetCursorPosition((Console.WindowWidth / 2 - text.Length / 2) - 5, Console.WindowHeight / 2);
            Console.WriteLine(text);
            Console.SetCursorPosition((Console.WindowWidth / 2 + text.Length / 2) - 5, Console.WindowHeight / 2);
            snake.Nick = Console.ReadLine();
            while (snake.Nick.Length < 3 || snake.Nick.Length > 11 || snake.Nick.Contains(" ") || snake.Nick == "" || snake.Nick == null || IsNickduplicated())
            {
                if (IsNickduplicated())
                {
                    Console.Clear();
                    Console.SetCursorPosition((Console.WindowWidth / 2 - text3.Length / 2), Console.WindowHeight / 2);
                    Console.WriteLine(text3);
                    Console.SetCursorPosition(Console.WindowWidth / 2 - text2.Length / 2, Console.WindowHeight / 2 + 2);
                    Console.WriteLine(text2);
                    Console.SetCursorPosition(Console.WindowWidth / 2 - text2.Length / 2 + text2.Length, Console.WindowHeight / 2 + 2);
                    snake.Nick = Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Console.SetCursorPosition((Console.WindowWidth / 2 - text1.Length / 2), Console.WindowHeight / 2);
                    Console.WriteLine(text1);
                    Console.SetCursorPosition(Console.WindowWidth / 2 - text2.Length / 2, Console.WindowHeight / 2 + 2);
                    Console.WriteLine(text2);
                    Console.SetCursorPosition(Console.WindowWidth / 2 - text2.Length / 2 + text2.Length, Console.WindowHeight / 2 + 2);
                    snake.Nick = Console.ReadLine();
                }

            }

        }
        protected override void MoveLogic()
        {
            if (skey == ConsoleKey.RightArrow)
            {
                if (!snake.Goleft)
                    snake.GoRight();
                else snake.GoLeft();
            }
            else if (skey == ConsoleKey.LeftArrow)
            {
                if (!snake.Goright)
                    snake.GoLeft();
                else snake.GoRight();
            }
            else if (skey == ConsoleKey.UpArrow)
            {
                if (!snake.Godown)
                    snake.GoUp();
                else snake.GoDown();
            }
            else if (skey == ConsoleKey.DownArrow)
            {
                if (!snake.Goup)
                    snake.GoDown();
                else snake.GoUp();
            }


        }
        private bool IsNickduplicated()
        {
            scoreboard = new ScoreBoard();
            var sb = scoreboard.SortSB();

            if (sb.Any(x => x.Key == snake.Nick))
            {
                return true;
            }
            return false;
        }

        private void Difficulties()
        {
            Console.Clear();
            Menu menu = new Menu();
            string[] elements = { "Easy", "Medium", "Hard", "Super Hard", "Growing" };
            menu.Setup(elements);
            var selection = menu.Open();
            switch (selection)
            {
                case 0:
                    speed = 200;
                    break;
                case 1:
                    speed = 150;
                    break;
                case 2:
                    speed = 100;
                    break;
                case 3:
                    speed = 50;
                    break;
                case 4:
                    growingspeedbool = true;
                    break;

            }
            Console.Clear();
        }





    }
}
