using GameOfLife.Interfaces;
using GameOfLife.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Runner
{
    public class ParallelRunner
    {
        private readonly IUserInterface _consoleUserInterface;

        private int totalLiveCells = 0;

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
            var gamesToShow = new int?[8];
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
                gamesToShow[i] = gameNo;
            }

            do
            {
                Console.Clear();
                foreach (var entry in gameRunnerInstances)
                {
                    totalLiveCells = 0;
                    for (int i = 0; i < gamesToShow.Length; i++)
                    {
                        if (entry.Key == gamesToShow[i])
                        {
                            Console.WriteLine($"\nGame no. {entry.Key}");
                            entry.Value.Process();
                            totalLiveCells =+ entry.Value.MatrixField.LiveCells;
                            entry.Value.Show();
                            if (entry.Value.MatrixField.LiveCells == 0)
                            {
                                _consoleUserInterface.GameOverOutput();
                            }
                        }
                    }
                } 
            } while (totalLiveCells != 0 || !_consoleUserInterface.IsAnyKeyPressed());
            Console.ReadLine();
        }
    }
}
