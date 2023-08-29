using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace snakev0
{
    class ScoreBoard
    {
        protected string path=@"ScoreBoard.txt";
        public virtual void SaveResult(string nick, int score)
        {
            string scoreresult = ($"{nick} {score}");
            StreamWriter sw;
            if(!File.Exists(path))
            {
                sw = File.CreateText(path);
            }
            else
            {
                sw = new StreamWriter(path, true);
            }
            sw.WriteLine(scoreresult);
            sw.Close();
        }
        public Dictionary<string, int> SortSB()
        {
            string[] lines= File.ReadAllLines(path);
            Dictionary<string, int> playersScores = new Dictionary<string,int>();
            foreach(var line in lines)
            {
                var values = line.Split(' ');
                playersScores.Add(values[0], Int32.Parse(values[1]));
            }
            var ordered= playersScores.OrderByDescending(y => y.Value).ToDictionary(y=>y.Key, y=>y.Value);
            return ordered; 
        }

        public void PrintSB()
        {
            var text = "-------Top 10 players-------";
            var text1 = "Scoreboard is empty";
            var counter = 0;
            Console.Clear();
            Console.CursorVisible = false;
           
                SortSB();
                Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2 + 4, 3);
                Console.WriteLine(text);
                if (SortSB().Count < 10)
                {
                    foreach (var value in SortSB())
                    {
                        counter++;
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 10 / 2 + counter - 4);
                        Console.WriteLine($"{counter}. {value.Key}: {value.Value}");
                    }
                }
                else
                {
                    foreach (var value in SortSB().Take(10))
                    {
                        counter++;
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 10 / 2 + counter - 4);
                        Console.WriteLine($"{counter}. {value.Key}: {value.Value}");
                    }
                }
                Console.SetCursorPosition(Console.WindowWidth / 2 - text.Length / 2, Console.WindowHeight / 2 - 10 / 2 + counter + 5);
                Console.WriteLine("Press any key to go back to menu...");

                Console.ReadKey();

           
            if(SortSB().Count==0||SortSB()==null)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - text1.Length / 2, Console.WindowHeight / 2 - 1);
                Console.WriteLine(text1);
                Console.ReadKey();
            }

        }
    }
}
