using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    interface IRuleLogic
    {
        Cell[,] ApplyNextGenerationRulesOnField(MatrixField matrixField);
    }
}
