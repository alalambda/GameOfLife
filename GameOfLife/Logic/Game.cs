using GameOfLife.FieldDrawer;
using GameOfLife.Interfaces;
using GameOfLife.Logger;
using GameOfLife.Model;
using GameOfLife.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameOfLife.Logic
{
    public class Game
    {
        private readonly ILogger<Data> _jsonLogger;
        private readonly IFieldDrawer<MatrixField> _consoleFieldDrawer;
        private readonly IUserInterface _consoleUserInterface;

        private MatrixField _matrixField;

        private int _iterations = 0;
        private int _liveCells;

        public Game()
        {
            _jsonLogger = new JsonLogger<Data>();
            _consoleFieldDrawer = new ConsoleFieldDrawer();
            _consoleUserInterface = new ConsoleUserInterface();
        }

        public void Start()
        {
            InitField();
            //InitRestoredGame();
            InitGame();
            Run();
        }

        private void InitField()
        {
            int x = _consoleUserInterface.GetDimensionInput("x");
            int y = _consoleUserInterface.GetDimensionInput("y");

            _matrixField = new MatrixField(x, y);
        }

        private void InitRestoredGame()
        {
            if (_consoleUserInterface.IsGameRestoreRequired())
            {
                var data = RestoreGame();
            }
        }

        private void InitGame()
        {
            _matrixField.ConfigureFirstGeneration();
            _consoleFieldDrawer.DrawField(_matrixField);
            _liveCells = _matrixField.CountLiveCells();
        }

        private void Run()
        { 
            while (_liveCells != 0 && !IsTerminateGame())
            {
                EvolveGeneration();

                _consoleUserInterface.AskForTerminateGame();

                _liveCells = _matrixField.CountLiveCells();
                _consoleUserInterface.LiveCellsOutput(_liveCells);

                _iterations++;
                _consoleUserInterface.IterationsOutput(_iterations);

                NormalizeFrame();
            }
            _consoleUserInterface.GameOverOutput();
            AskForSaveGame();
        }

        private void EvolveGeneration()
        {
            var nextGenField = _matrixField.ConfigureNextGeneration();
            _matrixField.Field = nextGenField;
            _consoleFieldDrawer.DrawField(_matrixField);
        }

        private void AskForSaveGame()
        {
            if (_liveCells != 0 && _consoleUserInterface.IsGameSaveRequired())
            {
                SaveGame();
            }
        }

        private void SaveGame()
        {
            var Data = new Data(_matrixField, _iterations, _liveCells);
            _jsonLogger.SaveGameToLogFile(Data);
        }

        private Data RestoreGame()
        {
            Data Data = _jsonLogger.RestoreLastGameFromLogFile();
            return Data;
        }

        private bool IsTerminateGame()
        {
            return _consoleUserInterface.IsAnyKeyPressed();
        }

        private void NormalizeFrame()
        {
            Thread.Sleep(1000);
        }
    }
}
