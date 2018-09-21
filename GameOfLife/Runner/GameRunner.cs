using GameOfLife.Drawer;
using GameOfLife.Interfaces;
using GameOfLife.Logger;
using GameOfLife.Logic;
using GameOfLife.Model;
using GameOfLife.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameOfLife.Runner
{
    public class GameRunner
    {
        private readonly IFieldDrawer<MatrixField> _consoleFieldDrawer;
        private readonly IUserInterface _consoleUserInterface;
        private readonly IGameLogic _gameLogic;
        private readonly IMatrixFieldLogic _matrixFieldLogic;
        private readonly ICellLogic _cellLogic;

        private MatrixField matrixField;

        private int iterations = 0;
        private int liveCells;

        public GameRunner()
        {
            _consoleFieldDrawer = new ConsoleFieldDrawer();
            _consoleUserInterface = new ConsoleUserInterface();
            _gameLogic = new GameLogic();
            _matrixFieldLogic = new MatrixFieldLogic();
            _cellLogic = new CellLogic();
        }

        public void Start()
        {
            InitGameField();
            //InitRestoredGame();
            InitGame();
            Run();
        }

        private void InitGameField()
        {
            var x = _consoleUserInterface.GetDimensionInput("x");
            var y = _consoleUserInterface.GetDimensionInput("y");

            matrixField = new MatrixField(x, y);

            var matrixFieldUnit = _matrixFieldLogic.GetFirstGeneration(matrixField.DimX, matrixField.DimY);
            matrixField.Field = matrixFieldUnit;
        }

        private void InitRestoredGame()
        {
            if (_consoleUserInterface.IsGameRestoreRequired())
            {
                var data = _gameLogic.RestoreGame();
            }
        }

        private void InitGame()
        {   
            _consoleFieldDrawer.DrawField(matrixField);
            liveCells = _cellLogic.CountLiveCells(matrixField);
        }

        private void Run()
        { 
            while (liveCells != 0 && !_gameLogic.IsTerminateGame())
            {
                matrixField = _gameLogic.EvolveGeneration(matrixField);
                _consoleFieldDrawer.DrawField(matrixField);

                _consoleUserInterface.AskForTerminateGame();

                liveCells = _cellLogic.CountLiveCells(matrixField);
                _consoleUserInterface.LiveCellsOutput(liveCells);

                iterations++;
                _consoleUserInterface.IterationsOutput(iterations);

                _consoleFieldDrawer.NormalizeFrame();
            }
            _consoleUserInterface.GameOverOutput();
            _gameLogic.SaveGame(matrixField, iterations, liveCells);
        }
    }
}
