using GameOfLife.Helpers;
using GameOfLife.Interfaces;
using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Logic
{
    public class MatrixFieldLogic : IMatrixFieldLogic
    {
        private readonly ICellLogic _cellLogic;
        private readonly IFieldGenerationLogic _fieldGenerationLogic;

        public MatrixFieldLogic()
        {
            _cellLogic = new CellLogic();
            _fieldGenerationLogic = new FieldGenerationLogic();
        }

        public MatrixFieldUnit GetFirstGeneration(int dimX, int dimY)
        {
            return _fieldGenerationLogic.GenerateRandomField(dimX, dimY);
        }

        public MatrixFieldUnit GetNextGeneration(int dimX, int dimY)
        {
            var nextFieldUnit = new MatrixFieldUnit(dimX, dimY);
            var matrixField = new MatrixField(dimX, dimY) { Field = nextFieldUnit };
            for (int y = 0; y < dimY; y++)
            {
                for (int x = 0; x < dimX; x++)
                {
                    var aliveNeighbours = _cellLogic.GetAliveNeighbours(x, y, matrixField);
                    var oldState = nextFieldUnit.Cells[x, y].State;
                    Cell cell = new Cell() { State = _cellLogic.DecideState(aliveNeighbours, oldState) };
                    nextFieldUnit.Cells[x, y] = cell;
                }
            }
            return nextFieldUnit;
        }
    }
}
