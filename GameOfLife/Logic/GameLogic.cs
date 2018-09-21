using GameOfLife.Drawer;
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
    public class GameLogic : IGameLogic
    {
        private readonly ILogger<Data> _jsonLogger;
        private readonly IFieldDrawer<MatrixField> _consoleFieldDrawer;
        private readonly IUserInterface _consoleUserInterface;
        private readonly IMatrixFieldLogic _matrixFieldLogic;

        public GameLogic()
        {
            _jsonLogger = new JsonLogger<Data>();
            _consoleFieldDrawer = new ConsoleFieldDrawer();
            _consoleUserInterface = new ConsoleUserInterface();
            _matrixFieldLogic = new MatrixFieldLogic();
        }

        public MatrixField EvolveGeneration(MatrixField matrixField)
        {
            var nextGenField = _matrixFieldLogic.GetFirstGeneration(matrixField.DimX, matrixField.DimY);
            matrixField.Field = nextGenField;
            return matrixField;
        }

        public void SaveGame(MatrixField matrixField, int iterations, int liveCells)
        {
            if (liveCells != 0 && _consoleUserInterface.IsGameSaveRequired())
            {
                var Data = new Data(matrixField, iterations, liveCells);
                _jsonLogger.SaveGameToLogFile(Data);
            }
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
    }
}
