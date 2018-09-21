using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    interface IFieldGenerationLogic
    {
        Cell[,] GenerateRandomField(int dimX, int dimY);
    }
}
