using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    interface IFieldGenerationLogic
    {
        MatrixFieldUnit GenerateRandomField(int dimX, int dimY);
    }
}
