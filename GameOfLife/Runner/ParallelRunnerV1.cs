﻿using GameOfLife.Constants;
using GameOfLife.Interfaces;
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

        private readonly Validator _validator;

        private ConcurrentDictionary<int, Task> _gameTasks;
        private ConcurrentDictionary<int, Action> _showActions;

        public ParallelRunnerV1()
        {
            _consoleUserInterface = new ConsoleUserInterface();
            _validator = new Validator();
            _gameTasks = new ConcurrentDictionary<int, Task>();
            _showActions = new ConcurrentDictionary<int, Action>();
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

        public void GetSelectedGamesAndShow()
        {
            while (true)
            {
                int?[] selectedGames = GetSelectedGames();
                Console.Clear();
                DisplaySelectedGames(selectedGames);
            }
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
            for (int i = 0; i < selectedGames.Length; i++)
            {
                if (selectedGames[i] != null)
                {
                    Console.WriteLine($"Game {selectedGames[i].Value + 1}");
                    ShowGame(selectedGames[i].Value);
                }
            }
            Thread.Sleep(1000);
        }

        private void CreateTasks(int maxGames, int x, int y)
        {
            Parallel.For(0, maxGames, i =>
            {
                var gameInstance = new GameRunner();
                var gameTask = new Task(() => gameInstance.Start(x, y));
                _gameTasks.TryAdd(i, gameTask);

                Action showAction = gameInstance.Show;
                _showActions.TryAdd(i, showAction);
            });
        }

        public delegate void PrintFieldAction(GameRunner gameRunnerInstance);


        private void StartAllTasks()
        {
            Parallel.ForEach(_gameTasks, gameTask =>
            {
                gameTask.Value.Start();
                Console.WriteLine($"{gameTask.Key + 1}. game is running");
            });
        }

        private void ShowGame(int i)
        {
            _showActions[i].Invoke();
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
