using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Logic
{
    public interface IField<T>
    {
        int GetAliveNeighboursForCell(int x, int y);
        State DecideStateForCell(int aliveNeighbors, State state);
        void DrawField(T fieldUnit);
        void ConfigureFirstGeneration();
        T ConfigureNextGeneration();
    }
}
