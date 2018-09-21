﻿using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    interface IMatrixFieldLogic
    {
        Cell[,] GetFirstGeneration(int dimX, int dimY);
    }
}
