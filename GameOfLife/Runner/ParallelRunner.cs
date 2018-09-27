using GameOfLife.Constants;
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

        private Dictionary<int, GameRunner> gameRunnerInstances;

        private int totalLiveCells = 0;
        private int totalIterations = 0;

        public ParallelRunner()
        {
            _consoleUserInterface = new ConsoleUserInterface();
            gameRunnerInstances = new Dictionary<int, GameRunner>();
        }

        public void Run()
        {
            int maxGames;
            _consoleUserInterface.AskForGameCountToRun();
            string input = Console.ReadLine();
            while (!int.TryParse(input, out maxGames) || maxGames < 1 || maxGames > 1000)
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine("Choose game count to run: ");
                input = Console.ReadLine();
            }

            int x = _consoleUserInterface.GetUserInput("x");
            int y = _consoleUserInterface.GetUserInput("y");

            gameRunnerInstances = CreateParallelInstances(maxGames, x, y);

            foreach (var entry in gameRunnerInstances)
            {
                Console.WriteLine($"created game no. {entry.Key + 1}");
            }

            Console.WriteLine("Enter game numbers to show. If you wish to show less than 8 games, end with 0.");

            int gameNo;
            var gamesToShow = new int?[ConstantValues.MaxRunningGames];
            for (int i = 0; i < ConstantValues.MaxRunningGames; i++)
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
                    for (int i = 0; i < gamesToShow.Length; i++)
                    {
                        if (entry.Key == gamesToShow[i])
                        {
                            Console.WriteLine($"\nGame no. {entry.Key}");
                            entry.Value.Process();

                            entry.Value.Show();

                            if (entry.Value.MatrixField.LiveCells == 0)
                            {
                                _consoleUserInterface.GameOverOutput();
                            }
                            totalIterations =+ entry.Value.MatrixField.Iterations;
                            totalLiveCells =+ entry.Value.MatrixField.LiveCells;
                            
                        }
                    }
                }
                _consoleUserInterface.LiveCellsOutput(totalLiveCells);
                _consoleUserInterface.IterationsOutput(totalIterations);
                Thread.Sleep(1000);
            } while (totalLiveCells != 0 && !_consoleUserInterface.IsAnyKeyPressed());
            Console.ReadLine();
        }

        public Dictionary<int, GameRunner> CreateParallelInstances(int maxGames, int x, int y)
        {
            var gameRunnerInstances = new Dictionary<int, GameRunner>(maxGames);
            Parallel.For(0, maxGames, i =>
            {
                var gameRunnerInstance = new GameRunner();
                gameRunnerInstance.Start(x, y);
                gameRunnerInstances.Add(i, gameRunnerInstance);
            });
            return gameRunnerInstances;
        }
    }
}
