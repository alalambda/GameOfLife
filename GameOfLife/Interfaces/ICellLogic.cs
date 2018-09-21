using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    interface ICellLogic
    {
        State DecideState(int aliveNeighbours, State oldState);
        int GetAliveNeighbours(int x, int y, MatrixField matrixField);
        int CountLiveCells(MatrixField matrixField);
    }
}
