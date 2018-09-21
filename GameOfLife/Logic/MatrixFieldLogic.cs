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

        public MatrixFieldLogic()
        {
            _cellLogic = new CellLogic();
        }

        public MatrixFieldUnit GetFirstGeneration(int dimX, int dimY)
        {
            var matrixFieldUnit = new MatrixFieldUnit(dimX, dimY);
            for (int y = 0; y < dimY; y++)
            {
                for (int x = 0; x < dimX; x++)
                {
                    Cell cell = new Cell() { State = StateMethods.GetRandomState<State>() };
                    matrixFieldUnit.Cells[x, y] = cell;
                }
            }
            return matrixFieldUnit;
        }

        public MatrixFieldUnit GetNextGeneration(int dimX, int dimY)
        {
            var nextField = new MatrixFieldUnit(dimX, dimY);
            var matrixField = new MatrixField(dimX, dimY) { Field = nextField };
            for (int y = 0; y < dimY; y++)
            {
                for (int x = 0; x < dimX; x++)
                {
                    var aliveNeighbours = _cellLogic.GetAliveNeighbours(x, y, matrixField);
                    var oldState = nextField.Cells[x, y].State;
                    Cell cell = new Cell() { State = _cellLogic.DecideState(aliveNeighbours, oldState) };
                    nextField.Cells[x, y] = cell;
                }
            }
            return nextField;
        }
    }
}
