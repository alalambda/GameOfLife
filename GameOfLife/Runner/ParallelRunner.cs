using GameOfLife.Constants;
using GameOfLife.Interfaces;
using GameOfLife.UserInterface;
using GameOfLife.Validators;
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

        private readonly Validator _validator;

        private Dictionary<int, GameRunner> gameRunnerInstances;

        private int totalLiveCells = 0;
        private int totalIterations = 0;

        public ParallelRunner()
        {
            _consoleUserInterface = new ConsoleUserInterface();
            _validator = new Validator();
            gameRunnerInstances = new Dictionary<int, GameRunner>();
        }

        public void Run()
        {
            int maxGames = GetMaxGames();

            int x = GetDimensionX();

            int y = GetDimensionY();

            gameRunnerInstances = CreateParallelRunningInstances(maxGames, x, y);

            foreach (var entry in gameRunnerInstances)
            {
                Console.WriteLine($"created game no. {entry.Key + 1}");
            }

            Console.WriteLine("Enter game numbers to show. If you wish to show less than 8 games, end with 0.");

            int gameNo;
            string input;
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
                Thread.Sleep(1000);
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
                            totalIterations += entry.Value.MatrixField.Iterations;
                            totalLiveCells += entry.Value.MatrixField.LiveCells;
                        }
                    }
                }
                _consoleUserInterface.LiveCellsOutput(totalLiveCells);
                _consoleUserInterface.IterationsOutput(totalIterations);
            } while (totalLiveCells != 0 && !_consoleUserInterface.IsAnyKeyPressed());
            Console.ReadLine();
        }

        public Dictionary<int, GameRunner> CreateParallelRunningInstances(int maxGames, int x, int y)
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

        public int GetMaxGames()
        {
            int maxGames = _consoleUserInterface.GetUserInput("Game count to run");
            while (!_validator.IsMaxGamesCountInputValid(maxGames))
            {
                _consoleUserInterface.InvalidInputMessage();
                maxGames = _consoleUserInterface.GetUserInput("Game count to run");
            }
            return maxGames;
        }

        public int GetDimensionX()
        {
            int x = _consoleUserInterface.GetUserInput("x");
            while (!_validator.IsDimensionInputValid(x))
            {
                _consoleUserInterface.InvalidInputMessage();
                x = _consoleUserInterface.GetUserInput("x");
            }
            return x;
        }

        public int GetDimensionY()
        {
            int y = _consoleUserInterface.GetUserInput("y");
            while (!_validator.IsDimensionInputValid(y))
            {
                _consoleUserInterface.InvalidInputMessage();
                y = _consoleUserInterface.GetUserInput("y");
            }
            return y;
        }
    }
}
