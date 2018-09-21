using GameOfLife.Utils;
using GameOfLife.Interfaces;
using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Logic
{
    public class FieldGenerationLogic : IFieldGenerationLogic
    {
        public Cell[,] GenerateRandomField(int dimX, int dimY)
        {
            Cell[,] cells = new Cell[dimX, dimY];
            for (int y = 0; y < dimY; y++)
            {
                for (int x = 0; x < dimX; x++)
                {
                    Cell cell = new Cell() { State = EnumValueRandomizer.GetRandomEnumValue<State>() };
                    cells[x, y] = cell;
                }
            }
            return cells;
        }
    }
}
