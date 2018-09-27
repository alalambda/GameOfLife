using GameOfLife.Interfaces;
using GameOfLife.Logic;
using GameOfLife.Model;
using GameOfLife.UserInterface;
using System.Threading;

namespace GameOfLife.Runner
{
    public class GameRunner
    {
        private readonly IUserInterface _consoleUserInterface;
        private readonly IGameLogic _gameLogic;
        private readonly IMatrixFieldLogic _matrixFieldLogic;
        private readonly ICellLogic _cellLogic;
        private readonly IRuleLogic _ruleLogic;

        public MatrixField MatrixField { get; set; }

        public GameRunner()
        {
            _consoleUserInterface = new ConsoleUserInterface();
            _gameLogic = new GameLogic();
            _matrixFieldLogic = new MatrixFieldLogic();
            _cellLogic = new CellLogic();
            _ruleLogic = new RuleLogic();
        }

        public void Start(int x, int y)
        {
            //if (_consoleUserInterface.IsGameRestoreRequired()) InitRestoredGame();
            //else InitNewGame();
            InitNewGame(x, y);
            //Show();
            InitLiveCells();
            Run();
        }

        public void InitNewGame(int x, int y)
        {
            MatrixField = new MatrixField(x, y);

            var cells = _matrixFieldLogic.GetFirstGeneration(x, y);
            MatrixField.Cells = cells;
        }

        public void InitRestoredGame()
        {
            var data = _gameLogic.RestoreGame();
            MatrixField = data.MatrixField;
            MatrixField.LiveCells = data.LiveCells;
            MatrixField.Iterations = data.Iterations;
        }

        public void Show()
        {
            _consoleUserInterface.OutputField(MatrixField);
        }

        public void InitLiveCells()
        {
            MatrixField.LiveCells = _cellLogic.CountLiveCells(MatrixField);
        }

        public void Run()
        { 
            while (MatrixField.LiveCells != 0 && !_consoleUserInterface.IsAnyKeyPressed())
            {
                MatrixField.Cells = _ruleLogic.ApplyNextGenerationRulesOnField(MatrixField);
                MatrixField.LiveCells = _cellLogic.CountLiveCells(MatrixField);
                MatrixField.Iterations++;

                Thread.Sleep(1000);
            }
        }
    }
}
