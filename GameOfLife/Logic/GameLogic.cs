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
        private readonly IUserInterface _consoleUserInterface;
        private readonly IMatrixFieldLogic _matrixFieldLogic;

        public GameLogic()
        {
            _jsonLogger = new JsonLogger<Data>();
            _consoleUserInterface = new ConsoleUserInterface();
            _matrixFieldLogic = new MatrixFieldLogic();
        }

        public void SaveGame(MatrixField matrixField)
        {
            var Data = new Data(matrixField, matrixField.Iterations, matrixField.LiveCells);
            _jsonLogger.SaveGameToLogFile(Data);
        }

        public Data RestoreGame()
        {
            var Data = _jsonLogger.RestoreLastGameFromLogFile();
            return Data;
        }
    }
}
