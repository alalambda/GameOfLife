using GameOfLife.Helpers;
using GameOfLife.Interfaces;
using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Logic
{
    public class FieldGenerationLogic : IFieldGenerationLogic
    {
        public MatrixFieldUnit GenerateRandomField(int dimX, int dimY)
        {
            MatrixFieldUnit matrixFieldUnit = new MatrixFieldUnit(dimX, dimY);
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
    }
}
