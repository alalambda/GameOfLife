using GameOfLife.Interfaces;
using GameOfLife.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Runner
{
    public class ParallelRunner
    {
        private readonly IUserInterface _consoleUserInterface;

        public ParallelRunner()
        {
            _consoleUserInterface = new ConsoleUserInterface();
        }

        public void Run()
        {
            int maxGames;
            Console.WriteLine("Choose game count to run: ");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out maxGames) || maxGames > 1000)
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine("Choose game count to run: ");
                input = Console.ReadLine();
            }

            int x = _consoleUserInterface.GetDimensionInput("x");
            int y = _consoleUserInterface.GetDimensionInput("y");

            var gameRunnerInstances = new Dictionary<int, GameRunner>(maxGames);
            
            Parallel.For(0, maxGames, i =>
            {
                var gameRunnerInstance = new GameRunner();
                gameRunnerInstance.Start(x, y);
                gameRunnerInstances.Add(i, gameRunnerInstance);
            });

            foreach (var entry in gameRunnerInstances)
            {
                Console.WriteLine($"created game no. {entry.Key + 1}");
            }

            Console.WriteLine("Enter game numbers to show. If you wish to show less than 8 games, end with 0.");

            int gameNo;
            var gamesToShow = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                input = Console.ReadLine();
                while (!int.TryParse(input, out gameNo))
                {
                    Console.WriteLine("Invalid input.");
                    Console.WriteLine("Enter game no. to show: ");
                    input = Console.ReadLine();
                }
                if (gameNo == 0) break;
                gamesToShow.Add(gameNo);
            }

            foreach (var entry in gameRunnerInstances)
            {
                foreach (var gameNumber in gamesToShow)
                {
                    if (entry.Key == gameNumber)
                    {
                        entry.Value.Show();
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
