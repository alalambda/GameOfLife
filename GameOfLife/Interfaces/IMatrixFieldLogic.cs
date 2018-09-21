using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    interface IMatrixFieldLogic
    {
        MatrixFieldUnit GetFirstGeneration(int dimX, int dimY);
        MatrixFieldUnit GetNextGeneration(int dimX, int dimY);
    }
}
