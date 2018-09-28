using GameOfLife.Constants;
using GameOfLife.Interfaces;
using GameOfLife.Logic;
using GameOfLife.UserInterface;
using GameOfLife.Validators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife.Runner
{
    public class ParallelRunnerV1
    {
        private readonly IUserInterface _consoleUserInterface;
        private readonly IGameLogic _gameLogic;

        private readonly Validator _validator;

        private ConcurrentDictionary<int, Task> _gameTasks;
        private ConcurrentDictionary<int, GameRunner> _gameRunnerInstances;

        public ParallelRunnerV1()
        {
            _consoleUserInterface = new ConsoleUserInterface();
            _gameLogic = new GameLogic();
            _validator = new Validator();
            _gameTasks = new ConcurrentDictionary<int, Task>();
            _gameRunnerInstances = new ConcurrentDictionary<int, GameRunner>();
        }

        public void RunParallel()
        {
            int maxGames = GetMaxGames();
            int x = GetDimensionX();
            int y = GetDimensionY();

            CreateTasks(maxGames, x, y);
            StartAllTasks();
            GetSelectedGamesAndShow();
            Console.ReadLine();
        }

        public void SaveAll()
        {
            Parallel.ForEach(_gameRunnerInstances, gameRunnerInstance =>
                {
                    var matrixField = gameRunnerInstance.Value.MatrixField;
                    _gameLogic.SaveGame(matrixField);
                });
        }

        public void GetSelectedGamesAndShow()
        {
            int?[] selectedGames = GetSelectedGames();
            DisplaySelectedGames(selectedGames);
        }

        public int?[] GetSelectedGames()
        {
            var selectedGames = new int?[ConstantValues.MaxRunningGames];
            _consoleUserInterface.AskForSelectedGames();
            for (int i = 0; i < ConstantValues.MaxRunningGames; i++)
            {
                int gameNo;
                string input = Console.ReadLine();
                while (!int.TryParse(input, out gameNo))
                {
                    Console.WriteLine("Invalid input.");
                    Console.WriteLine("Enter game no. to show: ");
                    input = Console.ReadLine();
                }
                if (gameNo == 0) break;
                selectedGames[i] = gameNo - 1;
            }
            return selectedGames;
        }

        public void DisplaySelectedGames(int?[] selectedGames)
        {
            int totalLiveCells;
            do
            {
                totalLiveCells = 0;
                int totalIterations = 0;
                Thread.Sleep(1000);
                Console.Clear();
                //if (_consoleUserInterface.IsGameSaveRequired())
                //{
                //    SaveAll();
                //}
                for (int i = 0; i < selectedGames.Length; i++)
                {
                    if (selectedGames[i] != null)
                    {
                        Console.WriteLine($"\nGame {selectedGames[i].Value + 1}");
                        _gameRunnerInstances[selectedGames[i].Value].Show();
                        totalLiveCells += _gameRunnerInstances[selectedGames[i].Value].MatrixField.LiveCells;
                        totalIterations += _gameRunnerInstances[selectedGames[i].Value].MatrixField.Iterations;
                        
                    }
                }
                _consoleUserInterface.LiveCellsOutput(totalLiveCells);
                _consoleUserInterface.IterationsOutput(totalIterations);
                if (totalLiveCells == 0)
                {
                    _consoleUserInterface.GameOverOutput();
                    break;
                }
            } while (totalLiveCells != 0 || !_consoleUserInterface.IsAnyKeyPressed());
        }

        public void DisplaySelectedGame(int selectedGame)
        {
            int? totalLiveCells = null;
            int totalIterations = 0;

            Console.Clear();
            while (totalLiveCells != 0)
            {
                Console.Clear();
                Thread.Sleep(1000);
                
                totalLiveCells = 0;
                Console.WriteLine($"\nGame {selectedGame + 1}");
                _gameRunnerInstances[selectedGame].Show();
                totalLiveCells += _gameRunnerInstances[selectedGame].MatrixField.LiveCells;
                totalIterations += _gameRunnerInstances[selectedGame].MatrixField.Iterations;
            }
            _consoleUserInterface.LiveCellsOutput(totalLiveCells.Value);
            _consoleUserInterface.IterationsOutput(totalIterations);
        }

        private void CreateTasks(int maxGames, int x, int y)
        {
            Parallel.For(0, maxGames, i =>
            {
                var gameInstance = new GameRunner();
                var gameTask = new Task(() => gameInstance.Start(x, y));
                _gameTasks.TryAdd(i, gameTask);
                _gameRunnerInstances.TryAdd(i, gameInstance);
            });
        }

        private void StartAllTasks()
        {
            Parallel.ForEach(_gameTasks, gameTask =>
            {
                gameTask.Value.Start();
                Console.WriteLine($"{gameTask.Key + 1}. game is {gameTask.Value.Status}");
            });
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
