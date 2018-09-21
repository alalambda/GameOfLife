using GameOfLife.Interfaces;
using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Logic
{
    class RuleLogic : IRuleLogic
    {
        private readonly ICellLogic _cellLogic;

        public RuleLogic()
        {
            _cellLogic = new CellLogic();
        }

        public Cell[,] ApplyNextGenerationRulesOnField(MatrixField matrixField)
        {
            var nextCells = new Cell[matrixField.DimX, matrixField.DimY];
            for (int y = 0; y < matrixField.DimY; y++)
            {
                for (int x = 0; x < matrixField.DimX; x++)
                {
                    var aliveNeighbours = _cellLogic.GetAliveNeighbours(x, y, matrixField);
                    var oldState = matrixField.Cells[x, y].State;
                    Cell cell = new Cell() { State = _cellLogic.DecideState(aliveNeighbours, oldState) };
                    nextCells[x, y] = cell;
                }
            }
            return nextCells;
        }
    }
}
