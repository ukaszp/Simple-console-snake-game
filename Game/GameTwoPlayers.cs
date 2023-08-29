using System;
using System.Collections.Generic;
using System.Threading;

namespace snakev0
{
    class GameTwoPlayers : Game
    {
        protected SecondSnake secondsnake = new SecondSnake(arenaWidth, arenaHeight);
        ConsoleKeyInfo keyInfo;
        ConsoleKey key;
        ConsoleKey firstkey;
        ConsoleKey seckey;

        public override void StartGame()
        {
            Console.CursorVisible = false;
            TypeNick();
            InformBoard();
            Console.Clear();
            snake.segments = new List<SnakeSegments>();
            secondsnake.segments = new List<SnakeSegments>();
            snake.GameOverBool = false;
            secondsnake.GameOverBool = false;
            arena.DrawArena();
            snake.DrawSnake();
            secondsnake.DrawSnake();
            fruit.DrawFruit(snake, secondsnake);
            while (!snake.GameOverBool && !secondsnake.GameOverBool)
            {
                snake.HitMs();
                secondsnake.HitMs();
                HandleInput();
                ChooseHandleInput();
                MoveLogic();
                secondsnake.MoveSnake();
                snake.MoveSnake();
                ScoreCounter();
                Logic();
                Thread.Sleep(speed);
            }
        }
        protected override void Logic()
        {
            if (fruit.x == snake.Headx || fruit.x == snake.Headx + 1)
            {
                if (fruit.y == snake.Heady)
                {
                    Console.Beep(400, 10);
                    snake.Lenght++;
                    snake.segments.Add(new SnakeSegments(snake.PrevStepX(), snake.PrevStepY()));
                    fruit.DrawFruit(snake, secondsnake);
                }
            }
            if ((snake.Headx > (Console.WindowWidth / 2 + arenaWidth / 2) - 2) || (snake.Headx < (Console.WindowWidth / 2 - arenaWidth / 2) + 2) || (snake.Heady < 1) || (snake.Heady > arena.Height - 1))
            {
                Console.SetCursorPosition(snake.Headx, snake.Heady);
                GameOver();
                snake.GameOverBool = true;
            }
            if (snake.HitMs() || SnakesCollision())
            {
                GameOver();
                snake.GameOverBool = true;
            }
            //secondsnake
            if (fruit.x == secondsnake.Headx || fruit.x == secondsnake.Headx + 1)
            {
                if (fruit.y == secondsnake.Heady)
                {
                    Console.Beep(400, 10);
                    secondsnake.Lenght++;
                    secondsnake.segments.Add(new SnakeSegments(secondsnake.PrevStepX(), secondsnake.PrevStepY()));
                    fruit.DrawFruit(snake);
                }
            }
            if ((secondsnake.Headx > (Console.WindowWidth / 2 + arenaWidth / 2) - 2) || (secondsnake.Headx < (Console.WindowWidth / 2 - arenaWidth / 2) + 2) || (secondsnake.Heady < 1) || (secondsnake.Heady > arena.Height - 1))
            {
                Console.SetCursorPosition(secondsnake.Headx, secondsnake.Heady);
                GameOver();
                secondsnake.GameOverBool = true;
            }
            if (secondsnake.HitMs())
            {
                GameOver();
                secondsnake.GameOverBool = true;
            }
        }
        protected override void GameOver()
        {
            Console.CursorVisible = false;
            string gameo = "Game Over";
            string result = $"{snake.Nick}'s score: {snake.Lenght}";
            string result2 = $"{secondsnake.Nick}'s score: {secondsnake.Lenght}";
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
            Console.Write(gameo);
            if (snake.GameOverBool == true)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - gameo.Length / 2, 2);
                Console.WriteLine($">>>{snake.Nick} is the winner<<<");
            }
            else if (secondsnake.GameOverBool == true)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - gameo.Length / 2, 2);
                Console.WriteLine($">>>{secondsnake.Nick} is the winner<<<");
            }
            Console.SetCursorPosition(Console.WindowWidth / 2 - result.Length / 2, (Console.WindowHeight / 2) + 2);
            Console.Write(result);
            Console.SetCursorPosition(Console.WindowWidth / 2 - result.Length / 2, (Console.WindowHeight / 2) + 4);
            Console.Write(result2);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 14, (Console.WindowHeight / 2) + 6);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            snake.GameOverBool = true;
            secondsnake.GameOverBool = true;
        }
        protected override void ScoreCounter()
        {
            Console.SetCursorPosition((Console.WindowWidth / 2) - 9, arenaHeight + 1);
            Console.Write($"{snake.Nick} scores: {snake.Lenght}");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 9, arenaHeight + 2);
            Console.Write($"{secondsnake.Nick} scores: {secondsnake.Lenght}");
        }

        protected override void TypeNick()
        {
            snake.Nick = "";
            secondsnake.Nick = "";
            Console.Clear();
            string text = "type your Nick: ";
            string text1 = "Damn... You don't even know how Nick looks like.";
            string text2 = "Try again: ";
            string text3 = "You cannot choose the same nick as first player.";
            Console.SetCursorPosition((Console.WindowWidth / 2 - text.Length / 2) - 5, Console.WindowHeight / 2);
            Console.WriteLine($"First Player, {text}");
            Console.SetCursorPosition((Console.WindowWidth / 2 + text.Length / 2) + 9, Console.WindowHeight / 2);
            snake.Nick = Console.ReadLine();
            while (snake.Nick.Length < 3 || snake.Nick.Length > 11 || snake.Nick.Contains(" ") || snake.Nick == "" || snake.Nick == null)
            {
                Console.Clear();
                Console.SetCursorPosition((Console.WindowWidth / 2 - text1.Length / 2), Console.WindowHeight / 2);
                Console.WriteLine(text1);
                Console.SetCursorPosition(Console.WindowWidth / 2 - text2.Length / 2, Console.WindowHeight / 2 + 2);
                Console.WriteLine(text2);
                Console.SetCursorPosition(Console.WindowWidth / 2 - text2.Length / 2 + text2.Length, Console.WindowHeight / 2 + 2);
                snake.Nick = Console.ReadLine();
            }
            Console.Clear();
            //secondsnake
            Console.SetCursorPosition((Console.WindowWidth / 2 - text.Length / 2) - 5, Console.WindowHeight / 2);
            Console.WriteLine($"Second Player, {text}");
            Console.SetCursorPosition((Console.WindowWidth / 2 + text.Length / 2) + 10, Console.WindowHeight / 2);
            secondsnake.Nick = Console.ReadLine();
            while (secondsnake.Nick.Length < 3 || secondsnake.Nick.Length > 14 || secondsnake.Nick.Contains(" ") || secondsnake.Nick == "" || secondsnake.Nick == null || secondsnake.Nick == snake.Nick)
            {
                if (secondsnake.Nick == snake.Nick)
                {
                    Console.Clear();
                    Console.SetCursorPosition((Console.WindowWidth / 2 - text1.Length / 2), Console.WindowHeight / 2);
                    Console.WriteLine(text3);
                    Console.SetCursorPosition(Console.WindowWidth / 2 - text2.Length / 2, Console.WindowHeight / 2 + 2);
                    Console.WriteLine(text2);
                    Console.SetCursorPosition(Console.WindowWidth / 2 - text2.Length / 2 + text2.Length, Console.WindowHeight / 2 + 2);
                    secondsnake.Nick = Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Console.SetCursorPosition((Console.WindowWidth / 2 - text1.Length / 2), Console.WindowHeight / 2);
                    Console.WriteLine(text1);
                    Console.SetCursorPosition(Console.WindowWidth / 2 - text2.Length / 2, Console.WindowHeight / 2 + 2);
                    Console.WriteLine(text2);
                    Console.SetCursorPosition(Console.WindowWidth / 2 - text2.Length / 2 + text2.Length, Console.WindowHeight / 2 + 2);
                    secondsnake.Nick = Console.ReadLine();
                }
            }
        }

        private void InformBoard()
        {
            Console.CursorVisible = false;
            string text1 = $">{snake.Nick} you're GREEN, you move using ARROWS";
            string text2 = $">{secondsnake.Nick} you're BLUE, you move using WSAD";
            string text3 = "Press any key to start...";
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth / 2 - text1.Length / 2) - 5, Console.WindowHeight / 2);
            Console.WriteLine(text1);
            Console.SetCursorPosition((Console.WindowWidth / 2 - text1.Length / 2) - 5, Console.WindowHeight / 2 + 2);
            Console.WriteLine(text2);
            Console.SetCursorPosition((Console.WindowWidth / 2 - text1.Length / 2 + text3.Length / 2) - 3, Console.WindowHeight / 2 + 4);
            Console.WriteLine(text3);
            Console.ReadKey();
            Console.ReadKey();
        }

        protected override void MoveLogic()
        {
            if (firstkey == ConsoleKey.RightArrow)
            {
                if (!snake.Goleft)
                    snake.GoRight();
                else snake.GoLeft();
            }
            else if (firstkey == ConsoleKey.LeftArrow)
            {
                if (!snake.Goright)
                    snake.GoLeft();
                else snake.GoRight();
            }
            else if (firstkey == ConsoleKey.UpArrow)
            {
                if (!snake.Godown)
                    snake.GoUp();
                else snake.GoDown();
            }
            else if (firstkey == ConsoleKey.DownArrow)
            {
                if (!snake.Goup)
                    snake.GoDown();
                else snake.GoUp();
            }
            //secondsnake
            if (seckey == ConsoleKey.D)
            {
                if (!secondsnake.Goleft)
                    secondsnake.GoRight();
                else secondsnake.GoLeft();
            }
            else if (seckey == ConsoleKey.A)
            {
                if (!secondsnake.Goright)
                    secondsnake.GoLeft();
                else secondsnake.GoRight();
            }
            else if (seckey == ConsoleKey.W)
            {
                if (!secondsnake.Godown)
                    secondsnake.GoUp();
                else secondsnake.GoDown();
            }
            else if (seckey == ConsoleKey.S)
            {
                if (!secondsnake.Goup)
                    secondsnake.GoDown();
                else secondsnake.GoUp();
            }
        }
        protected override void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
            }
        }
        private void ChooseHandleInput()
        {
            if (key == ConsoleKey.RightArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow)
            {
                firstkey = key;
            }
            else if (key == ConsoleKey.W || key == ConsoleKey.A || key == ConsoleKey.S || key == ConsoleKey.D)
            {
                seckey = key;
            }
            else { }

        }
        private bool SnakesCollision()
        {
            if (snake.segments.Count > 0 || secondsnake.segments.Count > 0)
            {
                foreach (SnakeSegments seg in snake.segments)
                {
                    if (secondsnake.Headx == seg.x && secondsnake.Heady == seg.y)
                    {
                        return true;
                    }
                }
                foreach (SnakeSegments seg in secondsnake.segments)
                {
                    if (snake.Headx == seg.x && snake.Heady == seg.y)
                    {
                        return true;
                    }
                }
            }
            if ((snake.Headx == secondsnake.Headx || snake.Headx + 1 == secondsnake.Headx || snake.Headx == secondsnake.Headx + 1) && snake.Heady == secondsnake.Heady)
            {
                return true;
            }
            return false;
        }
    }

}
