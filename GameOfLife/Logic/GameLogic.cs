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
    class GameLogic
    {
        private readonly ILogger<Data> _jsonLogger;
        private readonly IFieldDrawer<MatrixField> _consoleFieldDrawer;
        private readonly IUserInterface _consoleUserInterface;

        public GameLogic()
        {
            _jsonLogger = new JsonLogger<Data>();
            _consoleFieldDrawer = new ConsoleFieldDrawer();
            _consoleUserInterface = new ConsoleUserInterface();
        }

        public MatrixField EvolveGeneration(MatrixField matrixField)
        {
            var nextGenField = matrixField.ConfigureNextGeneration();
            matrixField.Field = nextGenField;
            return matrixField;
        }

        public void AskForSaveGame(MatrixField matrixField, int iterations, int liveCells)
        {
            if (liveCells != 0 && _consoleUserInterface.IsGameSaveRequired())
            {
                SaveGame(matrixField, iterations, liveCells);
            }
        }

        public void SaveGame(MatrixField matrixField, int iterations, int liveCells)
        {
            var Data = new Data(matrixField, iterations, liveCells);
            _jsonLogger.SaveGameToLogFile(Data);
        }

        public Data RestoreGame()
        {
            var Data = _jsonLogger.RestoreLastGameFromLogFile();
            return Data;
        }

        public bool IsTerminateGame()
        {
            return _consoleUserInterface.IsAnyKeyPressed();
        }

        public void NormalizeFrame()
        {
            Thread.Sleep(1000);
        }
    }
}
