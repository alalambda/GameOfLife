using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Helpers
{
    public class FieldGenerator
    {
        public MatrixFieldUnit RandomField(int dimX, int dimY)
        {
            MatrixFieldUnit matrixFieldUnit = new MatrixFieldUnit(dimX, dimY);
            for (int y = 0; y < dimX; y++)
            {
                for (int x = 0; x < dimY; x++)
                {
                    Cell cell = new Cell() { State = StateMethods.GetRandomState<State>() };
                    matrixFieldUnit.Cells[x, y] = cell;
                }
            }
            return matrixFieldUnit;
        }
    }
}
