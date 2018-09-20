using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    public interface IField<T>
    {
        int GetAliveNeighboursForCell(int x, int y);
        State DecideStateForCell(int aliveNeighbors, State state);
        void ConfigureFirstGeneration();
        T ConfigureNextGeneration();
    }
}
