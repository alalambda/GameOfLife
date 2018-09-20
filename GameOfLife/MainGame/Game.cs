using GameOfLife.FieldDrawer;
using GameOfLife.Interfaces;
using GameOfLife.Logger;
using GameOfLife.Logic;
using GameOfLife.Model;
using GameOfLife.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameOfLife.MainGame
{
    public class Game
    {
        private readonly IFieldDrawer<MatrixField> _consoleFieldDrawer;
        private readonly IUserInterface _consoleUserInterface;

        private readonly GameLogic _gameLogic;

        private MatrixField matrixField;

        private int iterations = 0;
        private int liveCells;

        public Game()
        {
            _consoleFieldDrawer = new ConsoleFieldDrawer();
            _consoleUserInterface = new ConsoleUserInterface();
            _gameLogic = new GameLogic();
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
            var x = _consoleUserInterface.GetDimensionInput("x");
            var y = _consoleUserInterface.GetDimensionInput("y");

            matrixField = new MatrixField(x, y);
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
            matrixField.ConfigureFirstGeneration();
            _consoleFieldDrawer.DrawField(matrixField);
            liveCells = matrixField.CountLiveCells();
        }

        private void Run()
        { 
            while (liveCells != 0 && !_gameLogic.IsTerminateGame())
            {
                matrixField = _gameLogic.EvolveGeneration(matrixField);
                _consoleFieldDrawer.DrawField(matrixField);

                _consoleUserInterface.AskForTerminateGame();

                liveCells = matrixField.CountLiveCells();
                _consoleUserInterface.LiveCellsOutput(liveCells);

                iterations++;
                _consoleUserInterface.IterationsOutput(iterations);

                _gameLogic.NormalizeFrame();
            }
            _consoleUserInterface.GameOverOutput();
            _gameLogic.AskForSaveGame(matrixField, iterations, liveCells);
        }
    }
}
