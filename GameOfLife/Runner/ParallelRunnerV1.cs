using GameOfLife.Interfaces;
using GameOfLife.UserInterface;
using GameOfLife.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLife.Runner
{
    public class ParallelRunnerV1
    {
        private readonly IUserInterface _consoleUserInterface;

        private readonly Validator _validator;

        private Dictionary<int, Task> gameTasks;
        private Dictionary<int, Task> showTasks;

        public ParallelRunnerV1()
        {
            _consoleUserInterface = new ConsoleUserInterface();
            _validator = new Validator();
            gameTasks = new Dictionary<int, Task>();
            showTasks = new Dictionary<int, Task>();
        }

        public void RunParallel()
        {
            int maxGames = GetMaxGames();
            int x = GetDimensionX();
            int y = GetDimensionY();

            CreateTasks(maxGames, x, y);
            StartAllTasks();
            Console.ReadLine();
        }

        private void CreateTasks(int maxGames, int x, int y)
        {
            Parallel.For(0, maxGames, i =>
            {
                var gameInstance = new GameRunner();
                var gameTask = new Task(() => gameInstance.Start(x, y));
                var showTask = new Task(() => gameInstance.Show());
                gameTasks.Add(i, gameTask);
                showTasks.Add(i, showTask);
            });
        }

        private void StartAllTasks()
        {
            foreach (var gameTask in gameTasks)
            {
                gameTask.Value.Start();
                Console.WriteLine($"{gameTask.Key + 1}. game is running");
            }
        }

        private void ShowGame(int i)
        {
            showTasks.
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
